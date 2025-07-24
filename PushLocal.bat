@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Api.Config\bin\Debug\Cadmus.Api.Config.10.1.19.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers\bin\Debug\Cadmus.Api.Controllers.12.0.4.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers.Export\bin\Debug\Cadmus.Api.Controllers.Export.0.0.3.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers.Import\bin\Debug\Cadmus.Api.Controllers.Import.11.0.8.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Models\bin\Debug\Cadmus.Api.Models.10.1.16.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Services\bin\Debug\Cadmus.Api.Services.12.0.4.nupkg -source C:\Projects\_NuGet
pause
