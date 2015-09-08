@echo off
if not exist "Build" mkdir "Build"
set unity="C:\Program Files\Unity\Editor\Unity.exe"
echo Located unity at %unity%
echo Building...
%unity% -batchmode -nographics -logFile Build/buildlog.txt -buildWindowsPlayer Build/Win32/500m.exe -buildWindows64Player Build/Win64/500m.exe -buildLinuxUniversalPlayer Build/Linux/500m -buildOSX64Player Build/MacOSX/500m.app -quit