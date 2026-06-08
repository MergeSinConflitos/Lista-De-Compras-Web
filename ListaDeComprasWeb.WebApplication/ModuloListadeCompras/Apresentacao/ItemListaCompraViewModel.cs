using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Apresentacao;

public record ListarItemViewModel(
    string Id,
    string NomeProduto,
    string NomeCategoria,
    int Quantidade,
    decimal Preco
);

public record AdicionarItemViewModel(
    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido.")]
    string ProdutoId,

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade,

    string ListaId,    //  para saber a cual lista pertenece el item
    List<Produto>? Produtos  //  para el select
);

public record RemoverItemViewModel(
    string Id,
    string NomeProduto,
    int Quantidade,
    decimal Preco,
    string ListaId     //  para volver a la lista correcta después de remover
);

public record EditarItemViewModel(
    string Id,
    string NomeProduto,
    int Quantidade,
    decimal Preco,
    string ListaId,   //  para volver a la lista correcta después de editar

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int QuantidadeEditada
);
