using Dapper;
using JApi.Models;
using JApi.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public ErrorController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [Route("CapturarError")]
        public IActionResult CapturarError()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var DescripcionError = ex!.Error.Message;
            var Origen = ex.Path;
            var IdUsuario = _utilitarios.ObtenerIdUsuario(User.Claims);

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("RegistrarError",
                    new
                    {
                        DescripcionError,
                        Origen,
                        IdUsuario
                    }
                );
            }

            return StatusCode(500, _utilitarios.RespuestaIncorrecta("Se presentó un error interno"));
        }
    }
}
