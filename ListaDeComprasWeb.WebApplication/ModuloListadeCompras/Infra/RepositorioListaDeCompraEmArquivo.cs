using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;

using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Infra;

public class RepositorioListaDeCompraEmArquivo : RepositorioBaseEmArquivo<ListaDeCompras>, IRepositorioListaDeCompra
{
    public RepositorioListaDeCompraEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaDeCompras> CarregarRegistros()
    {
        return contexto.ListaDeCompra;
    }
}
