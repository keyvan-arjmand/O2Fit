#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/User/User.API/User.API.csproj", "src/services/User/User.API/"]
COPY ["src/Services/User/User.WebFramework/User.WebFramework.csproj", "src/Services/User/User.WebFramework/"]
COPY ["src/Services/User/User.Service/User.Service.csproj", "src/Services/User/User.Service/"]
COPY ["src/Services/User/User.Data/User.Data.csproj", "src/Services/User/User.Data/"]
COPY ["src/Services/User/User.Domain/User.Domain.csproj", "src/Services/User/User.Domain/"]
COPY ["src/Services/User/User.Common/User.Common.csproj", "src/Services/User/User.Common/"]
RUN dotnet restore "src/services/User/User.API/User.API.csproj"
COPY . .
WORKDIR "/src/src/Services/User/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]