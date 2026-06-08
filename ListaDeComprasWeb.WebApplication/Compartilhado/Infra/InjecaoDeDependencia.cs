using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Infra;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Infra;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Infra;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmArquivo>();
        services.AddScoped<IRepositorioListaDeCompra, RepositorioListaDeCompraEmArquivo>();


    }
}
