using System;
using System.ComponentModel.DataAnnotations;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(50,MinimumLength =2,ErrorMessage ="O nome deve ter entre 2 e 50 caracteres")]
    string Nome,

    [Required(ErrorMessage ="Selecione uma cor válida")]
    string Cor
);

public record EditarCategoriaViewModel(
    string Id,

    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(50,MinimumLength =2,ErrorMessage ="O nome deve ter entre 2 e 50 caracteres")]
    string Nome,

    [Required(ErrorMessage ="Selecione uma cor válida")]
    string Cor
);

public record ExcluirCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);
