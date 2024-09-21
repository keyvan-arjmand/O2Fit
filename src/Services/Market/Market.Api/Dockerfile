FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Market/Market.Api/Market.Api.csproj", "src/Services/Market/Market.Api/"]
COPY ["src/Services/Market/Market.Application/Market.Application.csproj", "src/Services/Market/Market.Application/"]
COPY ["src/Services/Market/Market.Domain/Market.Domain.csproj", "src/Services/Market/Market.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Market/Market.Infrastructure/Market.Infrastructure.csproj", "src/Services/Market/Market.Infrastructure/"]
RUN dotnet restore "src/Services/Market/Market.Api/Market.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Market/Market.Api"
RUN dotnet build "Market.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Market.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Market.Api.dll"]
