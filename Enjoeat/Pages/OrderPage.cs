using OpenQA.Selenium;

namespace Enjoeat.Pages
{
    public class OrderPage
    {

        private readonly IWebDriver driver;

        public OrderPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string ResumoPedido()
        {
            return driver.FindElement(By.CssSelector(".content .jumbotron")).Text;
        }
    }
}
