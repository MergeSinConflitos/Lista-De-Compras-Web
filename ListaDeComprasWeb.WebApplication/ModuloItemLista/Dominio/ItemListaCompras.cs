using ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Dominio;

public class ItemListaCompras : EntidadeBase<ItemListaCompras>
{   
    public ListaDeCompras ListaCompras { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
    public decimal Quantidade { get; set; }

    public ItemListaCompras()
    {
        
    }
    public ItemListaCompras(ListaDeCompras listaCompras, Produto produto, decimal quantidade)
    {   
        ListaCompras = listaCompras;
        Produto = produto;
        Quantidade = quantidade;
        
    }
    
    public decimal CalcularSubtotal()
    {
        return Produto.Preco * Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Produto == null)
            erros.Add("O campo \"Produto\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A quantidade deve ser maior que zero.");

        return erros;
    }

    public override void Atualizar(ItemListaCompras entidadeAtualizada)
    {   
        ListaCompras = entidadeAtualizada.ListaCompras;
        Produto = entidadeAtualizada.Produto;
        Quantidade = entidadeAtualizada.Quantidade;
        
    }
}