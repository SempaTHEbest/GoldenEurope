FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GoldenEurope.API/GoldenEurope.API.csproj", "GoldenEurope.API/"]
RUN dotnet restore "GoldenEurope.API/GoldenEurope.API.csproj"
COPY . .
WORKDIR "/src/GoldenEurope.API"
RUN dotnet build "./GoldenEurope.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GoldenEurope.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoldenEurope.API.dll"]
