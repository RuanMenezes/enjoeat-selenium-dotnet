using Enjoeat.Base;
using NUnit.Framework;
using System.Threading;

namespace Enjoeat.Tests
{
    public class CarrinhoTests : BaseTest
    {
        [SetUp]
        public void Before()
        {
            homePage.AcessaRestaurantes();
            listaRestaurantePage.AcessaItem("BURGER HOUSE");
        }

        [Test]
        public void CarrinhoDeveEstarVazio()
        {
            Assert.IsTrue(restaurantePage.VerificaCarrinhoVazio());
        }

        [Test]
        public void DeveAdicionarItem()
        {
            string produto = "Batatas Fritas";
            restaurantePage.AdicionaProduto(produto);
            StringAssert.Contains(produto, restaurantePage.Carrinho().Text);
        }

        [Test]
        public void DeveAdicionarVariosItens()
        {
            var produtos = new[]
            {
                new { Name = "Classic Burger", Price = 18.50 },
                new { Name = "Batatas Fritas", Price = 5.50 },
                new { Name = "Refrigerante", Price = 4.50 }
            };

            // simulando um bug
            var total = 1.00;

            foreach (var p in produtos)
            {
                restaurantePage.AdicionaProduto(p.Name);
                StringAssert.Contains(p.Name, restaurantePage.Carrinho().Text);
                total += p.Price;
            }

            string totalString = total.ToString("0.00").Replace(".", ",");

            StringAssert.Contains(totalString, restaurantePage.Carrinho().Text);

        }

    }
}
