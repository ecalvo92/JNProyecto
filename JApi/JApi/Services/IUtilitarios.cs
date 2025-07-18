using JApi.Models;
using System.Security.Claims;

namespace JApi.Services
{
    public interface IUtilitarios
    {
        RespuestaEstandar RespuestaCorrecta(object? contenido);

        RespuestaEstandar RespuestaIncorrecta(string mensaje);

        string GenerarContrasenna(int longitud);

        void EnviarCorreo(string destinatario, string asunto, string cuerpo);

        string Encrypt(string texto);

        string GenerarToken(long IdUsuario);

        long ObtenerIdUsuario(IEnumerable<Claim> token);

    }
}
