FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Identity.V2/Identity.V2.Grpc/Identity.V2.Grpc.csproj", "src/Services/Identity.V2/Identity.V2.Grpc/"]
COPY ["src/Services/Identity.V2/Identity.V2.Application/Identity.V2.Application.csproj", "src/Services/Identity.V2/Identity.V2.Application/"]
COPY ["src/Services/Identity.V2/Identity.V2.Domain/Identity.V2.Domain.csproj", "src/Services/Identity.V2/Identity.V2.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/Services/Identity.V2/Identity.V2.Infrastructure/Identity.V2.Infrastructure.csproj", "src/Services/Identity.V2/Identity.V2.Infrastructure/"]
RUN dotnet restore "src/Services/Identity.V2/Identity.V2.Grpc/Identity.V2.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity.V2/Identity.V2.Grpc"
RUN dotnet build "Identity.V2.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.V2.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.V2.Grpc.dll"]
