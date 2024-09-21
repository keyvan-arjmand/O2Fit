#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Social/SocialMessaging.API/SocialMessaging.API.csproj", "src/Services/Social/SocialMessaging.API/"]
COPY ["src/Services/Social/SocialMessaging.WebFramework/SocialMessaging.WebFramework.csproj", "src/Services/Social/SocialMessaging.WebFramework/"]
COPY ["src/Services/Social/SocialMessaging.Service/SocialMessaging.Service.csproj", "src/Services/Social/SocialMessaging.Service/"]
COPY ["src/Services/Social/SocialMessaging.Data/SocialMessaging.Data.csproj", "src/Services/Social/SocialMessaging.Data/"]
COPY ["src/Services/Social/SocialMessaging.Domain/SocialMessaging.Domain.csproj", "src/Services/Social/SocialMessaging.Domain/"]
COPY ["src/Services/Social/SocialMessaging.Common/SocialMessaging.Common.csproj", "src/Services/Social/SocialMessaging.Common/"]
RUN dotnet restore "src/Services/Social/SocialMessaging.API/SocialMessaging.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Social/SocialMessaging.API"
RUN dotnet build "SocialMessaging.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialMessaging.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMessaging.API.dll"]