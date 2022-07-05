using RocketManagement.BackgroundServices.BackgroundJobs;
using RocketManagement.BackgroundServices.Hubs;
using RocketManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<TelemetryBackgroundJob>();

builder.Services.AddCustomRetryPolicy();
builder.Services.AddSerializers();
builder.Services.AddListeners();
builder.Services.AddIntegrationServices();
builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TelemetryMessageHub>("/TelemetryMessageHub");
});

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.Run();
