namespace ProveedoresIntranetWebApi.Dtos.UsuarioDtos
{
    public class UsuarioRegistroRequestDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? CodigoEmpresa { get; set; }
        public string? Telefono { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
