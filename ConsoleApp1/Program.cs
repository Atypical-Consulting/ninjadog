// See https://aka.ms/new-console-template for more information

using Spectre.Console;
using static SimpleExec.Command;

Console.WriteLine("Redirecting to dotnet CLI...");

// show the dotnet version at the end
var exitCode = 0;
var (standardOutput, standardError) = await ReadAsync(
    "dotnet",
    "--version",
    handleExitCode: code => (exitCode = code) == 0);

AnsiConsole.WriteLine(standardOutput);
AnsiConsole.WriteLine(standardError);

return exitCode;
