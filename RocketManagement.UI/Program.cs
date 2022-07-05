using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RocketManagement.Application.Extensions;
using RocketManagement.Application.Features.Rocket.Queries;
using RocketManagement.Infrastructure.Configurations;
using RocketManagement.Infrastructure.Extensions;
using RocketManagement.Infrastructure.HttpClients;
using RocketManagement.UI.BackgroundJobs;
using RocketManagement.UI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddCustomMediatR();
builder.Services.AddInfrastructure();
builder.Services.Configure<HttpClientSettings>(options => builder.Configuration.GetSection("HttpClientSettings").Bind(options));
builder.Services.AddHttpClient<ServiceHttpClient>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<TelemetryBackgroundJob>();

using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
{
    var mediator = serviceProvider.GetRequiredService<IMediator>();

    var result = await mediator.Send(new GetAllRocketsQuery());

    if (!result.IsSuccess) { return; }

    foreach (var rocket in result.Model)
    {
        builder.Services.AddHealthChecks().AddTcpHealthCheck((options) =>
        {
            options.AddHost("host.docker.internal", rocket.Telemetry.Port);
        }, $"{rocket.Id} - {rocket.Telemetry.Host}:{rocket.Telemetry.Port}", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy);
    }
}

builder.Services.AddHealthChecksUI(s =>
   {
       s.AddHealthCheckEndpoint("Telemetry System", "/healthz");
   }).AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Rocket/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapHealthChecksUI();

    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rocket}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TelemetryMessageHub>("/TelemetryMessageHub");
});

app.Run();
