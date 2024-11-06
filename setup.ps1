# Skapa en ny lösning
dotnet new sln -n QuizMasterSolution

# Skapa kärnbiblioteket för spelets logik
dotnet new classlib -n QuizMaster.Core

# Skapa konsolapplikationen
dotnet new console -n QuizMaster.ConsoleApp

# Skapa testprojektet med xUnit
dotnet new xunit -n QuizMaster.Tests
dotnet add QuizMaster.Tests package Shouldly

# Lägg till projektreferenser
dotnet add QuizMaster.ConsoleApp reference QuizMaster.Core
dotnet add QuizMaster.Tests reference QuizMaster.Core

# Lägg till projekten till lösningen
dotnet sln QuizMasterSolution.sln add QuizMaster.Core/QuizMaster.Core.csproj
dotnet sln QuizMasterSolution.sln add QuizMaster.ConsoleApp/QuizMaster.ConsoleApp.csproj
dotnet sln QuizMasterSolution.sln add QuizMaster.Tests/QuizMaster.Tests.csproj
