FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Advertise.V2/Advertise.Api/Advertise.Api.csproj", "src/Services/Advertise.V2/Advertise.Api/"]
COPY ["src/Services/Advertise.V2/Advertise.Application/Advertise.Application.csproj", "src/Services/Advertise.V2/Advertise.Application/"]
COPY ["src/Services/Advertise.V2/Advertise.Domain/Advertise.Domain.csproj", "src/Services/Advertise.V2/Advertise.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Advertise.V2/Advertise.Infrastructure/Advertise.Infrastructure.csproj", "src/Services/Advertise.V2/Advertise.Infrastructure/"]
RUN dotnet restore "src/Services/Advertise.V2/Advertise.Api/Advertise.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Advertise.V2/Advertise.Api"
RUN dotnet build "Advertise.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advertise.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advertise.Api.dll"]
