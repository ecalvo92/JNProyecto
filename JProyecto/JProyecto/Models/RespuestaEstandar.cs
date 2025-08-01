namespace JProyecto.Models
{
    public class RespuestaEstandar
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
    }

    public class RespuestaEstandar<T>
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
        public T? Contenido { get; set; }
    }
}
