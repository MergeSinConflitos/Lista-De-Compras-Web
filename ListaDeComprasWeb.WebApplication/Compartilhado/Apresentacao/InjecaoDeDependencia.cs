using System;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Apresentacao;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Apresentacao;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Apresentacao;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Apresentacao;
namespace ListaDeComprasWeb.WebApplication.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Localização das Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            config.AddProfile<CategoriaProfile>();
            config.AddProfile<ProdutoProfile>();
            config.AddProfile<ListaDeComprasProfile>();
            config.AddProfile<ItemListaProfile>();

        });
    }
}
