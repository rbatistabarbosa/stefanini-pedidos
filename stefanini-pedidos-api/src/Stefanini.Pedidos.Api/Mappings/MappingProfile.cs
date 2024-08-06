using Stefanini.Pedidos.Api.Dtos;
using Stefanini.Pedidos.Core.Models;
using AutoMapper;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ItensPedido.Sum(ip => ip.Quantidade * ip.Produto.Valor)));

        CreateMap<ItemPedido, ItemPedidoDto>()
            .ForMember(dest => dest.IdProduto, opt => opt.MapFrom(src => src.ProdutoId))
            .ForMember(dest => dest.NomeProduto, opt => opt.MapFrom(src => src.Produto.NomeProduto))
            .ForMember(dest => dest.ValorUnitario, opt => opt.MapFrom(src => src.Produto.Valor));
    }
}
