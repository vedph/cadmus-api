@echo off
echo PUSH PACKAGES TO NUGET
prompt
set nu=C:\Exe\nuget.exe
set src=-Source https://api.nuget.org/v3/index.json

%nu% push .\Cadmus.Api.Config\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Api.Models\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Api.Services\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Api.Controllers\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Api.Controllers.Export\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Api.Controllers.Import\bin\Debug\*.nupkg %src% -SkipDuplicate
echo COMPLETED
echo on
