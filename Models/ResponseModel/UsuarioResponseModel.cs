using Newtonsoft.Json;

namespace Aula2.Models.ResponseModel
{
    public class UsuarioResponseModel
    {
        [JsonProperty("address")]
        public Endereco DadoEndereco { get; set; }

        public string ObterUF()
        {
            return DadoEndereco.Estado?.Split('-').LastOrDefault();
        }
    }

    public class Endereco
    {
        [JsonProperty("state")]
        public string Estado { get; set; }
    }
}
