#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/O2fitUserHistory.Api/O2fitUserHistory.Api.csproj", "src/Services/O2fitUserHistory.Api/"]
RUN dotnet restore "src/Services/O2fitUserHistory.Api/O2fitUserHistory.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/O2fitUserHistory.Api"
RUN dotnet build "O2fitUserHistory.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "O2fitUserHistory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "O2fitUserHistory.Api.dll"]