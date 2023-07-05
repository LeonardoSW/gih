using Aula2.Models.ResponseModel;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Aula2.Services
{
    public class MercadoLivreApiService
    {
        public MercadoLivreApiService()
        {
        }

        public OrderResponseModel ObterPedidosRecentes()
        {
            var client = new RestClient("https://api.mercadolibre.com/orders/search/recent?seller=292025322");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer APP_USR-5876725707172758-070517-76eb4f8e3b78fed96bf79da10000c493-292025322");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<OrderResponseModel>(response.Content);
            else
                return null;
        }
    }
}
