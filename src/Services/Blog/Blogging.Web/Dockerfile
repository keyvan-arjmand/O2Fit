FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Blog/Blogging.Web/Blogging.Web.csproj", "src/Services/Blog/Blogging.Web/"]
RUN dotnet restore "src/Services/Blog/Blogging.Web/Blogging.Web.csproj"
COPY . .
WORKDIR "/src/src/Services/Blog/Blogging.Web"
RUN dotnet build "Blogging.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blogging.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blogging.Web.dll"]
