using Aula2.Domain.Interfaces;
using Aula2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aula2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IMercadoLivreApiService _mercadoLivreApiService;
        private OrderService _orderService;

        public OrderController(OrderService orderService, IMercadoLivreApiService mercadoLivreApiService)
        {
            _mercadoLivreApiService = mercadoLivreApiService;
            _orderService = orderService;
        }

        [HttpPost("Pedidos/MercadoLivre")]
        public IActionResult BaixarPedidosRecentes()
        {
            try
            {
                _mercadoLivreApiService.BaixarPedidosMercadoLivre();
                return Ok("Pedidos baixados com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}