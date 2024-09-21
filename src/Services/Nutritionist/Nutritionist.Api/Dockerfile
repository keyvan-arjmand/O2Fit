FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Nutritionist/Nutritionist.Api/Nutritionist.Api.csproj", "src/Services/Nutritionist/Nutritionist.Api/"]
COPY ["src/Services/Nutritionist/Nutritionist.Application/Nutritionist.Application.csproj", "src/Services/Nutritionist/Nutritionist.Application/"]
COPY ["src/Services/Nutritionist/Nutritionist.Domain/Nutritionist.Domain.csproj", "src/Services/Nutritionist/Nutritionist.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Nutritionist/Nutritionist.Infrastructure/Nutritionist.Infrastructure.csproj", "src/Services/Nutritionist/Nutritionist.Infrastructure/"]
RUN dotnet restore "src/Services/Nutritionist/Nutritionist.Api/Nutritionist.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Nutritionist/Nutritionist.Api"
RUN dotnet build "Nutritionist.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nutritionist.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nutritionist.Api.dll"]
