FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Chat/Chat.Api/Chat.Api.csproj", "src/Services/Chat/Chat.Api/"]
COPY ["src/Services/Chat/Chat.Application/Chat.Application.csproj", "src/Services/Chat/Chat.Application/"]
COPY ["src/Services/Chat/Chat.Domain/Chat.Domain.csproj", "src/Services/Chat/Chat.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Chat/Chat.Infrastructure/Chat.Infrastructure.csproj", "src/Services/Chat/Chat.Infrastructure/"]
RUN dotnet restore "src/Services/Chat/Chat.Api/Chat.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Chat/Chat.Api"
RUN dotnet build "Chat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Chat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.Api.dll"]
