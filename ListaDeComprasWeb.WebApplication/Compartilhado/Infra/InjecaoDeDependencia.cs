using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;

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

    }
}
