namespace JProyecto.Models
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
