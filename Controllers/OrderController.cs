using Aula2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aula2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private MercadoLivreApiService _mercadoLivreApiService;

        public OrderController()
        {
            _mercadoLivreApiService = new MercadoLivreApiService();
        }

        [HttpGet("Pedidos")]
        public IActionResult ObterPedidos()
        {
            var response = _mercadoLivreApiService.ObterPedidosRecentes();

            if (response == null)
                return BadRequest("Não foi possível obter os pedidos recentes do Mercado Livre");


            return Ok(response);
        }
    }
}