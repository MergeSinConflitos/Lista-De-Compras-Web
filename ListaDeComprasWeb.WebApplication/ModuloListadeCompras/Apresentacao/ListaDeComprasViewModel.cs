using System.ComponentModel.DataAnnotations;

using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Apresentacao;

public record ListarListasDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalGasto
);

public record CadastrarListaDeComprasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome
);

public record EditarListaDeComprasViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    StatusListaCompras Status
);

public record ExcluirListaDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status
);

public record DetalhesListaComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,        
    decimal TotalGasto,  
    List<ListarItemViewModel> Itens 
);