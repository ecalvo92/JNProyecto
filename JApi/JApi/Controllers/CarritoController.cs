using Dapper;
using JApi.Models;
using JApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public CarritoController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Route("RegistrarProductoCarrito")]
        public IActionResult RegistrarProductoCarrito(Carrito carrito)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("RegistrarProductoCarrito",
                    new
                    {
                        carrito.IdUsuario,
                        carrito.IdProducto,
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("El producto no fue agregado a su carrito"));
            }
        }

        [HttpPost]
        [Route("ConsultarCarrito")]
        public IActionResult ConsultarCarrito(Carrito carrito)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Query<Carrito>("ConsultarCarrito",
                    new
                    {
                        carrito.IdUsuario
                    });

                if (resultado.Any())
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No tiene productos registrados en su carrito"));
            }
        }

        [HttpPost]
        [Route("EliminarProductoCarrito")]
        public IActionResult EliminarProductoCarrito(Carrito carrito)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("EliminarProductoCarrito",
                    new
                    {
                        carrito.IdProducto,
                        carrito.IdUsuario
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("El producto no fue eliminado de su carrito"));
            }
        }

        



    }
}
