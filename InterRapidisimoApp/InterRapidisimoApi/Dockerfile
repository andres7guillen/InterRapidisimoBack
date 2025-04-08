#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InterRapidisimoApi/InterRapidisimoApi.csproj", "InterRapidisimoApi/"]
COPY ["InterRapidisimoApplication/InterRapidisimoApplication.csproj", "InterRapidisimoApplication/"]
COPY ["InterRapidisimoData/InterRapidisimoData.csproj", "InterRapidisimoData/"]
COPY ["InterRapidisimoDomain/InterRapidisimoDomain.csproj", "InterRapidisimoDomain/"]
COPY ["InterRapidisimoInfrastructure/InterRapidisimoInfrastructure.csproj", "InterRapidisimoInfrastructure/"]
RUN dotnet restore "./InterRapidisimoApi/InterRapidisimoApi.csproj"
COPY . .
WORKDIR "/src/InterRapidisimoApi"
RUN dotnet build "./InterRapidisimoApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InterRapidisimoApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterRapidisimoApi.dll"]