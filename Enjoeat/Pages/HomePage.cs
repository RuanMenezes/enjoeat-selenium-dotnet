using OpenQA.Selenium;

namespace Enjoeat.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Acessa()
        {
            driver.Navigate().GoToUrl("https://enjoeat.herokuapp.com");
        }

        public void AcessaRestaurantes()
        {
            this.Acessa();
            driver.FindElement(By.CssSelector("a[href='/restaurants']")).Click();
        }
    }
}
