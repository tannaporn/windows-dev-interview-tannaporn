# Windows Developer Interview

A .NET 8 WPF application containing solutions to a set of programming interview exercises, with a simple desktop interface for trying each solution.

## Exercises

- Bracket validation
- Natural string sorting
- Autocomplete matching
- Roman numeral conversion
- Descending digit sorting
- Tribonacci sequence generation

## Requirements

- Windows
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Run the application

```powershell
dotnet run --project windows-dev-interview-tannaporn.csproj
```

Enter values in the application and use the corresponding action button to see the result. The **Run tests** button runs a small set of examples inside the UI.

## Run the automated tests

Run the full test suite:

```powershell
dotnet test windows-dev-interview-tannaporn.sln
```

Run a specific test method by filtering on its fully qualified name:

```powershell
dotnet test windows-dev-interview-tannaporn.sln --filter "FullyQualifiedName=Tests.InterviewTests.Brackets"
```

Run a group of related tests by matching part of the test name. For example, this runs both `RomanRoundTrip` and `RomanInvalid`:

```powershell
dotnet test windows-dev-interview-tannaporn.sln --filter "FullyQualifiedName~Roman"
```

Use `dotnet test windows-dev-interview-tannaporn.sln --list-tests` to see all available test names that can be used with `--filter`.

## Project structure

```text
Assets/                                  Application icons
Services/                                Service interface and implementation
Utils/                                   Exercise logic and natural-sort comparer
Views/                                   WPF window and event handlers
Tests/WindowsDeveloperInterview.Tests/   xUnit tests
```
