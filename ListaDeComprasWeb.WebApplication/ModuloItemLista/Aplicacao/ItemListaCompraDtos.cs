
namespace ListaDeComprasWeb.WebApplication.ModuloItemListaCompra.Aplicacao;

public record ListarItensListaComprasDto(
    string Id,
    string ListaDeCompraId,
    string ListaDeCompraNome,
    string ProdutoId,
    string ProdutoNome,
    string CategoriaNome,
    string CategoriaCor,
    string Unidade,
    decimal ProdutoPreco,
    decimal Preco,
    decimal Quantidade
    );

    public record CadastrarItemListaDto(
        string ListaDeCompraId,
        string ProdutoId,
        decimal Quantidade
    );

    public record DetalhesItemListaDto(
        string Id,
        string ListaDeCompraId,
        string ListaDeCompraNome,
        string ProdutoId,
        string ProdutoNome,
        string CategoriaNome,
        string CategoriaCor,
        string Unidade,
        decimal ProdutoPreco,
        decimal Quantidade,
        decimal Subtotal
    );

    public record OpcaoProdutoDto(
        string Id,
        string Nome,
        string CategoriaNome,
        string CategoriaCor,
        string Unidade,
        decimal Preco
    );
