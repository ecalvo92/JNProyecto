using JApi.Models;

namespace JApi.Services
{
    public interface IUtilitarios
    {
        RespuestaEstandar RespuestaCorrecta(object? contenido);

        RespuestaEstandar RespuestaIncorrecta(string mensaje);
    }
}
