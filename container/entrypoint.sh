#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Production 

pushd dbmigrator
  dotnet OOD.WeddingPlanner.DbMigrator.dll
popd

pushd web
  ASPNETCORE_URLS=http://*:5000 dotnet OOD.WeddingPlanner.Web.dll
popd