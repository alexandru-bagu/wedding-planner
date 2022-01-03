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
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build-env /app/build .
COPY container/entrypoint.sh /app
ENTRYPOINT /app/entrypoint.sh