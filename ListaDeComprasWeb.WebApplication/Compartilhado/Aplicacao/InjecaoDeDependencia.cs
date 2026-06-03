using System;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
    }
}

