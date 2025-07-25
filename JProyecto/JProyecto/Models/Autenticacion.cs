namespace JProyecto.Models
{
    public class Autenticacion
    {
        public long IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Identificacion { get; set; }
        public string? Contrasenna { get; set; }
        public string? ConfirmarContrasenna { get; set; }
        public bool Estado { get; set; } = true;
        public string? Token { get; set; }
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
    }
}
