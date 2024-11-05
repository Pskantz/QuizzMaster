# Create the solution
dotnet new sln -n QuizzMasterSolution


# Create the core library project (for game logic)
dotnet new classlib -n QuizzMaster.Core
# Verkare inte behövas --> dotnet add HangmanGame.Core package System.Collections.Immutable


# Create the console application project
dotnet new console -n QuizzMaster.ConsoleApp


# Create the test project using xUnit
dotnet new xunit -n QuizzMaster.Tests
dotnet add QuizzMaster.Tests package Shouldly


# Add project references
dotnet add QuizzMaster.ConsoleApp reference QuizzMaster.Core
dotnet add QuizzMaster.Tests reference QuizzMaster.Core


# Add the projects to the solution
dotnet sln QuizzMasterSolution.sln add QuizzMaster.Core/QuizzMaster.Core.csproj
dotnet sln QuizzMasterSolution.sln add QuizzMaster.ConsoleApp/QuizzMaster.ConsoleApp.csproj
dotnet sln QuizzMasterSolution.sln add QuizzMaster.Tests/QuizzMaster.Tests.csproj
