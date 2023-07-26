namespace ProveedoresIntranetWebApi.Dtos.UsuarioDtos
{
    public class UsuarioResponseDto
    {
        public string? Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? CodigoEmpresa { get; set; }
        public string? Telefono { get; set; }
        public string? Token { get; set; } 
        public string? UserName { get; set;}
        public string? Email { get; set; }
    }
}
