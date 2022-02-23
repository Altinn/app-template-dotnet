cd .\Altinn.App.Api\
dotnet build -c Release
dotnet pack -c Release --include-source -p:SymbolPackageFormat=snupkg
cd .\bin\release
cd ..\..\..

cd .\Altinn.App.Common\
dotnet build -c Release
dotnet pack -c Release --include-source -p:SymbolPackageFormat=snupkg
cd .\bin\release
cd ..\..\..

cd .\Altinn.App.PlatformServices\
dotnet build -c Release
dotnet pack -c Release --include-source -p:SymbolPackageFormat=snupkg
cd .\bin\release
cd ..\..\..

dotnet nuget push Altinn.App.PlatformServices.4.27.0-alpha.1.nupkg -k oy2pgigwm6l4jpiwnznakc62izwk5ctujotbaewdf6nrwi -s https://api.nuget.org/v3/index.json
dotnet nuget push Altinn.App.Common.4.27.0-alpha.1.nupkg -k oy2pgigwm6l4jpiwnznakc62izwk5ctujotbaewdf6nrwi -s https://api.nuget.org/v3/index.json
dotnet nuget push Altinn.App.Api.4.27.0-alpha.1.nupkg -k oy2pgigwm6l4jpiwnznakc62izwk5ctujotbaewdf6nrwi -s https://api.nuget.org/v3/index.json