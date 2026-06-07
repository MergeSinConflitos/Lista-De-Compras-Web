using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Infra;

public class RepositorioListaDeCompraEmArquivo : RepositorioBaseEmArquivo<ListaDeCompra>, IRepositorioListaDeCompra
{
     public RepositorioListaDeCompraEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaDeCompra> CarregarRegistros()
    {
        return contexto.ListaDeCompra;
    }
}
