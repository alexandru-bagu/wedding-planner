# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

RUN apt -y update && apt -y install git
WORKDIR /app
COPY .git /app/.git
RUN git checkout .
RUN dotnet publish -c Release -o /app/build/web src/OOD.WeddingPlanner.Web/OOD.WeddingPlanner.Web.csproj
RUN dotnet publish -c Release -o /app/build/dbmigrator src/OOD.WeddingPlanner.DbMigrator/OOD.WeddingPlanner.DbMigrator.csproj

# # Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
WORKDIR /app
COPY container/entrypoint.sh /app
COPY --from=build-env /app/build .
ENTRYPOINT /app/entrypoint.sh