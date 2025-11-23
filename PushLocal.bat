@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Api.Config\bin\Debug\Cadmus.Api.Config.11.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers\bin\Debug\Cadmus.Api.Controllers.13.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers.Export\bin\Debug\Cadmus.Api.Controllers.Export.1.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Controllers.Import\bin\Debug\Cadmus.Api.Controllers.Import.12.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Models\bin\Debug\Cadmus.Api.Models.11.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Api.Services\bin\Debug\Cadmus.Api.Services.13.0.0.nupkg -source C:\Projects\_NuGet
pause
