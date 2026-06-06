using System;
using FluentResults;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    IRepositorioProduto repositorioProduto;
    IRepositorioCategoria repositorioCategoria;

    public ServicoProduto(IRepositorioProduto repositorioProduto, IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoria == null)
        {
            return Result.Fail("Categoria não encontarda");
        }

        if (ExisteProdutoComNomeNaCategoria(dto.Nome, dto.CategoriaId))
        {
            return Falha("Nome", "Já existe um produto com esse nome na categoria");
        }

        Produto novoProduto = new Produto(
            dto.Nome,
            dto.Unidade,
            dto.Preco,
            categoria!
        );

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok().WithSuccess("Produto cadastrado com sucesso");
    }

    public Result Editar(EditarProdutoDto dto)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoria == null)
        {
            return Result.Fail("Categoria não encontarda");
        }

        if (ExisteProdutoComNomeNaCategoria(dto.Nome, dto.CategoriaId, dto.Id))
        {
            return Falha("Nome", "Já existe um produto com esse nome na categoria");
        }

        Produto ProdutoAtualizado = new Produto(
            dto.Nome,
            dto.Unidade,
            dto.Preco,
            categoria!
        );

        repositorioProduto.Editar(dto.Id, ProdutoAtualizado);

        return Result.Ok().WithSuccess("Produto editado com sucesso");
    }

    public Result Excluir(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
        {
            return Result.Fail("Produto não encontrado");
        }

        repositorioProduto.Excluir(id);

        return Result.Ok().WithSuccess("Produto excluido com sucesso");

    }

    public List<ListarProdutosDtos> SelecionarTodos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos.Select(p => new ListarProdutosDtos
        (p.Id
        , p.Nome
        , p.Unidade
        , p.Preco
        , p.Categoria.Nome)).ToList();
    }


    public Result<DetalhesProdutoDto> SelecionarPorId(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
        {
            return Result.Fail("Produto não encontrado");
        }

        return new DetalhesProdutoDto(
              produto.Id,
              produto.Nome,
              produto.Unidade,
              produto.Preco,
              produto.Categoria.Id,
              produto.Categoria.Nome
          );


    }


    private bool ExisteProdutoComNomeNaCategoria(string nome, string categoria, string? idIgnorado = null)
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos.Any(p => p.Id != idIgnorado && string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase) && string.Equals(p.Categoria.Id, categoria, StringComparison.OrdinalIgnoreCase));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
