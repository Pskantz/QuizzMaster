# Create the solution
dotnet new sln -n HangmanGameSolution


# Create the core library project (for game logic)
dotnet new classlib -n HangmanGame.Core
# Verkare inte behövas --> dotnet add HangmanGame.Core package System.Collections.Immutable


# Create the console application project
dotnet new console -n HangmanGame.ConsoleApp


# Create the test project using xUnit
dotnet new xunit -n HangmanGame.Tests
dotnet add HangmanGame.Tests package Shouldly


# Add project references
dotnet add HangmanGame.ConsoleApp reference HangmanGame.Core
dotnet add HangmanGame.Tests reference HangmanGame.Core


# Add the projects to the solution
dotnet sln HangmanGameSolution.sln add HangmanGame.Core/HangmanGame.Core.csproj
dotnet sln HangmanGameSolution.sln add HangmanGame.ConsoleApp/HangmanGame.ConsoleApp.csproj
dotnet sln HangmanGameSolution.sln add HangmanGame.Tests/HangmanGame.Tests.csproj
