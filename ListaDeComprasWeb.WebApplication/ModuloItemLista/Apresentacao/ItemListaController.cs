using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApplication.ModuloItemLista.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloItemListaCompra.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApplication.ModuloItemLista.Apresentacao;

public class ItemListaController : Controller
{
    private readonly ServicoItemListaCompras servicoItemLista;
    private readonly ServicoListaDeCompras servicoListaDeCompras;
    private readonly IMapper mapeador;
    public ItemListaController(ServicoItemListaCompras servicoItemLista, ServicoListaDeCompras servicoListaDeCompras, IMapper mapeador)
    {
        this.servicoItemLista = servicoItemLista;
        this.servicoListaDeCompras = servicoListaDeCompras;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar(string id)
    {
        Result<DetalhesListaComprasDto> resultadoLista = servicoItemLista.SelecionarDetalhesLista(id);

        if (resultadoLista.IsFailed)
        {
            TempData.AddErrorMessage(resultadoLista);
            return RedirectToAction("Listar", "ListaDeCompras");
        }

        List<ListarItensListaComprasDto> dtos = servicoItemLista.SelecionarTodosPorLista(id);

        GerenciarItensListaViewModel gerenciarVm = new GerenciarItensListaViewModel(
            mapeador.Map<DetalhesItemListaViewModel>(resultadoLista.Value),
            mapeador.Map<List<ListarItemListaViewModel>>(dtos)
        );

        return View(gerenciarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar(string id)
    {
        Result<DetalhesListaComprasDto> resultadoLista = servicoItemLista.SelecionarDetalhesLista(id);

        if (resultadoLista.IsFailed)
        {
            TempData.AddErrorMessage(resultadoLista);
            return RedirectToAction("Listar", "ListaDeCompras");
        }

        CadastrarItemListaViewModel cadastrarVm = new CadastrarItemListaViewModel(
            id,
            string.Empty,   
            1,
            false,
            SelecionarProdutos()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarItemListaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Produtos = SelecionarProdutos() });

        CadastrarItemListaDto dto = mapeador.Map<CadastrarItemListaDto>(cadastrarVm);

        Result resultado = servicoItemLista.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(cadastrarVm with { Produtos = SelecionarProdutos()});
        }

        if (cadastrarVm.AdicionarOutro)
            return RedirectToAction(nameof(Cadastrar), new { id = cadastrarVm.ListaComprasId });

        return RedirectToAction(nameof(Listar), new { id = cadastrarVm.ListaComprasId });
    }

    [HttpGet]
    public ActionResult Excluir(string id)  
    {
        Result<DetalhesItemListaDto> resultado = servicoItemLista.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction("Listar", "ListaDeCompras");
        }

        ExcluirItemListaViewModel excluirVm = mapeador.Map<ExcluirItemListaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirItemListaViewModel excluirVm)
    {
        Result resultado = servicoItemLista.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar), new { id = excluirVm.ListaComprasId });
    }

    private List<OpcaoProdutoViewModel> SelecionarProdutos()
    {
        List<OpcaoProdutoDto> dtos = servicoItemLista.SelecionarProdutos();

        return mapeador.Map<List<OpcaoProdutoViewModel>>(dtos);
    }
}