using Aula2.Infra.Repository;
using Aula2.Models.ResponseModel;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Aula2.Services
{
    public class MercadoLivreApiService
    {
        private OrdersRepository _ordersRepository;

        public MercadoLivreApiService()
        {
            _ordersRepository = new OrdersRepository();
        }

        public void BaixarPedidosMercadoLivre()
        {
            var listaPedidosMercadoLivre = ObterPedidosRecentes();

            RemoverPedidosJaGravados(listaPedidosMercadoLivre);

            foreach (var pedido in listaPedidosMercadoLivre.Pedidos)
            {
                var uf = ObterUfComprador(pedido.DadoComprador.Id);

                _ordersRepository.GravarPedido(pedido, uf);
            }
        }

        private static string ObterUfComprador(long idUsuario)
        {
            var client = new RestClient($"https://api.mercadolibre.com/users/{idUsuario}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer APP_USR-5876725707172758-070813-41f6f992996484838b634403e4a8a9a2-292025322");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Não foi possivel obter o estado do comprador");

            var enderecoUsuario = JsonConvert.DeserializeObject<UsuarioResponseModel>(response.Content);

            return enderecoUsuario.ObterUF();
        }

        private static OrderResponseModel ObterPedidosRecentes()
        {
            var client = new RestClient("https://api.mercadolibre.com/orders/search/recent?seller=292025322");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer APP_USR-5876725707172758-070813-41f6f992996484838b634403e4a8a9a2-292025322");

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Não foi possivel obter os pedidos na api do mercado livre.");

            var listaPedidosMercadoLivre = JsonConvert.DeserializeObject<OrderResponseModel>(response.Content);
            return listaPedidosMercadoLivre;
        }

        private void RemoverPedidosJaGravados(OrderResponseModel novosPedidos)
        {
            var pedidosExistentes = _ordersRepository.ListarTodosPedidos();
            var numerosPedidosExistentes = pedidosExistentes.Select(x => x.Order_number);

            novosPedidos.Pedidos.RemoveAll(pedido => numerosPedidosExistentes.Contains(pedido.NumeroPedido.ToString()));
        }
    }
}
