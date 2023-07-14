using Aula2.Infra.Repository.Dapper;
using Aula2.Models.Entity;
using Aula2.Models.ResponseModel;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Aula2.Infra.Repository
{
    public class OrdersRepository
    {
        private readonly string _connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=IntegrationMercadoLivre;Integrated Security=True;TrustServerCertificate=true";

        public OrdersRepository() { }

        public List<OrderEntity> ListarPedidosNaoRespondidos()
        {
            var queryToExecute = DapperQueries.BuscarPedidosNaoRespondidos();
            var sqlConnection = new SqlConnection(_connectionString);

            return sqlConnection.Query<OrderEntity>(queryToExecute).ToList();
        }

        public List<OrderEntity> ListarTodosPedidos()
        {
            var queryToExecute = DapperQueries.BuscarTodosPedidos();
            var sqlConnection = new SqlConnection(_connectionString);

            return sqlConnection.Query<OrderEntity>(queryToExecute).ToList();
        }

        public void GravarPedido(Pedido pedido, string uf)
        {
            var queryToExecute = DapperQueries.InserirPedido(pedido, uf);
            var sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Execute(queryToExecute);
        }
    }
}
