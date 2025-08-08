using JProyecto.Models;
using JProyecto.Services;
using Microsoft.AspNetCore.Mvc;

namespace JProyecto.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Sesiones]
    [Administradores]
    public class ProductoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        public ProductoController(IConfiguration configuration, IHttpClientFactory http)
        {
            _configuration = configuration;
            _http = http;
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.GetAsync("api/Productos/ConsultarProductos").Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<List<Producto>>>().Result;
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
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto producto, IFormFile ImagenProducto)
        {
            var ext = Path.GetExtension(ImagenProducto.FileName);
            if (!ext.Equals(".png", StringComparison.CurrentCultureIgnoreCase))
            {
                ViewBag.Mensaje = "La imagen debe ser un archivo PNG.";
                return View();
            }

            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Productos/RegistrarProducto", producto).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<Producto>>().Result;

                    string archivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productos", datos?.Contenido?.IdProducto + ext);
                    using var stream = new FileStream(archivo, FileMode.Create);
                    ImagenProducto.CopyTo(stream);

                    return RedirectToAction("ConsultarProductos", "Producto");
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
        public IActionResult ActualizarProducto(long q)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.GetAsync("api/Productos/ConsultarProducto?IdProducto=" + q).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<Producto>>().Result;
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

        [HttpPost]
        public IActionResult ActualizarProducto(Producto producto, IFormFile ImagenProducto)
        {
            if (ImagenProducto != null)
            {
                var ext = Path.GetExtension(ImagenProducto.FileName);
                if (!ext.Equals(".png", StringComparison.CurrentCultureIgnoreCase))
                {
                    ViewBag.Mensaje = "La imagen debe ser un archivo PNG.";
                    return View();
                }
            }

            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("api/Productos/ActualizarProducto", producto).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    if (ImagenProducto != null)
                    {
                        var ext = Path.GetExtension(ImagenProducto.FileName);
                        string archivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productos", producto.IdProducto + ext);
                        using var stream = new FileStream(archivo, FileMode.Create);
                        ImagenProducto.CopyTo(stream);
                    }

                    return RedirectToAction("ConsultarProductos", "Producto");
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
