namespace JApi.Models
{
    public class Carrito
    {
        public long IdUsuario { get; set; }
        public long IdProducto { get; set; }

        public long IdCarrito { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }

        public long IdMaestro { get; set; }
    }
}
