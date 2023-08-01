using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Dtos.UsuarioDtos;
using ProveedoresIntranetWebApi.Middleware;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;
using System.Net;

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
            if(usuario is null)
            {
                throw new MiddlewareException(
                        HttpStatusCode.Unauthorized,
                        new { mensaje = "El usuario del token no existe en la base de datos." }
                );
            }
            return TransformerUserToUserDto(usuario!);
        }
        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);

            if (usuario is null)
            {
                throw new MiddlewareException(
                        HttpStatusCode.Unauthorized,
                        new { mensaje = "El email del usuario no existe en mi base de datos." }
                );
            }

           var resultado = await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);

            if (resultado.Succeeded)
            {
                return TransformerUserToUserDto(usuario!);
            }

            throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensaje = "Las credenciales son incorrectas." });
        }
        public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
        {

            var existeEmail = await _contexto.Users.Where(x => x.Email == request.Email).AnyAsync();
            if (existeEmail)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "El email del usuario ya existe en la base de datos." });
            }


            var existeUsername = await _contexto.Users.Where(x => x.UserName == request.UserName).AnyAsync();
            if (existeUsername)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "El username el usuario ya existe en la base de datos." });
            }

            var usuario = new Usuario
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                CodigoEmpresa = request.CodigoEmpresa,
                Telefono = request.Telefono,
                UserName = request.UserName,
                Email = request.Email,

            };
            var resultado = await _userManager.CreateAsync(usuario!, request.Password!);
            if (resultado.Succeeded)
            {
                return TransformerUserToUserDto(usuario!);
            }

            throw new Exception("No se pudo registrar el usuario.");
        }
    }
}
