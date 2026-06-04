using System;
using FluentResults;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoCategoria(IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome))
        {
            return Falha("Nome", "Já existe uma categoria com esse nome");
        }

        Categoria novaCategoria = new Categoria(dto.Nome, dto.Cor);

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok().WithSuccess("Categoria cadastrada com sucesso");
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome, dto.Id))
        {
            return Falha("Nome", "Já existe uma categoria com esse nome");
        }

        Categoria categoriaAtualizada = new Categoria(dto.Nome, dto.Cor);

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok().WithSuccess("Categoria editada com sucesso");
    }

    public Result Excluir(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        repositorioCategoria.Excluir(id);

        return Result.Ok().WithSuccess("Categoria excluida com sucesso");
    }

    public List<ListarCategoriasDtos> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias
            .Select(c => new ListarCategoriasDtos(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new DetalhesCategoriaDto(categoria.Id, categoria.Nome, categoria.Cor));
    }


    private bool ExisteCategoriaComNome(string nome, string? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias.Any(c => c.Id != idIgnorado && string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
