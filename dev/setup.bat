@echo off
:START
set /p id="Enter script name: "

if "%id%"=="" (
	echo "Invalid name.."
	GOTO START
) ELSE (
	GOTO PROJECT	
)
:PROJECT
mkdir ..\scripts\"%id%"
xcopy /E /y template ..\scripts\"%id%"\

pause