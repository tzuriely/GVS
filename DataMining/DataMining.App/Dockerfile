#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DataMining/DataMining.App/DataMining.App.csproj", "DataMining/DataMining.App/"]
COPY ["DataMining/DataMining.Robots/DataMining.Robots.csproj", "DataMining/DataMining.Robots/"]
COPY ["DataMining/DataMining.Shared/DataMining.Shared.csproj", "DataMining/DataMining.Shared/"]
RUN dotnet restore "DataMining/DataMining.App/DataMining.App.csproj"
COPY . .
WORKDIR "/src/DataMining/DataMining.App"
RUN dotnet build "DataMining.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataMining.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataMining.App.dll"]