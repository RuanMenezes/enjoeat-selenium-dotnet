using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Enjoeat.Pages
{
    public class RestaurantePage
    {
        private readonly IWebDriver driver;

        public RestaurantePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool VerificaDetalhes(string texto)
        {
            bool found = false;

            while (!found)
            {
                var info = driver.FindElements(By.CssSelector(".content .box-solid"));
                var text = info[0].Text;

                if (text.Contains(texto))
                {
                    found = true;
                    return found;
                }
                Thread.Sleep(500);
            }

            return false;
        }

        public bool VerificaCarrinhoVazio()
        {
            var info = driver.FindElements(By.CssSelector(".content .box-solid"));
            return info.Last().Text.Contains("Seu carrinho está vazio!");
        }

        public void AdicionaProduto(string produtoNome)
        {
            var produtos = driver.FindElements(By.ClassName("menu-item-info-box"));
            var produtoEncontrado = produtos.FirstOrDefault(p => p.Text.Contains(produtoNome.ToUpper()));

            if (produtoEncontrado == null)
            {
                throw new System.ArgumentException("Erro ao buscar um produto pelo nome. Info:" + produtoNome );
            }

            produtoEncontrado.FindElement(By.LinkText("Adicionar")).Click();
            Thread.Sleep(1500);
        }

        public IWebElement Carrinho()
        {
            return driver.FindElement(By.Id("cart"));
        }

        public void FecharPedido()
        {
            driver.FindElement(By.CssSelector("a[href='/order']")).Click();
        }
    }
}
