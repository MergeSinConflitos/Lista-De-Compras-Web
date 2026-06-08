using ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Dominio;
using FluentResults;

namespace ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Aplicacao;


public class ServicoListaDeCompra
{
    private readonly IRepositorioListaDeCompra repositorioListaDeCompra;

    public ServicoListaDeCompra(IRepositorioListaDeCompra repositorioListaDeCompra)
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
}
