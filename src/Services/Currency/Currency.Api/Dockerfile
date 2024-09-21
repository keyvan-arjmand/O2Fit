FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Currency/Currency.Api/Currency.Api.csproj", "src/Services/Currency/Currency.Api/"]
COPY ["src/Services/Currency/Currency.Application/Currency.Application.csproj", "src/Services/Currency/Currency.Application/"]
COPY ["src/Services/Currency/Currency.Domain/Currency.Domain.csproj", "src/Services/Currency/Currency.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Currency/Currency.Infrastructure/Currency.Infrastructure.csproj", "src/Services/Currency/Currency.Infrastructure/"]
RUN dotnet restore "src/Services/Currency/Currency.Api/Currency.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Currency/Currency.Api"
RUN dotnet build "Currency.Api.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Currency.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Currency.Api.dll"]
