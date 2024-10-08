﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Ticket/Ticket.Api/Ticket.Api.csproj", "src/Services/Ticket/Ticket.Api/"]
COPY ["src/Services/Ticket/Ticket.Domain/Ticket.Domain.csproj", "src/Services/Ticket/Ticket.Domain/"]
COPY ["src/BuildingBlocks/Common.Constants/Common.Constants.csproj", "src/BuildingBlocks/Common.Constants/"]
COPY ["src/BuildingBlocks/Common.Enums/Common.Enums.csproj", "src/BuildingBlocks/Common.Enums/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Ticket/Ticket.Infrastructure/Ticket.Infrastructure.csproj", "src/Services/Ticket/Ticket.Infrastructure/"]
COPY ["src/Services/Ticket/Ticket.Application/Ticket.Application.csproj", "src/Services/Ticket/Ticket.Application/"]
RUN dotnet restore "src/Services/Ticket/Ticket.Api/Ticket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Ticket/Ticket.Api"
RUN dotnet build "Ticket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticket.Api.dll"]
