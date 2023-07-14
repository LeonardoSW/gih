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

        [HttpGet("Pedidos/BancoDados")]
        public IActionResult ObterPedidosSemResposta()
        {
            return Ok();
        }
    }
}