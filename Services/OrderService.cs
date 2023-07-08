using Aula2.Infra.Repository;

namespace Aula2.Services
{
    public class OrderService
    {
        private OrdersRepository _orderRepository;

        public OrderService(OrdersRepository repository)
        {
            _orderRepository = repository;
        }

        public void ListarPedidosSemResposta()
        {
            var listaPedidos = _orderRepository.ListarPedidosNaoRespondidos();
        }
    }
}
