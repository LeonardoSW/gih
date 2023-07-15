using Aula2.Models.ResponseModel;
using Newtonsoft.Json;
using RestSharp;

namespace Aula2.Infra.DelegateHandlers
{
    public class MercadoLivreAccessTokenHandler : DelegatingHandler
    {

        private readonly string RefreshToken = "TG-64b2da550330840001cbb31e-292025322";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://api.mercadolibre.com/oauth/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", "5876725707172758");
            request.AddParameter("client_secret", "NP0KT2GeNBYBSZKWQVCTM23xh8I57mAE");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", RefreshToken);
            var token = client.Execute(request);

            var objResponse = JsonConvert.DeserializeObject<RefreshTokenResponseModel>(token.Content);

            req.Headers.Add("Authorization", $"Bearer {objResponse.Token}");

            var response = await base.SendAsync(req, cancellationToken);

            return response;
        }
    }
}
