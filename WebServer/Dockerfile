FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /web
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebServer/WebServer.csproj", "WebServer/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["PostgreSQL/PostgreSQL.csproj", "PostgreSQL/"]
COPY ["ApiServer/ApiServer.csproj", "ApiServer/"]
RUN dotnet restore "WebServer/WebServer.csproj"
COPY . .
WORKDIR "/src/WebServer"
RUN dotnet build "WebServer.csproj" -c $BUILD_CONFIGURATION -o /web/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebServer.csproj" -c $BUILD_CONFIGURATION -o /web/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /web
COPY --from=publish /web/publish .
ENTRYPOINT ["dotnet", "WebServer.dll"]
