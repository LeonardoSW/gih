using Newtonsoft.Json;

namespace Aula2.Models.ResponseModel
{
    public class EnderecoPedidoResponseModel
    {
        [JsonProperty("receiver_address")]
        public Endereco DadoEndereco { get; set; }
    }

    public class Endereco
    {
        [JsonProperty("state")]
        public Estado DadosEstado { get; set; }
    }

    public class Estado
    {
        [JsonProperty("id")]
        public string Uf { get; set; }
    }
}
