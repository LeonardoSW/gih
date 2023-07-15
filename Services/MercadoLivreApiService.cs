using Aula2.Domain.Interfaces;
using Aula2.Infra.Repository;
using Aula2.Models.ResponseModel;
using Newtonsoft.Json;
using System.Net;

namespace Aula2.Services
{
    public class MercadoLivreApiService : IMercadoLivreApiService
    {
        private OrdersRepository _ordersRepository;
        private readonly HttpClient _httpClient;

        public MercadoLivreApiService(HttpClient httpClient)
        {
            _ordersRepository = new OrdersRepository();
            _httpClient = httpClient;
        }

        public async void BaixarPedidosMercadoLivre()
        {
            var listaPedidosMercadoLivre = await ObterPedidosRecentesAsync();

            RemoverPedidosJaGravados(listaPedidosMercadoLivre);

            foreach (var pedido in listaPedidosMercadoLivre.Pedidos)
            {
                var uf = await ObterUfCompradorAsync(pedido.NumeroPedido);
                _ordersRepository.GravarPedido(pedido, uf);

                var dadosEnvio = await _ordersRepository.ObterPrazoAsync(pedido.NumeroPedido);

                var mensagemPrazo = new MessageAfterSales(dadosEnvio.Shipping_min_days, dadosEnvio.Shipping_max_days, dadosEnvio.Ml_user_name, dadosEnvio.Ml_user_id, dadosEnvio.Ml_seller_id);
                await ResponderClienteAsync(pedido.NumeroPedido, mensagemPrazo);

                var mensagemInformativa = new MessageAfterSales(dadosEnvio.Ml_user_id, dadosEnvio.Ml_seller_id);
                await ResponderClienteAsync(pedido.NumeroPedido, mensagemInformativa);

                await _ordersRepository.MarcarPedidoRespondidoAsync(pedido.NumeroPedido);
            }
        }

        private async Task<string> ObterUfCompradorAsync(long orderNumber)
        {
            var response = await _httpClient.GetAsync($"orders/{orderNumber}/shipments");
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Não foi possivel obter o estado do comprador");

            var enderecoUsuario = JsonConvert.DeserializeObject<EnderecoPedidoResponseModel>(await response.Content.ReadAsStringAsync());

            return enderecoUsuario.DadoEndereco.DadosEstado.Uf;
        }

        private async Task<OrderResponseModel> ObterPedidosRecentesAsync()
        {
            var response = await _httpClient.GetAsync("orders/search/recent?seller=292025322");
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Não foi possivel obter os pedidos na api do mercado livre.");

            var listaPedidosMercadoLivre = JsonConvert.DeserializeObject<OrderResponseModel>(await response.Content.ReadAsStringAsync());
            return listaPedidosMercadoLivre;
        }

        private void RemoverPedidosJaGravados(OrderResponseModel novosPedidos)
        {
            var pedidosExistentes = _ordersRepository.ListarTodosPedidos();
            var numerosPedidosExistentes = pedidosExistentes.Select(x => x.Order_number);

            novosPedidos.Pedidos.RemoveAll(pedido => numerosPedidosExistentes.Contains(pedido.NumeroPedido.ToString()));
        }

        private async Task ResponderClienteAsync(long orderNumber, MessageAfterSales mensagem)
        {
            var response = await _httpClient.PostAsJsonAsync($"messages/packs/{orderNumber}/sellers/292025322?tag=post_sale", mensagem);
        }
    }
}
