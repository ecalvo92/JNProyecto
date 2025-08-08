using Dapper;
using JApi.Models;
using JApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public ProductosController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Query<Producto>("ConsultarProductos",
                    new
                    {
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No hay información registrada"));
            }
        }

        [HttpPost]
        [Route("RegistrarProducto")]
        [Authorize]
        public IActionResult RegistrarProducto(Producto producto)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Producto>("RegistrarProducto",
                    new
                    {
                        producto.Nombre,
                        producto.Descripcion,
                        producto.Precio,
                        producto.Inventario
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("La información del producto no fue registrada"));
            }
        }

        [HttpGet]
        [Route("ConsultarProducto")]
        public IActionResult ConsultarProducto(long IdProducto)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Producto>("ConsultarProducto",
                    new
                    {
                        IdProducto
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No hay información registrada"));
            }
        }

        [HttpPut]
        [Route("ActualizarProducto")]
        [Authorize]
        public IActionResult ActualizarProducto(Producto producto)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarProducto",
                    new
                    {
                        producto.IdProducto,
                        producto.Nombre,
                        producto.Descripcion,
                        producto.Precio,
                        producto.Inventario
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("La información del producto no fue actualizada"));
            }
        }

    }
}
