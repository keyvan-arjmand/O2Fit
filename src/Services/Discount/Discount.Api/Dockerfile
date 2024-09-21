#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Discount/Discount.Api/Discount.Api.csproj", "src/Services/Discount/Discount.Api/"]
COPY ["src/Services/Discount/Discount.Application/Discount.Application.csproj", "src/Services/Discount/Discount.Application/"]
COPY ["src/Services/Discount/Discount.Domain/Discount.Domain.csproj", "src/Services/Discount/Discount.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Discount/Discount.Infrastructure/Discount.Infrastructure.csproj", "src/Services/Discount/Discount.Infrastructure/"]
RUN dotnet restore "src/Services/Discount/Discount.Api/Discount.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount/Discount.Api"
RUN dotnet build "Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]