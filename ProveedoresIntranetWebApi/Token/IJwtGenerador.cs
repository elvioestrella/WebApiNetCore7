using ProveedoresIntranetWebApi.Models;

namespace ProveedoresIntranetWebApi.Token
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
