using System;
using AutoMapper;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriasDtos, ListarCategoriasViewModel>();
        CreateMap<CadastrarCategoriaViewModel, CadastrarCategoriaDto>();
        CreateMap<EditarCategoriaViewModel, EditarCategoriaDto>();

        CreateMap<DetalhesCategoriaDto, EditarCategoriaViewModel>();
        CreateMap<DetalhesCategoriaDto, ExcluirCategoriaViewModel>();
    }
}
