FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChuckIt-Backend/ChuckItApi.csproj", "ChuckIt-Backend/"]
RUN dotnet restore "./ChuckIt-Backend/ChuckItApi.csproj"
COPY . .
WORKDIR "/src/ChuckIt-Backend"
RUN dotnet build "./ChuckItApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChuckItApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChuckItApi.dll"]