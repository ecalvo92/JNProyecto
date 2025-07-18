using JApi.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveSegura").Value!);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(texto);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveSegura").Value!);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string GenerarToken(long IdUsuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveSegura").Value!);

            var claims = new[]
            {
                new Claim("IdUsuario", IdUsuario.ToString()),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public long ObtenerIdUsuario(IEnumerable<Claim> token)
        {
            if (token.Any())
            {
                var idUsuarioClaim = token.FirstOrDefault(c => c.Type == "IdUsuario");
                if (idUsuarioClaim != null && long.TryParse(idUsuarioClaim.Value, out long idUsuario))
                {
                    return idUsuario;
                }
            }

            return 0;
        }

    }
}
