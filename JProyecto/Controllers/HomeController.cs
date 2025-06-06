using Dapper;
using JProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JProyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #region Index

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

    #endregion

    #region Registro
    
    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registro(Autenticacion autenticacion)
    {
        using (var context = new SqlConnection("Server=EDUARDO; DataBase=JNDataBase; Integrated Security=True; TrustServerCertificate=True"))
        {
            var Estado = true;

            var resultado = context.Execute("RegistrarUsuario",
                new { autenticacion.Nombre, 
                      autenticacion.Correo, 
                      autenticacion.NombreUsuario, 
                      autenticacion.Contrasenna,
                      Estado }
                );

            if (resultado > 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "No se pudo autenticar";
            return View();
        }
    }

    #endregion

    [HttpGet]
    public IActionResult Principal()
    {
        return View();
    }

}
