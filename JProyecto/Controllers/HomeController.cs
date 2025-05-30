using JProyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace JProyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Autenticacion autenticacion)
    {
        ViewBag.Mensaje = "No se pudo autenticar";
        return View();

        //return RedirectToAction("Principal","Home");
    }


    public IActionResult Principal()
    {
        return View();
    }

}
