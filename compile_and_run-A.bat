@echo off
:: Set the path to the C# compiler
set CSC_PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe"

:: Compile the C# program
%CSC_PATH% /target:winexe /out:IQTest-App.exe IQTestForm.cs

:: Check if the compilation was successful
if exist IQTest-App.exe (
    echo Compilation successful. Running the program...
    :: Run the compiled program
    IQTest-App.exe
) else (
    echo Compilation failed.
)
pause
