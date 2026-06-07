using ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloItemListaCompras.Dominio;

public class ItemListaCompras : EntidadeBase<ItemListaCompras>
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }

    public decimal Preco
    {
        get
        {
            return Produto.Preco * Quantidade;
        }
    }

    public ItemListaCompras()
    {
        
    } 

    public ItemListaCompras(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
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
        Quantidade = entidadeAtualizada.Quantidade;
    }
}