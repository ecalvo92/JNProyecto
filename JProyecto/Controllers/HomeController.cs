using Dapper;
using JProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JProyecto.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
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
        using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
        {
            var resultado = context.QueryFirstOrDefault<Autenticacion>("ValidarInicioSesion",
                new { autenticacion.NombreUsuario,
                      autenticacion.Contrasenna }
                );

            if (resultado != null)
                return RedirectToAction("Principal", "Home");

            ViewBag.Mensaje = "No se pudo autenticar";
            return View();
        }
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
        using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
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
                return RedirectToAction("Index", "Home");

            ViewBag.Mensaje = "No se pudo registrar";
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
