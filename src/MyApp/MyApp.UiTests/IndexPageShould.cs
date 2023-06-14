using System.Drawing;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit.Abstractions;

namespace MyApp.UiTests;

public class IndexPageShould
{
    private const string BaseUrl = "https://localhost:7173/";
    private readonly ITestOutputHelper _testOutputHelper;

    public IndexPageShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    [Trait("Category", "Index")]
    public async Task ContainTheCorrectHeadingText()
    {
        // Arrange
        using var driver = new ChromeDriver();
        const string expectedHeadingText = "BAD EXPECTATION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
        driver.Manage().Window.Size = new Size(480, 960);
        
        // Act
        driver.Navigate().GoToUrl(BaseUrl);
        Task.Delay(5000).Wait();

        var screenShot = driver.GetScreenshot();
        screenShot.SaveAsFile($"../../../ScreenShots/{nameof(IndexPageShould)}_{nameof(ContainTheCorrectHeadingText)}_{DateTime.Now.Date:yyyy-MM-dd}.png", ScreenshotImageFormat.Png);
        _testOutputHelper.WriteLine($"Screenshot saved to {nameof(IndexPageShould)}_{nameof(ContainTheCorrectHeadingText)}_{DateTime.Now.Date:yyyy-MM-dd}.png");

        // Assert
        var actualHeadingText = driver.FindElement(By.TagName("h1")).Text;
        actualHeadingText.Should().BeEquivalentTo(expectedHeadingText);

        _testOutputHelper.WriteLine($"Actual heading text: {actualHeadingText}");
        
        driver.Quit();
    }
}