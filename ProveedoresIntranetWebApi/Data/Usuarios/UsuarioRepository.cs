using Microsoft.AspNetCore.Identity;
using ProveedoresIntranetWebApi.Dtos.UsuarioDtos;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;

namespace ProveedoresIntranetWebApi.Data.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSesion _usuarioSesion;
        public UsuarioRepository(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IJwtGenerador jwtGenerador,
            AppDbContext contexto,
            IUsuarioSesion usuarioSesion
         )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerador = jwtGenerador;
            _contexto = contexto;
            _usuarioSesion = usuarioSesion;
        }

        private UsuarioResponseDto TransformerUserToUserDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                CodigoEmpresa = usuario.CodigoEmpresa,
                UserName = usuario.UserName,
                Token = _jwtGenerador.CrearToken(usuario)
            };
        }
        public async Task<UsuarioResponseDto> GetUsuario()
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
            return TransformerUserToUserDto(usuario!);


        }

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);

            await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);

            return TransformerUserToUserDto(usuario!);
        }

        public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
        {
            var usuario = new Usuario
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                CodigoEmpresa = request.CodigoEmpresa,
                Telefono = request.Telefono,
                UserName = request.UserName,
                Email = request.Email,

            };
            await _userManager.CreateAsync(usuario!, request.Password!);
            return TransformerUserToUserDto(usuario!);
        }
    }
}
