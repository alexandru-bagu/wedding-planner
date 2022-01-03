#!/bin/bash

cd web
ASPNETCORE_ENVIRONMENT=Production ASPNETCORE_URLS=http://*:$PORT dotnet OOD.WeddingPlanner.Web.dll