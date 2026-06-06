using System;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Aplicacao;

public record OpcaoCategoriaDto(
    string Id,
    string Nome
);

public record ListarProdutosDtos(
    string Id,
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaNome
);

public record CadastrarProdutoDto(
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaId
);

public record EditarProdutoDto(
    string Id,
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaId
);

public record DetalhesProdutoDto(
    string Id,
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaId,
    string CategoriaNome
);
