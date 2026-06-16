@echo off
chcp 65001 >nul
setlocal EnableDelayedExpansion
echo ============================================
echo   Push Database to Supabase
echo ============================================
echo.

cd /d "%~dp0"

REM Check if psql exists
if exist "C:\Program Files\PostgreSQL\18\bin\psql.exe" (
    set PSQL=C:\Program Files\PostgreSQL\18\bin\psql.exe
) else if exist "C:\Program Files\PostgreSQL\17\bin\psql.exe" (
    set PSQL=C:\Program Files\PostgreSQL\17\bin\psql.exe
) else if exist "C:\Program Files\PostgreSQL\16\bin\psql.exe" (
    set PSQL=C:\Program Files\PostgreSQL\16\bin\psql.exe
) else (
    where psql >nul 2>nul
    if %ERRORLEVEL% EQU 0 (
        set PSQL=psql
    ) else (
        echo [ERROR] psql not found!
        pause
        exit /b 1
    )
)

echo Using psql: %PSQL%
echo.

REM Use PowerShell to parse config and run psql
powershell -Command ^
    "$config = Get-Content 'db.config.txt' -Raw;" ^
    "$host_addr = ($config -split ';' | Where-Object { $_ -match '^Host=' }) -replace '^Host=';" ^
    "$port = ($config -split ';' | Where-Object { $_ -match '^Port=' }) -replace '^Port=';" ^
    "$database = ($config -split ';' | Where-Object { $_ -match '^Database=' }) -replace '^Database=';" ^
    "$username = ($config -split ';' | Where-Object { $_ -match '^Username=' }) -replace '^Username=';" ^
    "$password = ($config -split ';' | Where-Object { $_ -match '^Password=' }) -replace '^Password=';" ^
    "Write-Host 'Host:' $host_addr;" ^
    "Write-Host 'Database:' $database;" ^
    "Write-Host 'Username:' $username;" ^
    "Write-Host '';" ^
    "Write-Host 'Pushing database.sql...';" ^
    "$env:PGPASSWORD = $password;" ^
    "& '%PSQL%' -h $host_addr -p $port -U $username -d $database 'sslmode=require' -f database.sql;" ^
    "if ($LASTEXITCODE -eq 0) { Write-Host '' [SUCCESS] Database pushed! } else { Write-Host '' [ERROR] Failed }"

echo.
pause
