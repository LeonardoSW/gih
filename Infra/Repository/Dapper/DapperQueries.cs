namespace Aula2.Infra.Repository.Dapper
{
    public static class DapperQueries
    {
        public static string BuscarPedidosNaoRespondidos
            => "select * from IntegrationMercadoLivre.dbo.orders where first_message = 0";
    }
}
