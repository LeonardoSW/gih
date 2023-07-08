using Aula2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aula2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private MercadoLivreApiService _mercadoLivreApiService;
        private OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _mercadoLivreApiService = new MercadoLivreApiService();
            _orderService = orderService;
        }

        [HttpGet("Pedidos")]
        public IActionResult ObterPedidos()
        {
            _orderService.ListarPedidosSemResposta();

            var response = _mercadoLivreApiService.ObterPedidosRecentes();

            if (response == null)
                return BadRequest("Não foi possível obter os pedidos recentes do Mercado Livre");


            return Ok(response);
        }


    }
}