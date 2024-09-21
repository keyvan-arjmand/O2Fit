#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Blog/Blogging.API/Blogging.API.csproj", "src/Services/Blog/Blogging.API/"]
COPY ["src/Services/Blog/Blogging.WebFramework/Blogging.WebFramework.csproj", "src/Services/Blog/Blogging.WebFramework/"]
COPY ["src/Services/Blog/Blogging.Service/Blogging.Service.csproj", "src/Services/Blog/Blogging.Service/"]
COPY ["src/Services/Blog/Blogging.Data/Blogging.Data.csproj", "src/Services/Blog/Blogging.Data/"]
COPY ["src/Services/Blog/Blogging.Domain/Blogging.Domain.csproj", "src/Services/Blog/Blogging.Domain/"]
COPY ["src/Services/Blog/Blogging.Common/Blogging.Common.csproj", "src/Services/Blog/Blogging.Common/"]
RUN dotnet restore "src/Services/Blog/Blogging.API/Blogging.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Blog/Blogging.API"
RUN dotnet build "Blogging.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blogging.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blogging.API.dll"]