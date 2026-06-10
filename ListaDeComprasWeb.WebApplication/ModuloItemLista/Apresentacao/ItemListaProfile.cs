using AutoMapper;
using ListaDeComprasWeb.WebApplication.ModuloItemListaCompra.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Apresentacao;
using static ListaDeComprasWeb.WebApplication.ModuloItemLista.Aplicacao.ServicoItemListaCompras;

namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Apresentacao;

public class ItemListaProfile : Profile
{
    public ItemListaProfile()
    {   
        CreateMap<OpcaoProdutoDto, OpcaoProdutoViewModel>();
        CreateMap<DetalhesListaItemDto, DetalhesItemListaViewModel>();
        CreateMap<ListarItensListaComprasDto, ListarItemListaViewModel>();
        CreateMap<CadastrarItemListaViewModel, CadastrarItemListaDto>();
        CreateMap<DetalhesItemListaDto, ExcluirItemListaViewModel>(); 
            
    }
}