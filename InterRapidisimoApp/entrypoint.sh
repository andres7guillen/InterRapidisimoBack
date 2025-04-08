#!/bin/bash

echo "🚀 Ejecutando migraciones..."

# Espera a que el SQL Server esté listo y luego corre las migraciones
/usr/bin/wait-for-it dotnet ef database update --project InterRapidisimoData/InterRapidisimoData.csproj