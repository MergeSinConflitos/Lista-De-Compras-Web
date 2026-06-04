using System;
using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApplication.ModuloCategoria.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaController : Controller
{
    IMapper mapeador;
    ServicoCategoria servicoCategoria;

    public CategoriaController(IMapper mapeador, ServicoCategoria servicoCategoria)
    {
        this.mapeador = mapeador;
        this.servicoCategoria = servicoCategoria;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCategoriasDtos> dtos = servicoCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = mapeador.Map<List<ListarCategoriasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel cadastrarVm = new CadastrarCategoriaViewModel(
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarCategoriaDto dto = mapeador.Map<CadastrarCategoriaDto>(cadastrarVm);

        Result resultado = servicoCategoria.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesCategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesCategoriaDto dto = resultado.Value;

        EditarCategoriaViewModel editarVm = mapeador.Map<EditarCategoriaViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarCategoriaDto dto = mapeador.Map<EditarCategoriaDto>(editarVm);

        Result resultado = servicoCategoria.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesCategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesCategoriaDto dto = resultado.Value;

        ExcluirCategoriaViewModel excluirVm = mapeador.Map<ExcluirCategoriaViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCategoriaViewModel excluirVm)
    {
        Result resultado = servicoCategoria.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}
