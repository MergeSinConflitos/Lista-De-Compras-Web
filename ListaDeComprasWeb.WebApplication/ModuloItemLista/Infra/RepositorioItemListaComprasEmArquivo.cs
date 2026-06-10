
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Infra;

public class RepositorioItemListaEmArquivo : RepositorioBaseEmArquivo<ItemListaCompras>, IRepositorioItemLista
{
    public RepositorioItemListaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<ItemListaCompras> CarregarRegistros()
    {
        return contexto.ItensLista;
    }
}