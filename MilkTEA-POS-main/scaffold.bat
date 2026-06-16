@echo off
title EF Scaffold Tool

echo ==============================
echo   SCAFFOLD DATABASE (CLEAN)
echo ==============================

REM Check file tồn tại
if not exist db.config.txt (
    echo [ERROR] Missing db.config.txt
    pause
    exit /b
)

REM Đọc connection string
set /p CONN=<db.config.txt

echo [INFO] Scaffolding...

REM Chạy scaffold (ẩn warning + error rác)
dotnet ef dbcontext scaffold "%CONN%" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f --schema public >nul 2>&1

REM Check lỗi
if %errorlevel% neq 0 (
    echo [ERROR] Scaffold failed!
    echo Try checking connection or packages.
    pause
    exit /b
)

echo [SUCCESS] Models updated successfully!
echo ==============================

pause