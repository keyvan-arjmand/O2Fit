FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Wallet/Wallet.Grpc/Wallet.Grpc.csproj", "src/Services/Wallet/Wallet.Grpc/"]
COPY ["src/Services/Wallet/Wallet.Application/Wallet.Application.csproj", "src/Services/Wallet/Wallet.Application/"]
COPY ["src/Services/Wallet/Wallet.Domain/Wallet.Domain.csproj", "src/Services/Wallet/Wallet.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/Services/Wallet/Wallet.Infrastructure/Wallet.Infrastructure.csproj", "src/Services/Wallet/Wallet.Infrastructure/"]
RUN dotnet restore "src/Services/Wallet/Wallet.Grpc/Wallet.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Wallet/Wallet.Grpc"
RUN dotnet build "Wallet.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wallet.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wallet.Grpc.dll"]
