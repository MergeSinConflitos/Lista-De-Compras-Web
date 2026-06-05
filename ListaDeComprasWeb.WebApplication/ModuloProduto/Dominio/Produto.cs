using System;
using ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

public class Produto : EntidadeBase<Produto>
{

    public string Nome { get; set; }
    public Categoria Categoria { get; set; }
    public string Unidade { get; set; }
    public decimal Preco { get; set; }

    public Produto(string nome, string unidade, decimal preco, Categoria categoria)
    {
        Nome = nome;
        Categoria = categoria;
        Unidade = unidade;
        Preco = preco;
    }

    public Produto()
    {

    }


    public override void Atualizar(Produto entidadeAtualizada)
    {
        Produto produtoAtualizado = (Produto)entidadeAtualizada;

        Nome = produtoAtualizado.Nome;
        Unidade = produtoAtualizado.Unidade;
        Preco = produtoAtualizado.Preco;
        Categoria = produtoAtualizado.Categoria;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add("O campo \"Nome\"é obrigatório");
        }
        else if (Nome.Length < 2 || Nome.Length > 100)
        {
            erros.Add("O nome deve ter entre 2 e 100 caracteres");
        }

        if (string.IsNullOrWhiteSpace(Unidade))
        {
            erros.Add("Selecione uma unidade de medida valida");
        }

        if (Preco < 0)
        {
            erros.Add("Informe um preço valido");
        }

        if (Categoria == null)
        {
            erros.Add("Selecione uma categoria");
        }

        return erros;
    }
}
