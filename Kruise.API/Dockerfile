#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kruise.API/Kruise.API.csproj", "Kruise.API/"]
COPY ["Kruise.BusinessLogic/Kruise.BusinessLogic.csproj", "Kruise.BusinessLogic/"]
COPY ["Kruise.Domain/Kruise.Domain.csproj", "Kruise.Domain/"]
COPY ["Kruise.DataAccess.Postgres/Kruise.DataAccess.Postgres.csproj", "Kruise.DataAccess.Postgres/"]
RUN dotnet restore "Kruise.API/Kruise.API.csproj"
COPY . .
WORKDIR "/src/Kruise.API"
RUN dotnet build "Kruise.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kruise.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kruise.API.dll"]