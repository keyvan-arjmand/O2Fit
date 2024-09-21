#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Order/Ordering.API/Ordering.API.csproj", "src/Services/Order/Ordering.API/"]
COPY ["src/Services/Order/Ordering.WebFramework/Ordering.WebFramework.csproj", "src/Services/Order/Ordering.WebFramework/"]
COPY ["src/Services/Order/Ordering.Service/Ordering.Service.csproj", "src/Services/Order/Ordering.Service/"]
COPY ["src/Services/Order/Ordering.Data/Ordering.Data.csproj", "src/Services/Order/Ordering.Data/"]
COPY ["src/Services/Order/Ordering.Domain/Ordering.Domain.csproj", "src/Services/Order/Ordering.Domain/"]
COPY ["src/Services/Order/Ordering.Common/Ordering.Common.csproj", "src/Services/Order/Ordering.Common/"]
RUN dotnet restore "src/Services/Order/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]