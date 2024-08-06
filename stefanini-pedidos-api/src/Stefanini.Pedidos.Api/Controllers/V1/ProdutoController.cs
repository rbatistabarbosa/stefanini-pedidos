using Microsoft.AspNetCore.Mvc;
using Stefanini.Pedidos.Api.Dtos;
using Stefanini.Pedidos.Infrastructure.Repositories;
using Stefanini.Pedidos.Infrastructure.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stefanini.Pedidos.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/produto")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("/")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
        {
            var pedidos = await _produtoRepository.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpPost]
        [Route("/populate")]
        public async Task<ActionResult> Populate()
        {
            await _produtoRepository.AddRandomProductsAsync(10);
            return NoContent();
        }
    }
}