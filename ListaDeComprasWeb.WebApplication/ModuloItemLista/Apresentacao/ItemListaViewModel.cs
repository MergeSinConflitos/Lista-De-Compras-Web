using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;


namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Apresentacao;

public record OpcaoProdutoViewModel(
    string Id,
    string Nome,
    string CategoriaNome,
    string Unidade,
    decimal Preco
);

public record DetalhesItemListaViewModel(
    string Id,
    string Nome,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalEstimado
);

public record ListarItemListaViewModel(
    string Id,                  
    string ListaComprasId,      
    string ListaComprasNome,
    string ProdutoId,           
    string ProdutoNome,
    string CategoriaNome,
    string CategoriaCor,
    string Unidade,             
    decimal ProdutoPreco,              
    decimal Quantidade,
    decimal Subtotal
);

public record CadastrarItemListaViewModel(
    string ListaComprasId,      

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido.")]
    string ProdutoId,           

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor maior que 0.")]
    decimal Quantidade,

    bool AdicionarOutro,

    [ValidateNever]
    List<OpcaoProdutoViewModel> Produtos
);

public record ExcluirItemListaViewModel(
    string Id,                  
    string ListaComprasId,      
    string ListaComprasNome,
    string ProdutoId,           
    string ProdutoNome,
    string CategoriaNome,
    string CategoriaCor,
    string Unidade,             
    decimal ProdutoPreco,             
    decimal Quantidade,
    decimal Subtotal
);

public record GerenciarItensListaViewModel(
    DetalhesItemListaViewModel Lista,
    List<ListarItemListaViewModel> Itens
);