@echo off

cd ../
for /f %%i in ('yarn -V') do set VAR=%%i
IF errorlevel 1 SET VAR="0"
IF "%VAR%"=="0.19.1" (
call yarn
) ELSE (
echo Installing yarn globally
call npm i yarn@0.19.1 -q -g --no-progress --no-spin
echo Installing project npm dependencies
call yarn
)
cd scripts