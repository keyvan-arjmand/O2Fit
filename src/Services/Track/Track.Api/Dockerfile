FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Track/Track.Api/Track.Api.csproj", "src/Services/Track/Track.Api/"]
COPY ["src/Services/Track/Track.Application/Track.Application.csproj", "src/Services/Track/Track.Application/"]
COPY ["src/Services/Track/Track.Domain/Track.Domain.csproj", "src/Services/Track/Track.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Track/Track.Infrastructure/Track.Infrastructure.csproj", "src/Services/Track/Track.Infrastructure/"]
RUN dotnet restore "src/Services/Track/Track.Api/Track.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Track/Track.Api"
RUN dotnet build "Track.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Track.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Track.Api.dll"]
