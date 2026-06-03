using System;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;

public record ListarCategoriasDtos(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    string Cor
);

public record EditarCategoriaDto(
    string Id,
    string Nome,
    string Cor
);

public record DetalhesCategoriaDto(
    string Id,
    string Nome,
    string Cor
);

