FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Food.V2/Food.V2.Api/Food.V2.Api.csproj", "src/Services/Food.V2/Food.V2.Api/"]
COPY ["src/Services/Food.V2/Food.V2.Application/Food.V2.Application.csproj", "src/Services/Food.V2/Food.V2.Application/"]
COPY ["src/Services/Food.V2/Food.V2.Domain/Food.V2.Domain.csproj", "src/Services/Food.V2/Food.V2.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Food.V2/Food.V2.Infrastructure/Food.V2.Infrastructure.csproj", "src/Services/Food.V2/Food.V2.Infrastructure/"]
RUN dotnet restore "src/Services/Food.V2/Food.V2.Api/Food.V2.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Food.V2/Food.V2.Api"
RUN dotnet build "Food.V2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Food.V2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Food.V2.Api.dll"]
