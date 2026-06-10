using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using FluentResults;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Dominio;
using Microsoft.AspNetCore.Mvc;


namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;


public class ServicoListaDeCompras : Controller
{
    private readonly IRepositorioListaDeCompra repositorioListaDeCompra;
    private readonly IRepositorioItemLista repositorioItemListaCompra;    

    public ServicoListaDeCompras(IRepositorioListaDeCompra repositorioListaDeCompra, IRepositorioItemLista repositorioItemListaCompra)
    {
        this.repositorioListaDeCompra = repositorioListaDeCompra;
        this.repositorioItemListaCompra = repositorioItemListaCompra;
    }

    public Result Cadastrar(CadastrarListaComprasDto dto)
    {
        ListaDeCompras novaLista = new ListaDeCompras(dto.Nome, DateTime.Now);

        Result resultadoValidacao = ValidarEntidade(novaLista);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioListaDeCompra.Cadastrar(novaLista);

        return Result.Ok().WithSuccess("Lista cadastrada com sucesso!");
    }

    public Result Editar(EditarListaComprasDto dto)
    {
        ListaDeCompras? lista = repositorioListaDeCompra.SelecionarPorId(dto.Id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");

        ListaDeCompras listaAtualizada = new ListaDeCompras(dto.Nome, lista.DataCriacao, dto.Status);
       
        Result resultadoValidacao = ValidarEntidade(listaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioListaDeCompra.Editar(dto.Id, listaAtualizada);

        return Result.Ok().WithSuccess("Lista editada com sucesso!");
    }

    public Result Excluir(string id)
    {
        ListaDeCompras? lista = repositorioListaDeCompra.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");

        List<ItemListaCompras> itensDaLista = SelecionarItensDaLista(id);

        foreach (ItemListaCompras item in itensDaLista)
            repositorioItemListaCompra.Excluir(item.Id);

        repositorioListaDeCompra.Excluir(id);

        return Result.Ok().WithSuccess("Lista excluída com sucesso!");
    }

    public List<ListarListasComprasDto> SelecionarTodos()
    {
        return repositorioListaDeCompra
            .SelecionarTodos()
            .Select(l =>
            {
                List<ItemListaCompras> itens = SelecionarItensDaLista(l.Id);

                return new ListarListasComprasDto(
                    l.Id,
                    l.Nome,
                    l.DataCriacao,
                    l.Status,
                    itens.Count,
                    itens.Sum(i => i.CalcularSubtotal())
                );
            })
            .ToList();
    }

    public Result<DetalhesListaComprasDto> SelecionarPorId(string id)
    {
        ListaDeCompras? lista = repositorioListaDeCompra.SelecionarPorId(id);

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

    private List<ItemListaCompras> SelecionarItensDaLista(string listaId)
    {
        return repositorioItemListaCompra.Filtrar(i => i.ListaCompras.Id == listaId);
    }

    private static Result ValidarEntidade(ListaDeCompras lista)
    {
        List<string> erros = lista.Validar();

        if (erros.Count == 0)
            return Result.Ok();
        
        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
}
