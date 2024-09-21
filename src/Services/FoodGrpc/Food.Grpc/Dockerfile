#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/FoodGrpc/Food.Grpc/Food.Grpc.csproj", "src/Services/FoodGrpc/Food.Grpc/"]
COPY ["src/Services/FoodGrpc/Food.Application/Food.Application.csproj", "src/Services/FoodGrpc/Food.Application/"]
COPY ["src/Services/FoodGrpc/Food.Domain/Food.Domain.csproj", "src/Services/FoodGrpc/Food.Domain/"]
COPY ["src/Services/FoodGrpc/Food.Infrastructure/Food.Infrastructure.csproj", "src/Services/FoodGrpc/Food.Infrastructure/"]
RUN dotnet restore "src/Services/FoodGrpc/Food.Grpc/Food.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/FoodGrpc/Food.Grpc"
RUN dotnet build "Food.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Food.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Food.Grpc.dll"]