using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Middleware;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;
using System.Net;

namespace ProveedoresIntranetWebApi.Data.NotasDeCreditos
{
    public class NotaDeCreditoRepository : INotaDeCreditoRepository
    {
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSesion _usuarioSesion;
        private readonly UserManager<Usuario> _userManager;

        public NotaDeCreditoRepository(AppDbContext contexto, UsuarioSesion usuarioSesion, UserManager<Usuario> userManager)
        {
            _contexto = contexto;
            _usuarioSesion = usuarioSesion;
            _userManager = userManager;
        }

        public async Task CreateNotaDeCredito(NotaDeCredito NotaDeCredito)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

            if (usuario is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.Unauthorized,
                    new { mensaje = "El usuario no es valido para hacer esta inserción" }
                );
            }

            if (NotaDeCredito is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "Los datos de la nota de crédito son incorrectos." }
                );
            }

            NotaDeCredito.FechaCreacion = DateTime.Now;
            NotaDeCredito.UsuarioId = Guid.Parse(usuario!.Id);

            _contexto.NotaDeCredito.Add(NotaDeCredito);
        }

        public async Task DeleteNotaDeCreditoById(int id)
        {
            var notaDeCredito = await _contexto.NotaDeCredito!.FirstOrDefaultAsync(x => x.Id == id);

            if (notaDeCredito is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "No se encontro esta nota de crédito para ser eliminada." }
                );
            }

            _contexto.NotaDeCredito!.Remove(notaDeCredito!);
        }

        public async Task<IEnumerable<NotaDeCredito>> GetAllNotasDeCreditos()
        {
            var resultado =  await _contexto.NotaDeCredito!.ToListAsync();

            if (resultado.Count == 0)
            {
                throw new MiddlewareException(
                    HttpStatusCode.NotFound,
                    new { mensaje = "No tienes nota de crédito disponibles." }
                );
            }

            return resultado;
        }

        public async Task<NotaDeCredito?> GetNotasDeCreditosById(int id)
        {
            var resultado = await _contexto.NotaDeCredito.FirstOrDefaultAsync(x => x.Id == id)!;

            if (resultado is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "Nota de crédito no encontrada." }
                );
            }

            return resultado;
        }

        public bool SaveChanges()
        {
            return (_contexto.SaveChanges() >= 0);
        }
    }
}
