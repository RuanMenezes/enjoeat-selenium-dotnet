using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Enjoeat.Base;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Enjoeat.Tests
{
    public class FinalizarPedidoTests : BaseTest
    {
        [SetUp]
        public void Before()
        {
            homePage.AcessaRestaurantes();
            listaRestaurantePage.AcessaItem("GREEN FOOD");
            restaurantePage.AdicionaProduto("Salada Ceasar");
            restaurantePage.FecharPedido();
        }

        [Test]
        public void DeveFinalizarPedidoPorCartao()
        {
            pedidosPage.AdicionaDadosCliente("Fernando Papito", "eu@papito.io");
            pedidosPage.AdicionaDadosEntrega("Avenida Paulista", "1500", "17 andar");
            pedidosPage.SelecionaOpcaoPagamento("Cartão de Débito");
            pedidosPage.Concluir();

            StringAssert.Contains(
                "Seu pedido foi recebido pelo restaurante. Prepare a mesa que a comida está chegando!",
                orderPage.ResumoPedido()
            );
        }
    }
}
