using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListadeCompras.Apresentacao;

public record ListarListasDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalGasto
);

public record CadastrarListaComprasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome
);

public record EditarListaComprasViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    StatusListaCompras Status
);

public record ExcluirListaComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status
);