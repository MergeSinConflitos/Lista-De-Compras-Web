using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Apresentacao;

public record OpcaoCategoriaViewModel(
    string Id,
    string Nome
);

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaNome
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido.")]
    [StringLength(100,MinimumLength =2, ErrorMessage ="O campo \"Nome\"deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Unidade De Medida\"deve ser preenchido")]
    string Unidade,

    [Range(1,int.MaxValue,ErrorMessage ="O preço deve ser maior que 0")]
    decimal Preco,

    [Required(ErrorMessage ="Selecione uma categoria valida")]
    string CategoriaId,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias

);

public record EditarProdutoViewModel(
    string Id,

    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido.")]
    [StringLength(100,MinimumLength =2, ErrorMessage ="O campo \"Nome\"deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Unidade De Medida\"deve ser preenchido")]
    string Unidade,

    [Range(1,int.MaxValue,ErrorMessage ="O preço deve ser maior que 0")]
    decimal Preco,

    [Required(ErrorMessage ="Selecione uma categoria valida")]
    string CategoriaId,


    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string Unidade,
    decimal Preco,
    string CategoriaNome
);