using AutoMapper;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Apresentacao;

public class ListaDeComprasProfile : Profile
{
    public ListaDeComprasProfile()
    {
        CreateMap<ListarListasComprasDto, ListarListasDeComprasViewModel>();

        CreateMap<CadastrarListaDeComprasViewModel, CadastrarListaComprasDto>();
        CreateMap<EditarListaDeComprasViewModel, EditarListaComprasDto>();

        CreateMap<DetalhesListaComprasDto, EditarListaDeComprasViewModel>();
        CreateMap<DetalhesListaComprasDto, ExcluirListaDeComprasViewModel>();
        CreateMap<DetalhesListaComprasDto, ListarListasDeComprasViewModel>();
    }
}
