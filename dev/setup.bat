@echo off
:START
set /p id="Enter name: "

if "%id%"=="" (
	echo "Invalid name.."
	GOTO START
) ELSE (
	GOTO PROJECT	
)
:PROJECT
mkdir ..\script\"%id%"
xcopy /E /y template ..\scripts\"%id%"\
cd ..\script\"%id%"

pause