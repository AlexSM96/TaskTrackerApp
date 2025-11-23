FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskTracker.WebAPI/TaskTracker.WebAPI.csproj", "TaskTracker.WebAPI/"]
COPY ["TaskTracker.Persistance/TaskTracker.Persistance.csproj", "TaskTracker.Persistance/"]
COPY ["TaskTracker.Application/TaskTracker.Application.csproj", "TaskTracker.Application/"]
COPY ["TaskTracker.Domain/TaskTracker.Domain.csproj", "TaskTracker.Domain/"]
RUN dotnet restore "TaskTracker.WebAPI/TaskTracker.WebAPI.csproj"
COPY . .
WORKDIR "/src/TaskTracker.WebAPI"
RUN dotnet build "./TaskTracker.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskTracker.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTracker.WebAPI.dll"]
