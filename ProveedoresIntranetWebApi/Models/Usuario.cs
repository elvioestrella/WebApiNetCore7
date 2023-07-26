using Microsoft.AspNetCore.Identity;

namespace ProveedoresIntranetWebApi.Models
{
    public class Usuario : IdentityUser
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? CodigoEmpresa { get; set; }
        public string? Telefono { get; set; }
    }
}
