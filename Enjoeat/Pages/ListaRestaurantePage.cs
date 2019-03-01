using OpenQA.Selenium;
using System.Linq;

namespace Enjoeat.Pages
{
    public class ListaRestaurantePage
    {
        private readonly IWebDriver driver;

        public ListaRestaurantePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AcessaItem(string nome)
        {
            var itens = driver.FindElements(By.CssSelector(".place-info-box"));
            var alvo = itens.FirstOrDefault(x => x.Text.Contains(nome));
            alvo.Click();
        }

        public int ItemsQuantidade()
        {
            return driver.FindElements(By.CssSelector(".place-info-box")).Count;
        }
    }
}
