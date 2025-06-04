FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProductsMockApi/ProductsMockApi.csproj", "src/ProductsMockApi/"]
COPY ["src/ProductsMockApi.Application/ProductsMockApi.Application.csproj", "src/ProductsMockApi.Application/"]
RUN dotnet restore "src/ProductsMockApi/ProductsMockApi.csproj"
COPY . .
WORKDIR "/src/src/ProductsMockApi"
RUN dotnet build "./ProductsMockApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductsMockApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsMockApi.dll"]
