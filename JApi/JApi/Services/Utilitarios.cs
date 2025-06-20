using JApi.Models;

namespace JApi.Services
{
    public class Utilitarios
    {

        public RespuestaEstandar RespuestaCorrecta(object? contenido)
        { 
            return new RespuestaEstandar
            {
                Codigo = 0,
                Mensaje = "Operación exitosa",
                Contenido = contenido
            };
        }

        public RespuestaEstandar RespuestaIncorrecta(string mensaje)
        {
            return new RespuestaEstandar
            {
                Codigo = 99,
                Mensaje = mensaje,
                Contenido = null
            };
        }

    }
}
