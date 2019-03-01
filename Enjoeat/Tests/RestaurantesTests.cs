using Enjoeat.Base;
using NUnit.Framework;

namespace Enjoeat.Tests
{
    public class RestaurantesTests : BaseTest
    {

        [Test]
        public void DeveAbrirAListaDeRestaurantes()
        {
            homePage.AcessaRestaurantes();

            Assert.IsTrue(
                driver.PageSource.Contains("Todos os Restaurantes"),
                "Erro ao verificar se estou na lista de restaurantes :("
            );
        }

        [Test]
        public void VerificaItens()
        {
            homePage.AcessaRestaurantes();
            Assert.AreEqual(6, listaRestaurantePage.ItemsQuantidade());
        }

        [Test]
        public void DeveExibirCategoria()
        {
            homePage.AcessaRestaurantes();
            listaRestaurantePage.AcessaItem("BURGER HOUSE");
            Assert.IsTrue(restaurantePage.VerificaDetalhes("Hamburgers"));
        }
    }
}