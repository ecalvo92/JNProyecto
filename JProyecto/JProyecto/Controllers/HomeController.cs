using JProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace JProyecto.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _http;
    public HomeController(IConfiguration configuration, IHttpClientFactory http)
    {
        _configuration = configuration;
        _http = http;
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
        using (var http = _http.CreateClient())
        {
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
            var resultado = http.PostAsJsonAsync("api/Home/Index", autenticacion).Result;

            if (resultado.IsSuccessStatusCode)
            {
                return RedirectToAction("Principal", "Home");
            }
            else
            { 
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View();
            }                
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
        using (var http = _http.CreateClient())
        {
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
            var resultado = http.PostAsJsonAsync("api/Home/Registro", autenticacion).Result;

            if (resultado.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View();
            }
        }
    }

    #endregion

    #region Recuperar Acceso

    [HttpGet]
    public IActionResult RecuperarAcceso()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RecuperarAcceso(Autenticacion autenticacion)
    {
        using (var http = _http.CreateClient())
        {
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
            var resultado = http.PostAsJsonAsync("api/Home/RecuperarAcceso", autenticacion).Result;

            if (resultado.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View();
            }
        }
    }

    #endregion

    [HttpGet]
    public IActionResult Principal()
    {
        return View();
    }

}
