using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; }
    public string Cor { get; set; }

    public Categoria(string nome, string cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public Categoria()
    {

    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

        Nome = categoriaAtualizada.Nome;
        Cor = categoriaAtualizada.Cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add("O campo \"Nome\" é obrigatório");
        }
        else if (Nome.Length < 2 || Nome.Length > 100)
        {
            erros.Add("O nome deve conter entre 2 e 100 caracteres");
        }

        if (string.IsNullOrWhiteSpace(Cor))
        {
            erros.Add("Selecione uma cor válida");
        }

        return erros;
    }
}
