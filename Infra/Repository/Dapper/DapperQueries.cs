using Aula2.Models.ResponseModel;

namespace Aula2.Infra.Repository.Dapper
{
    public static class DapperQueries
    {
        public static string BuscarPedidosNaoRespondidos()
        {
            return "select * from IntegrationMercadoLivre.dbo.orders where first_message = 0";
        }

        public static string BuscarTodosPedidos()
        {
            return "select * from IntegrationMercadoLivre.dbo.orders";
        }

        public static string InserirPedido(Pedido pedido, string uf)
        {
            return @$"insert into IntegrationMercadoLivre.dbo.orders 
                     (order_number, 
                      purchase_date, 
                      price, 
                      first_message, 
                      ml_seller_id, 
                      ml_user_id, 
                      ml_user_name, 
                      ml_buyer_uf) 
                     values 
                     ('{pedido.NumeroPedido}', 
                      '{pedido.DataCompra:yyyy-MM-dd HH:mm:ss}', 
                      {pedido.Preco}, 
                      0,
                      '{pedido.DadoVendedor.Id}',
                      '{pedido.DadoComprador.Id}',
                      '{pedido.DadoComprador.Nome}',
                      '{uf}')";
        }

        public static string ObterPrazoPorPedido(long orderNumber)
        {
            return @$"select 
                        ml_user_id,
                        ml_seller_id,
                        ml_user_name,
                        shipping_min_days, 
                        shipping_max_days 
                      from orders as pedido
                      join shipping_states as prazos on (pedido.ml_buyer_uf = prazos.uf)
                      where order_number = {orderNumber}";
        }

        public static string MarcarPedidoRespondido(long orderNumber)
        {
            return $"update orders set first_message = 0 where order_number = {orderNumber}";
        }
    }
}
