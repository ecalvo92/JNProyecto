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
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public UsuarioController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario(long IdUsuario)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ConsultarUsuario",
                    new
                    {
                        IdUsuario
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));
            }
        }

        [HttpPut]
        [Route("ActualizarUsuario")]
        public IActionResult ActualizarUsuario(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarUsuario",
                    new
                    {
                        autenticacion.Identificacion,
                        autenticacion.Nombre,
                        autenticacion.Correo,
                        autenticacion.IdUsuario
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue actualizada"));
            }
        }

        [HttpPut]
        [Route("CambiarContrasenna")]
        public IActionResult CambiarContrasenna(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarContrasenna",
                    new
                    {
                        autenticacion.IdUsuario,
                        autenticacion.Contrasenna
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue actualizada"));
            }
        }

        [HttpGet]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Query<Autenticacion>("ConsultarUsuarios",
                    new
                    {
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No hay información registrada"));
            }
        }

        [HttpGet]
        [Route("ConsultarRoles")]
        public IActionResult ConsultarRoles()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Query<Rol>("ConsultarRoles",
                    new
                    {
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No hay información registrada"));
            }
        }

        [HttpPut]
        [Route("ActualizarDatosUsuario")]
        public IActionResult ActualizarDatosUsuario(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarDatosUsuario",
                    new
                    {
                        autenticacion.IdRol,
                        autenticacion.Estado,
                        autenticacion.IdUsuario
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("La información no fue actualizada"));
            }
        }        

    }
}
