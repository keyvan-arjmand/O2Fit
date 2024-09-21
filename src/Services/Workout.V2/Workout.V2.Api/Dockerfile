FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Workout.V2/Workout.V2.Api/Workout.V2.Api.csproj", "src/Services/Workout.V2/Workout.V2.Api/"]
COPY ["src/Services/Workout.V2/Workout.V2.Application/Workout.V2.Application.csproj", "src/Services/Workout.V2/Workout.V2.Application/"]
COPY ["src/Services/Workout.V2/Workout.V2.Domain/Workout.V2.Domain.csproj", "src/Services/Workout.V2/Workout.V2.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Workout.V2/Workout.V2.Infrastructure/Workout.V2.Infrastructure.csproj", "src/Services/Workout.V2/Workout.V2.Infrastructure/"]
RUN dotnet restore "src/Services/Workout.V2/Workout.V2.Api/Workout.V2.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Workout.V2/Workout.V2.Api"
RUN dotnet build "Workout.V2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workout.V2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workout.V2.Api.dll"]
