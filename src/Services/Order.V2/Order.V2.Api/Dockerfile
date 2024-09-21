#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Order.V2/Order.V2.Api/Order.V2.Api.csproj", "src/Services/Order.V2/Order.V2.Api/"]
COPY ["src/Services/Order.V2/Order.V2.Application/Order.V2.Application.csproj", "src/Services/Order.V2/Order.V2.Application/"]
COPY ["src/Services/Order.V2/Order.V2.Domain/Order.V2.Domain.csproj", "src/Services/Order.V2/Order.V2.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Order.V2/Order.V2.Infrastructure/Order.V2.Infrastructure.csproj", "src/Services/Order.V2/Order.V2.Infrastructure/"]
RUN dotnet restore "src/Services/Order.V2/Order.V2.Api/Order.V2.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Order.V2/Order.V2.Api"
RUN dotnet build "Order.V2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.V2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.V2.Api.dll"]