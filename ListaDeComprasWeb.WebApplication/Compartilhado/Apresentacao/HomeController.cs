using System;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
