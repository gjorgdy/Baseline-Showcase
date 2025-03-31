FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /api
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiServer/ApiServer.csproj", "ApiServer/"]
COPY ["PostgreSQL/PostgreSQL.csproj", "PostgreSQL/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "ApiServer/ApiServer.csproj"
COPY . .
WORKDIR "/src/ApiServer"
RUN dotnet build "ApiServer.csproj" -c $BUILD_CONFIGURATION -o /api/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiServer.csproj" -c $BUILD_CONFIGURATION -o /api/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /api
COPY --from=publish /api/publish .
RUN touch .env
ENTRYPOINT ["dotnet", "ApiServer.dll"]
