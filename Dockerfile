# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

RUN apt -y update
RUN apt -y install git

COPY .git /app/.git
WORKDIR /app
RUN git checkout .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/build/web src/OOD.WeddingPlanner.Web/OOD.WeddingPlanner.Web.csproj

# # Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY container/entrypoint.sh /app
COPY --from=build-env /app/build .
#ENTRYPOINT /app/entrypoint.sh