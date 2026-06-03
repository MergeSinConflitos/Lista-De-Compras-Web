using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Infra;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Categoria> CarregarRegistros()
    {
        return contexto.Categorias;
    }
}
