FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/YarpApiGatewayServiceDiscovery/YarpApiGatewayServiceDiscovery.csproj", "src/ApiGateways/YarpApiGatewayServiceDiscovery/"]
RUN dotnet restore "src/ApiGateways/YarpApiGatewayServiceDiscovery/YarpApiGatewayServiceDiscovery.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/YarpApiGatewayServiceDiscovery"
RUN dotnet build "YarpApiGatewayServiceDiscovery.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YarpApiGatewayServiceDiscovery.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpApiGatewayServiceDiscovery.dll"]
