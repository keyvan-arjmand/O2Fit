#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/WorkOut/WorkoutTracker.API/WorkoutTracker.API.csproj", "src/Services/WorkOut/WorkoutTracker.API/"]
COPY ["src/Services/WorkOut/WorkoutTracker.WebFramework/WorkoutTracker.WebFramework.csproj", "src/Services/WorkOut/WorkoutTracker.WebFramework/"]
COPY ["src/Services/WorkOut/WorkoutTracker.Service/WorkoutTracker.Service.csproj", "src/Services/WorkOut/WorkoutTracker.Service/"]
COPY ["src/Services/WorkOut/WorkoutTracker.Data/WorkoutTracker.Data.csproj", "src/Services/WorkOut/WorkoutTracker.Data/"]
COPY ["src/Services/WorkOut/WorkoutTracker.Domain/WorkoutTracker.Domain.csproj", "src/Services/WorkOut/WorkoutTracker.Domain/"]
COPY ["src/Services/WorkOut/WorkoutTracker.Common/WorkoutTracker.Common.csproj", "src/Services/WorkOut/WorkoutTracker.Common/"]
RUN dotnet restore "src/Services/WorkOut/WorkoutTracker.API/WorkoutTracker.API.csproj"
COPY . .
WORKDIR "/src/src/Services/WorkOut/WorkoutTracker.API"
RUN dotnet build "WorkoutTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkoutTracker.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkoutTracker.API.dll"]