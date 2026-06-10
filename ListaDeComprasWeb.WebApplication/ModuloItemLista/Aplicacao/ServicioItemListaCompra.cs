using FluentResults;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloItemListaCompra.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;

namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Aplicacao;

public class ServicoItemListaCompras
{
    private readonly IRepositorioItemLista repositorioItemListaCompra;
    private readonly IRepositorioListaDeCompra repositorioListaDeCompra;
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoItemListaCompras(IRepositorioItemLista repositorioItemListaCompra, IRepositorioListaDeCompra repositorioListaDeCompra, IRepositorioProduto repositorioProduto)
    {
        this.repositorioItemListaCompra = repositorioItemListaCompra;
        this.repositorioListaDeCompra = repositorioListaDeCompra;
        this.repositorioProduto = repositorioProduto;
    }

    

    public Result Cadastrar(CadastrarItemListaDto dto)
    {
        Result<(ListaDeCompras Lista, Produto Produto)> resultadoRelacionamentos =
            SelecionarRelacionamentos(dto.ListaDeCompraId, dto.ProdutoId);

        if (resultadoRelacionamentos.IsFailed)
            return Result.Fail(resultadoRelacionamentos.Errors);

        if (ExisteProdutoNaLista(dto.ListaDeCompraId, dto.ProdutoId))
            return Falha(nameof(dto.ProdutoId), "Este produto já foi adicionado nesta lista.");

        
        ItemListaCompras novoItem = new ItemListaCompras(resultadoRelacionamentos.Value.Lista, resultadoRelacionamentos.Value.Produto, dto.Quantidade);

        Result resultadoValidacao = ValidarEntidade(novoItem);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao; 

        repositorioItemListaCompra.Cadastrar(novoItem);

        return Result.Ok().WithSuccess("Item adicionado com sucesso!");
    }

    public Result Excluir(string id)
    {
        ItemListaCompras? item = repositorioItemListaCompra.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");

        repositorioItemListaCompra.Excluir(id);

        return Result.Ok().WithSuccess("Item removido com sucesso!");
    }

    public List<ListarItensListaComprasDto> SelecionarTodosPorLista(string listaDeComprasId)
    {
        return repositorioItemListaCompra
            .Filtrar(i => i.ListaCompras.Id == listaDeComprasId)
            .Select(MapearParaListarDto)
            .ToList();
    }

    public Result<DetalhesItemListaDto> SelecionarPorId(string id)
    {
        ItemListaCompras? item = repositorioItemListaCompra.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");

        return Result.Ok(MapearParaDetalhesDto(item));
    }

    public Result<DetalhesListaComprasDto> SelecionarDetalhesLista(string id)
    {
        ListaDeCompras? lista = repositorioListaDeCompra.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");

        List<ItemListaCompras> itens = repositorioItemListaCompra.Filtrar(i => i.ListaCompras.Id == lista.Id);

        return Result.Ok(new DetalhesListaComprasDto(
            lista.Id,
            lista.Nome,
            lista.DataCriacao,
            lista.Status,
            itens.Count,    
            itens.Sum(i => i.CalcularSubtotal())
        ));
    }

    public List<OpcaoProdutoDto> SelecionarProdutos()
    {
        return repositorioProduto.SelecionarTodos()
            .Select(p => new OpcaoProdutoDto(
                p.Id,
                p.Nome,
                p.Categoria.Nome,
                p.Categoria.Cor,
                p.Unidade,
                p.Preco
            ))
            .ToList();
    }

    private Result<(ListaDeCompras Lista, Produto Produto)> SelecionarRelacionamentos(
        string id,
        string produtoId)
    {
        ListaDeCompras? lista = repositorioListaDeCompra.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail(new Error("Selecione uma lista válida.")
                .WithMetadata("Campo", nameof(id)));

        Produto? produto = repositorioProduto.SelecionarPorId(produtoId);

        if (produto == null)
            return Result.Fail(new Error("Selecione um produto válido.")
                .WithMetadata("Campo", nameof(produtoId)));

        return Result.Ok((lista, produto));
    }

    public List<ListarItensListaComprasDto> SelecionarItensDaLista(string id)
    {
        List<ItemListaCompras> itens = repositorioItemListaCompra.SelecionarTodos()
            .Where(i => i.ListaCompras.Id == id)
            .ToList();

        List<ListarItensListaComprasDto> dtos = new List<ListarItensListaComprasDto>();

        foreach (ItemListaCompras item in itens)
        {
            ListarItensListaComprasDto dto = new ListarItensListaComprasDto(
                item.Id,
                item.ListaCompras.Id,
                item.ListaCompras.Nome,
                item.Produto.Id,
                item.Produto.Nome,
                item.Produto.Categoria.Nome,
                item.Produto.Categoria.Cor,
                item.Produto.Unidade,
                item.Produto.Preco,
                item.CalcularSubtotal(),
                item.Quantidade
            );
            dtos.Add(dto);
        }

        return dtos;
    }

    private bool ExisteProdutoNaLista(string id, string produtoId)
    {
        return repositorioItemListaCompra.SelecionarTodos()
            .Any(i => i.ListaCompras.Id == id && i.Produto.Id == produtoId);
    }

    public List<Produto> SelecionarProdutosDisponiveis(string id)
    {
        List<Produto> todosProdutos = repositorioProduto.SelecionarTodos();

        List<ItemListaCompras> itensNaLista = repositorioItemListaCompra.SelecionarTodos()
            .Where(i => i.ListaCompras.Id == id)
            .ToList();

        List<Produto> produtosDisponiveis = new List<Produto>();

        foreach (Produto p in todosProdutos)
        {
            bool jaEstaNaLista = itensNaLista.Any(i => i.Produto.Id == p.Id);

            if (!jaEstaNaLista)
                produtosDisponiveis.Add(p);
        }

        return produtosDisponiveis;
    }

    private static Result ValidarEntidade(ItemListaCompras item)
    {
        List<string> erros = item.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);
        return Result.Fail(erro);
    }

    private static ListarItensListaComprasDto MapearParaListarDto(ItemListaCompras item)
    {
        return new ListarItensListaComprasDto(
            item.Id,
            item.ListaCompras.Id,
            item.ListaCompras.Nome,
            item.Produto.Id,
            item.Produto.Nome,
            item.Produto.Categoria.Nome,
            item.Produto.Categoria.Cor,
            item.Produto.Unidade,
            item.Produto.Preco,
            item.Quantidade,
            item.CalcularSubtotal()
        );
    }

    private static DetalhesItemListaDto MapearParaDetalhesDto(ItemListaCompras item)
    {
        return new DetalhesItemListaDto(
            item.Id,
            item.ListaCompras.Id,
            item.ListaCompras.Nome,
            item.Produto.Id,
            item.Produto.Nome,
            item.Produto.Categoria.Nome,
            item.Produto.Categoria.Cor,
            item.Produto.Unidade,
            item.Produto.Preco,
            item.Quantidade,
            item.CalcularSubtotal()
        );
    }  

    public record DetalhesListaItemDto(
    Guid Id,
    string Nome,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalEstimado
    );

    
}

