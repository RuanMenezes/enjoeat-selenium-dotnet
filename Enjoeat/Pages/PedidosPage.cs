using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enjoeat.Pages
{
    public class PedidosPage
    {
        private readonly IWebDriver driver;

        public PedidosPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AdicionaDadosCliente(string nome, string email)
        {
            IWebElement campoNome = driver.FindElement(By.CssSelector("input[formcontrolname=name]"));
            IWebElement campoEmail = driver.FindElement(By.CssSelector("input[formcontrolname=email]"));
            IWebElement campoConfirmaEmail = driver.FindElement(By.CssSelector("input[formcontrolname=emailConfirmation]"));

            campoNome.SendKeys(nome);
            campoEmail.SendKeys(email);
            campoConfirmaEmail.SendKeys(email);
        }

        public void AdicionaDadosEntrega(string endereco, string numero, string complemento)
        {
            var campoEndereco = driver.FindElement(By.CssSelector("input[formcontrolname=address]"));
            var campoNumero = driver.FindElement(By.CssSelector("input[formcontrolname=number]"));
            var campoComplemento = driver.FindElement(By.CssSelector("input[formcontrolname=optionalAddress]"));

            campoEndereco.SendKeys(endereco);
            campoNumero.SendKeys(numero);
            campoComplemento.SendKeys(complemento);
        }

        public void SelecionaOpcaoPagamento(string opcao)
        {
            var divsRadio = driver.FindElements(By.CssSelector("mt-radio[formcontrolname=paymentOption] div"));
            var divOpcao = divsRadio.FirstOrDefault(x => x.Text.Contains(opcao));

            if (divOpcao == null)
            {
                throw new System.ArgumentException(String.Format("Erro ao buscar uma opção de pagamento. Info: {0}", opcao));
            }

            divOpcao.FindElement(By.CssSelector("div")).Click();
        }

        public void Concluir()
        {
            driver.FindElement(By.ClassName("btn-success")).Click();
        }

    }
}
