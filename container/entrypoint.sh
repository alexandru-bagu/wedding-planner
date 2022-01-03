#!/bin/bash

dotnet dbmigrator/OOD.WeddingPlanner.DbMigrator.dll && \
dotnet web/OOD.WeddingPlanner.Web.dll