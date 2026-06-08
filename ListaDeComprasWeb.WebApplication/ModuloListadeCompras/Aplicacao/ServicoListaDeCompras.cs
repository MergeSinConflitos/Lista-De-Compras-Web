using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using FluentResults;


namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;


public class ServicoListaDeCompras
{
    private readonly IRepositorioListaDeCompra repositorioListaDeCompra;

    public ServicoListaDeCompras(IRepositorioListaDeCompra repositorioListaDeCompra)
    {
        this.repositorioListaDeCompra = repositorioListaDeCompra;
    }

    public Result Cadastrar(CadastrarListaComprasDto dto)
    {
        ListaDeCompra novaLista = new ListaDeCompra(dto.Nome);

        List<string> erros = novaLista.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioListaDeCompra.Cadastrar(novaLista);

        return Result.Ok().WithSuccess("Lista cadastrada com sucesso!");
    }

    public Result Editar(EditarListaComprasDto dto)
    {
        ListaDeCompra listaAtualizada = new ListaDeCompra(dto.Nome);

        List<string> erros = listaAtualizada.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        bool conseguiuEditar = repositorioListaDeCompra.Editar(dto.Id, listaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Lista não encontrada.");

        return Result.Ok().WithSuccess("Lista editada com sucesso!");
    }

    public Result Excluir(string id)
    {
        ListaDeCompra? lista = repositorioListaDeCompra.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");

        if (lista.Itens.Count > 0)
            return Result.Fail("Não é possível excluir uma lista que possui itens vinculados.");

        repositorioListaDeCompra.Excluir(id);

        return Result.Ok().WithSuccess("Lista excluída com sucesso!");
    }

    public List<ListarListasComprasDto> SelecionarTodos()
    {
        List<ListaDeCompra> listas = repositorioListaDeCompra.SelecionarTodos();

        List<ListarListasComprasDto> dtos = new List<ListarListasComprasDto>();

        foreach (ListaDeCompra l in listas)
        {
            ListarListasComprasDto dto = new ListarListasComprasDto(
                l.Id,
                l.Nome,
                l.DataCriacao,
                l.Status,
                l.Itens.Count,
                l.TotalGasto
            );
            dtos.Add(dto);
        }

        return dtos;
    }

    public Result<DetalhesListaComprasDto> SelecionarPorId(string id)
    {
        ListaDeCompra? lista = repositorioListaDeCompra.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");

        return Result.Ok(new DetalhesListaComprasDto(
            lista.Id,
            lista.Nome,
            lista.DataCriacao,
            lista.Status,
            lista.Itens.Count,
            lista.TotalGasto
        ));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);
        return Result.Fail(erro);
    }
}
