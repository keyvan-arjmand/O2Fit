#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Advertise/Advertising.API/Advertising.API.csproj", "src/Services/Advertise/Advertising.API/"]
COPY ["src/Services/Advertise/Advertising.WebFramework/Advertising.WebFramework.csproj", "src/Services/Advertise/Advertising.WebFramework/"]
COPY ["src/Services/Advertise/Advertising.Service/Advertising.Service.csproj", "src/Services/Advertise/Advertising.Service/"]
COPY ["src/Services/Advertise/Advertising.Data/Advertising.Data.csproj", "src/Services/Advertise/Advertising.Data/"]
COPY ["src/Services/Advertise/Advertising.Domain/Advertising.Domain.csproj", "src/Services/Advertise/Advertising.Domain/"]
COPY ["src/Services/Advertise/Advertising.Common/Advertising.Common.csproj", "src/Services/Advertise/Advertising.Common/"]
RUN dotnet restore "src/Services/Advertise/Advertising.API/Advertising.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Advertise/Advertising.API"
RUN dotnet build "Advertising.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advertising.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advertising.API.dll"]