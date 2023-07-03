using Aula2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aula2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        public static string ListaProdutos { get; set; } = "Escova de Dente; Iphone 7; Xícara; Pen Drive";

        public ProdutoController()
        {
        }

        [HttpGet("Obter")]
        public IActionResult ObterProduto([FromQuery] int idProduto)
        {
            bool sucesso = false;

            string resultado = ObterProdutoMetod(idProduto);

            if (resultado != "Produto não encontrado.")
            {
                sucesso = true;
            }


            if (sucesso == true)
            {
                return Ok("Produto obtido com sucesso" + ": " + resultado);
            }
            else
            {
                return BadRequest("Produto não pode ser obtido.");
            }
        }

        [HttpGet("Obter/Todos")]
        public IActionResult ObterTodosProdutos()
        {
            return Ok(ListaProdutos);
        }

        [HttpPost("Inserir")]
        public IActionResult InserirProduto([FromBody] ProdutoInputModel produto)
        {
            ListaProdutos = ListaProdutos + "; " + produto.NomeProduto + " " + produto.Marca;

            return Ok("Produto foi inserido");
        }

        [HttpDelete("ApagarTudo")]
        public IActionResult RemoverTudo()
        {
            ListaProdutos = "";
            return Ok("Produtos removidos");
        }


        private string ObterProdutoMetod(int id)
        {

            if (id == 1)
            {
                return "Bicicleta Caloi";
            }
            if (id == 2)
            {
                return "Notebook Acer";
            }
            if (id == 3)
            {
                return "Peteca";
            }

            return "Produto não encontrado.";
        }
    }
}