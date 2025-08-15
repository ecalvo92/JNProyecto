using JProyecto.Models;
using System.Security.Cryptography;
using System.Text;

namespace JProyecto.Services
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accesor;
        private readonly IHttpClientFactory _http;
        public Utilitarios(IConfiguration configuration, IHttpContextAccessor accesor, IHttpClientFactory http)
        {
            _configuration = configuration;
            _accesor = accesor;
            _http = http;
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

        public List<Carrito> ConsultarDatosCarrito()
        {
            using (var http = _http.CreateClient())
            {
                var IdUsuario = _accesor.HttpContext!.Session.GetString("IdUsuario");
                var carrito = new Carrito
                {
                    IdUsuario = long.Parse(IdUsuario!)
                };

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accesor.HttpContext!.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("api/Carrito/ConsultarCarrito", carrito).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaEstandar<List<Carrito>>>().Result;

                    _accesor.HttpContext!.Session.SetString("Cantidad", datos?.Contenido?.Sum(x => x.Cantidad).ToString()!);
                    _accesor.HttpContext!.Session.SetString("Total", datos?.Contenido?.Sum(x => x.Total).ToString()!);
                    return datos?.Contenido!;
                }
                else
                {
                    _accesor.HttpContext!.Session.SetString("Cantidad", "0");
                    _accesor.HttpContext!.Session.SetString("Total", "0");
                    return new List<Carrito>();
                }
            }
        }

    }
}
