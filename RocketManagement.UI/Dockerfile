#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RocketManagement.UI/RocketManagement.UI.csproj", "RocketManagement.UI/"]
COPY ["RocketManagement.Infrastructure/RocketManagement.Infrastructure.csproj", "RocketManagement.Infrastructure/"]
COPY ["RocketManagement.Application/RocketManagement.Application.csproj", "RocketManagement.Application/"]
RUN dotnet restore "RocketManagement.UI/RocketManagement.UI.csproj"
COPY . .
WORKDIR "/src/RocketManagement.UI"
RUN dotnet build "RocketManagement.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RocketManagement.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RocketManagement.UI.dll"]
