# vs-csharp-selenium

## Description
Deomnstrates how to use Selenium with C# to automate UI testing via XUnit.

## Quick Start
To run both projects in Visual Studio:
  - Right-click the Web project and select `Debug > Start without Debugging`
  - For tests, click `Test > Test Explorer` and click `Run All Tests`

Or via the dotnet cli (from ./src/MyApp/):

  - `dotnet dev-certs https --trust`
  - `dotnet run --project .\MyApp.Web\MyApp.Web.csproj --launch-profile https`
  - `dotnet test`
