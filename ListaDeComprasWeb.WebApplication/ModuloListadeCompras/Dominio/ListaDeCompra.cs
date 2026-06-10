using ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;

public class ListaDeCompras : EntidadeBase<ListaDeCompras>
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public StatusListaCompras Status { get; set; } = StatusListaCompras.Aberta;
    public List<ItemListaCompras> Itens { get; set; } = new List<ItemListaCompras>();

    public decimal TotalGasto
    {
        get
        {
            decimal totalGasto = 0;
            foreach (ItemListaCompras item in Itens)
                totalGasto += item.CalcularSubtotal(); 
            return totalGasto;
        }
    }

    public ListaDeCompras() { }

    public ListaDeCompras(string nome, DateTime dataCriacao, StatusListaCompras status = StatusListaCompras.Aberta)
    {
        Nome = nome;
        DataCriacao = dataCriacao;
        Status = status;
    }   

    public void AdicionarItem(Produto produto, decimal quantidade) 
    {
        ItemListaCompras item = new ItemListaCompras(this, produto, quantidade);
        Itens.Add(item);
    }

    public bool RemoverItem(string idItem)
    {
        foreach (ItemListaCompras item in Itens)
        {
            if (item.Id == idItem)
            {
                Itens.Remove(item);
                return true;
            }
        }
        return false;
    }

    public override void Atualizar(ListaDeCompras listaAtualizada)
    {
        Nome = listaAtualizada.Nome;
        Status = listaAtualizada.Status;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        return erros;
    }
}
