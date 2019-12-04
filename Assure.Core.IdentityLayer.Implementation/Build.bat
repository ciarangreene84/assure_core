@ECHO OFF
SET version=%1
ECHO %version% > version.txt

dotnet pack Assure.Core.IdentityLayer.Implementation.csproj /p:Version=%version%
dotnet nuget push bin\Debug\Assure.Core.IdentityLayer.Implementation.%version%.nupkg --api-key 7e2daaa8-2b0c-4c0e-8434-fe1a40856c2f --source https://www.myget.org/F/assure/api/v2/package