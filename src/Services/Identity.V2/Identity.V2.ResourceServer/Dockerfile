FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Identity.V2/Identity.V2.ResourceServer/Identity.V2.ResourceServer.csproj", "src/Services/Identity.V2/Identity.V2.ResourceServer/"]
RUN dotnet restore "src/Services/Identity.V2/Identity.V2.ResourceServer/Identity.V2.ResourceServer.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity.V2/Identity.V2.ResourceServer"
RUN dotnet build "Identity.V2.ResourceServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.V2.ResourceServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.V2.ResourceServer.dll"]
