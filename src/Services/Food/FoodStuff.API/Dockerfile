#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Food/FoodStuff.API/FoodStuff.API.csproj", "src/Services/Food/FoodStuff.API/"]
COPY ["src/Services/Food/FoodStuff.WebFramework/FoodStuff.WebFramework.csproj", "src/Services/Food/FoodStuff.WebFramework/"]
COPY ["src/Services/Food/FoodStuff.Service/FoodStuff.Service.csproj", "src/Services/Food/FoodStuff.Service/"]
COPY ["src/Services/Food/FoodStuff.Data/FoodStuff.Data.csproj", "src/Services/Food/FoodStuff.Data/"]
COPY ["src/Services/Food/FoodStuff.Domain/FoodStuff.Domain.csproj", "src/Services/Food/FoodStuff.Domain/"]
COPY ["src/Services/Food/FoodStuff.Common/FoodStuff.Common.csproj", "src/Services/Food/FoodStuff.Common/"]
RUN dotnet restore "src/Services/Food/FoodStuff.API/FoodStuff.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Food/FoodStuff.API"
RUN dotnet build "FoodStuff.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodStuff.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodStuff.API.dll"]