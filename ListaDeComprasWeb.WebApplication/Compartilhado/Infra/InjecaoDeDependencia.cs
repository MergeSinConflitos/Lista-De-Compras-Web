using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Infra;

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

    }
}
