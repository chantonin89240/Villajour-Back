using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class SeleniumTests
{
    [Fact]
    public void TestAddAnnouncement()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl("https://localhost:5001/announcements/add");

            var titleField = driver.FindElement(By.Name("title"));
            var descriptionField = driver.FindElement(By.Name("description"));
            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            titleField.SendKeys("TestAnnonce");
            descriptionField.SendKeys("Ceci est une annonce");
            submitButton.Click();

            var successMessage = driver.FindElement(By.Id("success-message"));
            Assert.NotNull(successMessage);
            Assert.Equal("Annonce ajoutée avec succès!", successMessage.Text);
        }
    }
}
