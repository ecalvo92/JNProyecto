using JProyecto.Models;

namespace JProyecto.Services
{
    public interface IUtilitarios
    {
        string Encrypt(string texto);
        List<Carrito> ConsultarDatosCarrito();
    }
}
