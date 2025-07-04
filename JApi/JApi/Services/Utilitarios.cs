using JApi.Models;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace JApi.Services
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;
        public Utilitarios(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        public string GenerarContrasenna(int longitud)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < longitud; i++)
            {
                int index = random.Next(caracteres.Length);
                sb.Append(caracteres[index]);
            }

            return sb.ToString();
        }

        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            var correoSalida = _configuration.GetSection("SMTP:CorreoSalida").Value;
            var claveCorreoSalida = _configuration.GetSection("SMTP:ClaveCorreoSalida").Value;

            if (!string.IsNullOrEmpty(correoSalida) && !string.IsNullOrEmpty(claveCorreoSalida))
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress(correoSalida);
                mensaje.To.Add(destinatario);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = true;

                SmtpClient cliente = new SmtpClient("smtp.office365.com", 587);
                cliente.Credentials = new NetworkCredential(correoSalida, claveCorreoSalida);
                cliente.EnableSsl = true;

                cliente.Send(mensaje);
            }
        }

    }
}
