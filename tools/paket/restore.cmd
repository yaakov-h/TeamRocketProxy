@echo off
pushd .
cd /d %~dp0..\..\

echo Restoring packages....
tools\paket\paket.bootstrapper.exe
tools\paket\paket.exe restore

popd