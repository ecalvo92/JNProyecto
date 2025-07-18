namespace JApi.Models
{
    public class Autenticacion
    {
        public long IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Identificacion { get; set; }
        public string? Contrasenna { get; set; }
        public bool Estado { get; set; }
        public string? Token { get; set; }
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }

    }
}
