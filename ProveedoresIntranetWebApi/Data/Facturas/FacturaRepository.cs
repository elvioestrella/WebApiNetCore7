using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Middleware;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;
using System.Net;

namespace ProveedoresIntranetWebApi.Data.Facturas
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSesion _usuarioSesion;
        private readonly UserManager<Usuario> _userManager;

        public FacturaRepository(AppDbContext contexto, UsuarioSesion usuarioSesion, UserManager<Usuario> userManager) 
        {
            _contexto = contexto;
            _usuarioSesion = usuarioSesion;
            _userManager = userManager;
        }
        public async Task CreateFactura(Factura factura)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
            if(usuario is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.Unauthorized,
                    new { mensaje = "El usuario no es valido para hacer esta inserción" }
                );
            }

            if(factura is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "Los datos de la factura son incorrectos." }
                );
            }

            factura.FechaCreacion = DateTime.Now;
            factura.UsuarioId = Guid.Parse(usuario!.Id);

            _contexto.Factura.Add(factura);
        }

        public async Task DeleteFacturaById(int id)
        {
            var factura = await _contexto.Factura!.FirstOrDefaultAsync(f => f.Id == id);
            if(factura is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "No se encontro esta factura para ser eliminada." }
                );
            }
            _contexto.Factura!.Remove(factura!);

        }

        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            var resultado = await _contexto.Factura!.ToListAsync();

            if(resultado.Count == 0)
            {
                throw new MiddlewareException(
                    HttpStatusCode.NotFound,
                    new { mensaje = "No tiene facturas disponibles." }
                );
            }

            return resultado;
        }

        public async Task<Factura?> GetFacturaById(int id)
        {
            var resultado = await _contexto.Factura.FirstOrDefaultAsync(x => x.Id == id)!;
            if(resultado is null)
            {
                throw new MiddlewareException(
                    HttpStatusCode.BadRequest,
                    new { mensaje = "Factura no encontrada." }
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
