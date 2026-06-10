using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Aplicacao;
using ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Dominio;

using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApplication.ModuloListaDeCompras.Apresentacao;

public class ListaDeComprasController : Controller
{
    IMapper mapeador;
    ServicoListaDeCompras servicoListaCompras;

    public ListaDeComprasController(IMapper mapeador, ServicoListaDeCompras servicoListaCompras)
    {
        this.mapeador = mapeador;
        this.servicoListaCompras = servicoListaCompras;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasComprasDto> dtos = servicoListaCompras.SelecionarTodos();

        List<ListarListasDeComprasViewModel> listarVms = mapeador.Map<List<ListarListasDeComprasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarListaDeComprasViewModel cadastrarVm = new CadastrarListaDeComprasViewModel(string.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarListaDeComprasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarListaComprasDto dto = mapeador.Map<CadastrarListaComprasDto>(cadastrarVm);

        Result resultado = servicoListaCompras.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(cadastrarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesListaComprasDto> resultado = servicoListaCompras.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaComprasDto dto = resultado.Value;

        EditarListaDeComprasViewModel editarVm = mapeador.Map<EditarListaDeComprasViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarListaDeComprasViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarListaComprasDto dto = mapeador.Map<EditarListaComprasDto>(editarVm);

        Result resultado = servicoListaCompras.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(editarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesListaComprasDto> resultado = servicoListaCompras.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaComprasDto dto = resultado.Value;

        ExcluirListaDeComprasViewModel excluirVm = mapeador.Map<ExcluirListaDeComprasViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirListaDeComprasViewModel excluirVm)
    {
        Result resultado = servicoListaCompras.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
            TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Detalhes(string id)
    {
        Result<DetalhesListaComprasDto> resultadoDetalhes = servicoListaCompras.SelecionarPorId(id);

        if (resultadoDetalhes.IsFailed)
        {
            TempData.AddErrorMessage(resultadoDetalhes);
            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaComprasDto detalhes = resultadoDetalhes.Value;

        Result resultado = servicoListaCompras.Editar(
            new EditarListaComprasDto(detalhes.Id, detalhes.Nome, detalhes.Status)
        );

       if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}