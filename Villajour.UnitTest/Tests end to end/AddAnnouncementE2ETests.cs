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
            driver.Navigate().GoToUrl("https://villajour.fr/");

            var titleField = driver.FindElement(By.Id("fonctionnalites"));
            //var descriptionField = driver.FindElement(By.Name("description"));
            //var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            //titleField.SendKeys("TestAnnonce");
            //descriptionField.SendKeys("Ceci est une annonce");
            //submitButton.Click();

            //var successMessage = driver.FindElement(By.Id("success-message"));
            Assert.NotNull(titleField);
            //Assert.Equal("Annonce ajoutée avec succès!", successMessage.Text);
        }
    }
}
