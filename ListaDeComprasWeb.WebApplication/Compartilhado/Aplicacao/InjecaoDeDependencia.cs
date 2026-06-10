using System;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoListaDeCompras>();
        services.AddScoped<ServicoItemListaCompras>();

    }
}

