using Dapper;
using JApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("RegistrarUsuario",
                    new
                    {
                        autenticacion.Nombre,
                        autenticacion.Correo,
                        autenticacion.NombreUsuario,
                        autenticacion.Contrasenna,
                        autenticacion.Estado
                    }
                );

                if(resultado > 0)
                    return Ok();
                else
                    return BadRequest();
            }
        }

        [HttpPost]
        [Route("Index")]
        public IActionResult Index(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ValidarInicioSesion",
                    new
                    {
                        autenticacion.NombreUsuario,
                        autenticacion.Contrasenna
                    }
                );

                if (resultado != null)
                    return Ok();
                else
                    return NotFound();
            }
        }

    }
}
