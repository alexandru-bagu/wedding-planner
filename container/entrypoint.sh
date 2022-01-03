#!/bin/bash

dotnet dbmigrator/OOD.WeddingPlanner.DbMigrator.dll && \
ASPNETCORE_URLS=http://*:$PORT dotnet web/OOD.WeddingPlanner.Web.dll