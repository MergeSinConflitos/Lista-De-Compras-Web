using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeComprasWeb.WebApplication.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Infra.Arquivos;

public sealed class ContextoJson
{
    private readonly string caminhoArquivo;

    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public List<Produto> Produtos { get; set; } = new List<Produto>();

    public List<ListaDeCompra> ListaDeCompra { get; set; } = new List<ListaDeCompra>();

    public ContextoJson()
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ClubeDaLeituraWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo = JsonSerializer
            .Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        Categorias = contextoSalvo.Categorias;
        Produtos = contextoSalvo.Produtos;
        ListaDeCompra = contextoSalvo.ListaDeCompra;
    }
}
