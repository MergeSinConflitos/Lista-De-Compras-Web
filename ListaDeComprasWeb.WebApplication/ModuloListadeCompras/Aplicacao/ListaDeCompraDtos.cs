using ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Aplicacao;

public record ListarListasComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalGasto
);

public record CadastrarListaComprasDto(
    string Nome
);

public record EditarListaComprasDto(
    string Id,
    string Nome,
    StatusListaCompras Status
);

public record DetalhesListaComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalGasto
);