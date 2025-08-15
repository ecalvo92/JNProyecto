using JProyecto.Models;
using JProyecto.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace JProyecto.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Sesiones]
    public class CarritoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        private readonly IUtilitarios _utilitarios;
        public CarritoController(IConfiguration configuration, IHttpClientFactory http, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _http = http;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        public IActionResult AgregarAlCarrito(long id)
        {
            using (var http = _http.CreateClient())
            {
                var carrito = new Carrito
                {
                    IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!),
                    IdProducto = id
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/RegistrarProductoCarrito", carrito).Result;

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

        [HttpGet]
        public IActionResult ConsultarCarrito()
        {
            var resultado = _utilitarios.ConsultarDatosCarrito();
            return View(resultado);
        }

        [HttpGet]
        public IActionResult EliminarProductoCarrito(long id)
        {
            using (var http = _http.CreateClient())
            {
                var carrito = new Carrito
                {
                    IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!),
                    IdProducto = id
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/EliminarProductoCarrito", carrito).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("ConsultarCarrito", "Carrito");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;

                    var datosCarrito = _utilitarios.ConsultarDatosCarrito();
                    return View("ConsultarCarrito", datosCarrito);
                }
            }
        }

        [HttpGet]
        public IActionResult ProcesarPagoCarrito()
        {
            using (var http = _http.CreateClient())
            {
                var carrito = new Carrito
                {
                    IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!)
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/ProcesarPagoCarrito", carrito).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("Principal", "Home");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;

                    var datosCarrito = _utilitarios.ConsultarDatosCarrito();
                    return View("ConsultarCarrito", datosCarrito);
                }
            }
        }

        [HttpGet]
        public IActionResult ConsultarFacturas()
        {
            using (var http = _http.CreateClient())
            {
                var carrito = new Carrito
                {
                    IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!)
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/ConsultarFacturas", carrito).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<List<Carrito>>>().Result;
                    return View(datos?.Contenido!);
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult ConsultarDetalleFactura(long id)
        {
            using (var http = _http.CreateClient())
            {
                var carrito = new Carrito
                {
                    IdMaestro = id
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/ConsultarDetalleFactura", carrito).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<List<Carrito>>>().Result;
                    return View(datos?.Contenido!);
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaEstandar>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

    }
}
