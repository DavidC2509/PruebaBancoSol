#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/TrackFinance.Web/TrackFinance.Web.csproj", "src/TrackFinance.Web/"]
COPY ["src/TrackFinance.Infrastructure/TrackFinance.Infrastructure.csproj", "src/TrackFinance.Infrastructure/"]
COPY ["src/TrackFinance.Core/TrackFinance.Core.csproj", "src/TrackFinance.Core/"]
COPY ["src/TrackFinance.SharedKernel/TrackFinance.SharedKernel.csproj", "src/TrackFinance.SharedKernel/"]
RUN dotnet restore "src/TrackFinance.Web/TrackFinance.Web.csproj"
COPY . .
WORKDIR "/src/src/TrackFinance.Web"
RUN dotnet build "TrackFinance.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrackFinance.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackFinance.Web.dll"]