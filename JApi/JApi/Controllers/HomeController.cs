using Dapper;
using JApi.Models;
using JApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace JApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public HomeController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("RegistrarUsuario",
                    new {   autenticacion.Nombre,
                            autenticacion.Correo,
                            autenticacion.Identificacion,
                            autenticacion.Contrasenna,
                            autenticacion.Estado    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue registrada"));
            }
        }

        [HttpPost]
        [Route("Index")]
        public IActionResult Index(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ValidarInicioSesion",
                    new {   autenticacion.Correo,
                            autenticacion.Contrasenna   });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));                
            }
        }

        [HttpPost]
        [Route("RecuperarAcceso")]
        public IActionResult RecuperarAcceso(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ValidarCorreo",
                    new {   autenticacion.Correo    });

                if (resultado != null)
                {
                    var Contrasenna = _utilitarios.GenerarContrasenna(10);

                    var resultadoActualizacion = context.Execute("ActualizarContrasenna",
                        new {   resultado.IdUsuario,
                                Contrasenna });

                    if (resultadoActualizacion > 0)
                    {
                        _utilitarios.EnviarCorreo(resultado.Correo!, "Recuperación de Acceso", Contrasenna);
                        return Ok(_utilitarios.RespuestaCorrecta(null));
                    }
                }

                return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));
            }
        }

    }
}
