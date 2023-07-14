using Newtonsoft.Json;

namespace Aula2.Models.ResponseModel
{
    public class OrderResponseModel
    {
        [JsonProperty("results")]
        public List<Pedido> Pedidos { get; set; }
    }

    public class Pedido
    {
        [JsonProperty("id")]
        public long NumeroPedido { get; set; }

        [JsonProperty("date_created")]
        public DateTime DataCompra { get; set; }

        [JsonProperty("total_amount")]
        public decimal Preco { get; set; }

        [JsonProperty("buyer")]
        public UsuarioMercadoLivre DadoComprador { get; set; }

        [JsonProperty("seller")]
        public UsuarioMercadoLivre DadoVendedor { get; set; }
    }

    public class UsuarioMercadoLivre
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nickname")]
        public string Nome { get; set; }
    }

}
