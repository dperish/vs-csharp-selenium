using System.Drawing;                                                          // There are some hidden usings imported via globals in Usings.cs       
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit.Abstractions;

namespace MyApp.UiTests;

public class IndexPageShould
{
    private const string BaseUrl = "https://localhost:7173/";                  // Any module level variables should be marked const when possible

    private readonly ITestOutputHelper _testOutputHelper;                      // This will be dependency injected by xUnit, [CTRL] + [.] can auto gen the ctor, arg, assignments

    public IndexPageShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]                                                                     // Fact attributes are paramless, [Theory] attr would allow for test method args
    [Trait("Category", "Index")]                                               // Traits are a way to group tests, can be used to filter tests in test explorer
    public async Task ContainTheCorrectHeadingText()
    {
        // Arrange
        using var driver = new ChromeDriver();                                 // This is new-school C# syntax for using a disposable object, it will be disposed at the end of the method scope
        const string expectedHeadingText = "Welcome";
        driver.Manage().Window.Size = new Size(480, 960);
        
        // Act
        driver.Navigate().GoToUrl(BaseUrl);
        await Task.Delay(1000);

        var screenShot = driver.GetScreenshot();
        var fileName = $"{nameof(IndexPageShould)}_{nameof(ContainTheCorrectHeadingText)}_{DateTime.Now.Date:yyyy-MM-dd}.png";
        screenShot.SaveAsFile($"../../../ScreenShots/{fileName}", ScreenshotImageFormat.Png);
        _testOutputHelper.WriteLine($"Screenshot saved to {nameof(IndexPageShould)}_{nameof(ContainTheCorrectHeadingText)}_{DateTime.Now.Date:yyyy-MM-dd}.png");

        // Assert                                                              // Find element is using an extension method we created in /Extensions/WebDriverExtensions.cs
        var actualHeadingText = driver.FindElement(By.TagName("h1"), TimeSpan.FromSeconds(1)).Text;
        
        _testOutputHelper.WriteLine($"Actual heading text: {actualHeadingText}");

        actualHeadingText.Should().BeEquivalentTo(expectedHeadingText);        // Should() is another extension method, this one from FluentAssertions nuget package

        driver.Quit();                                                         
    }
}