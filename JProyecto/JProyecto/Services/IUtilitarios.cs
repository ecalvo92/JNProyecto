using System.Security.Claims;

namespace JProyecto.Services
{
    public interface IUtilitarios
    {
        string Encrypt(string texto);

        long ObtenerIdUsuario(IEnumerable<Claim> token);
    }
}
