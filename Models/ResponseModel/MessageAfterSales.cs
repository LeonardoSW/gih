using System.Text.Json.Serialization;

namespace Aula2.Models.ResponseModel
{
    public class MessageAfterSales
    {
        [JsonPropertyName("from")]
        public UserSend De { get; set; }

        [JsonPropertyName("to")]
        public UserSend Para { get; set; }

        [JsonPropertyName("text")]
        public string Mensagem { get; set; }

        public MessageAfterSales(int diaMinimo, int diaMaximo, string nomeComprador, string idComprador, string idVendedor)
        {
            De = new UserSend(idVendedor);
            Para = new UserSend(idComprador);
            Mensagem = MensagemPrazo(diaMinimo, diaMaximo, nomeComprador);
        }

        public MessageAfterSales(string idComprador, string idVendedor)
        {
            De = new UserSend(idVendedor);
            Para = new UserSend(idComprador);
            Mensagem = MensagemInformativa();
        }

        private static string MensagemPrazo(int diaMinimo, int diaMaximo, string nomeComprador)
        {
            return @$"Olá {nomeComprador}, espero que esteja tudo bem aí! 
Vou agilizar seu envio para receber o quanto antes, vamos enviar por aqui seu código de rastreamento.
O envio é internacional com FRETE GRÁTIS, sem nenhum custo adicional.
Com prazo de entrega de {diaMinimo} a {diaMaximo} dias corridos, entregue pelos CORREIOS no seu endereço.";
        }

        private static string MensagemInformativa()
        {
            return @$"Nossa negociação vai ser totalmente SEGURA. Pois seu pagamento ficará retido com o mercado livre, e será liberado somente quando você informar que recebeu do produto.
Qualquer dúvida estou a disposição.
Atenciosamente,
Kaori";
        }
    }

    public class UserSend
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        public UserSend(string userId)
        {
            UserId = userId;
        }
    }
}
