#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Wallet/Wallet.Api/Wallet.Api.csproj", "src/Services/Wallet/Wallet.Api/"]
COPY ["src/Services/Wallet/Wallet.Application/Wallet.Application.csproj", "src/Services/Wallet/Wallet.Application/"]
COPY ["src/Services/Wallet/Wallet.Domain/Wallet.Domain.csproj", "src/Services/Wallet/Wallet.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Wallet/Wallet.Infrastructure/Wallet.Infrastructure.csproj", "src/Services/Wallet/Wallet.Infrastructure/"]
RUN dotnet restore "src/Services/Wallet/Wallet.Api/Wallet.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Wallet/Wallet.Api"
RUN dotnet build "Wallet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wallet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wallet.Api.dll"]