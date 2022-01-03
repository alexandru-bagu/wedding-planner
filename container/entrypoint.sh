#!/bin/bash

cd web
ASPNETCORE_ENVIRONMENT=Production ASPNETCORE_URLS=https://*:$PORT dotnet OOD.WeddingPlanner.Web.dll