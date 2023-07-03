using Microsoft.AspNetCore.Mvc;

namespace Aula2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        public VendaController()
        {
        }

        [HttpDelete]
        public IActionResult ExcluirVenda()
        {
            return Ok();
        }
    }
}
