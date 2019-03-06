@echo off
setlocal enabledelayedexpansion

for /f "usebackq tokens=*" %%i in (`vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)
:: without ENABLEEXTENSIONS, %InstallDir% is only available outside the above loop.
for %%v in (15.0) do (
    if exist "%InstallDir%\MSBuild\%%v\Bin\MSBuild.exe" (
        set msBuildExe="%InstallDir%\MSBuild\%%v\Bin\MSBuild.exe"
    goto :finish
  )
)
:finish
endlocal & if not [%msBuildExe%]==[] if exist %msBuildExe% ( echo %msBuildExe% )