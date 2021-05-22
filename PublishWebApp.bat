if not exist "%~dp0publish" mkdir "%~dp0publish"

set "OutputPath=%~dp0publish\WebApp"

rmdir "%OutputPath%" /s /q

mkdir "%OutputPath%"

cd %~dp0GomokuOutput
dotnet publish --output "%OutputPath%" --configuration release --self-contained true -r win-x64

pause;