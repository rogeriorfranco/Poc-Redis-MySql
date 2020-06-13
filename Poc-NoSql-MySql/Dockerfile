#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Poc-NoSql-MySql/Poc-NoSql-MySql.csproj", "Poc-NoSql-MySql/"]
RUN dotnet restore "Poc-NoSql-MySql/Poc-NoSql-MySql.csproj"
COPY . .
WORKDIR "/src/Poc-NoSql-MySql"
RUN dotnet build "Poc-NoSql-MySql.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poc-NoSql-MySql.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc-NoSql-MySql.dll"]