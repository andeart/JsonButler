@echo off

set configurationName=%1
set runTests=%2

if not "%configurationName%"=="Release" (
    set "configurationName=Debug"
)

nuget restore ./JsonButler/JsonButler.sln
python ./Scripts/clear_fody_refs.py

for /f "usebackq tokens=*" %%m in (`"%~dp0get-msBuild-path.bat"`) do (
    call %%m /p:Configuration=%configurationName% ./JsonButler/JsonButler.sln
)

if "%configurationName%"=="Debug" (
    if "%runTests%" == "true" (
        dotnet vstest ./JsonButler/JsonButler.Tests/bin/Debug/Andeart.JsonButler.Tests.dll /Framework:.NETFramework,Version=v4.7.1 /InIsolation /logger:trx
        goto finish
    )
)
:notestsrun
echo No tests were run.
exit /b 9
:finish
echo Tests were run.