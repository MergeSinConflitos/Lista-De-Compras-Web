using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Infra;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>, IRepositorioProduto
{
    public RepositorioProdutoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Produto> CarregarRegistros()
    {
        return contexto.Produtos;
    }
}
