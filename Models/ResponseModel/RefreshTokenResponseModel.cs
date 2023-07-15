using Newtonsoft.Json;

namespace Aula2.Models.ResponseModel
{
    public class RefreshTokenResponseModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
