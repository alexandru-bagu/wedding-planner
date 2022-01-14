#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Production 

pushd dbmigrator
  while ! dotnet OOD.WeddingPlanner.DbMigrator.dll; do
    sleep 1;
  done
popd

pushd web
  ASPNETCORE_URLS="http://*:44380;http://*:80" dotnet OOD.WeddingPlanner.Web.dll
popd