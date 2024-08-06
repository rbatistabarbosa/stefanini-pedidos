using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Stefanini.Pedidos.Api.Dtos;
using Stefanini.Pedidos.Core.Models;
using Stefanini.Pedidos.Infrastructure.Repositories.Contracts;
using AutoMapper;
using Stefanini.Pedidos.Infrastructure.Repositories;
using System;

namespace Stefanini.Pedidos.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedido")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<ActionResult<PedidoDto>> Get(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                return NotFound(id);
            }
            return Ok(_mapper.Map<PedidoDto>(pedido));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoDto pedidoDto)
        {
            var pedido = new Pedido();
            pedido.NomeCliente = pedidoDto.NomeCliente;
            pedido.EmailCliente = pedidoDto.EmailCliente;
            pedido.Pago = pedidoDto.Pago;
            pedido.DataCriacao = DateTime.Now;

            foreach (var itemDto in pedidoDto.ItensPedido)
            {
                var produto = await _produtoRepository.GetByIdAsync(itemDto.IdProduto);
                if (produto != null)
                {
                    var itemPedido = new ItemPedido
                    {
                        Produto = produto,
                        Quantidade = itemDto.Quantidade
                    };
                    pedido.ItensPedido.Add(itemPedido);
                }
            }

            await _pedidoRepository.AddAsync(pedido);
            
            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }

        [HttpPut]
        [Route("/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PedidoDto pedidoDto)
        {
            if (id != pedidoDto.Id)
            {
                return BadRequest();
            }
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _pedidoRepository.UpdateAsync(pedido);
            return NoContent();
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _pedidoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}