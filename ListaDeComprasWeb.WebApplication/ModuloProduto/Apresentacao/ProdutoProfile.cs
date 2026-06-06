using System;
using AutoMapper;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutosDtos, ListarProdutosViewModel>();
        CreateMap<ListarCategoriasDtos, OpcaoCategoriaViewModel>();

        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();

        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>()
               .ForCtorParam("Categorias", opt => opt.MapFrom(_ => new List<OpcaoCategoriaViewModel>()));

        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();

    }
}
