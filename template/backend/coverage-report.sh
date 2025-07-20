#!/bin/bash
echo "Install tools if not present"
dotnet tool install --global coverlet.console || true
dotnet tool install --global dotnet-reportgenerator-globaltool || true

echo "Clean and build solution"
dotnet restore
dotnet build Ambev.DeveloperEvaluation.sln --configuration Release --no-restore

echo "Run tests with coverage"
dotnet test ./tests/Ambev.DeveloperEvaluation.Unit \
--no-restore --verbosity normal \
/p:CollectCoverage=true \
/p:CoverletOutputFormat=cobertura \
/p:CoverletOutput=./TestResults/coverage.cobertura.xml
/p:CollectCoverage=true \
/p:CoverletOutputFormat=cobertura \
/p:CoverletOutput=./TestResults/coverage.cobertura.xml

echo "Generate coverage report"
reportgenerator \
-reports:"./TestResults/coverage.cobertura.xml" \
-targetdir:"./TestResults/CoverageReport" \
-reporttypes:Html

echo ""
echo "Coverage report generated at TestResults/CoverageReport/index.html"
read -p "Pressione Enter para continuar..."