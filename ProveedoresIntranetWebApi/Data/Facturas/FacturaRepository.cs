using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;

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

            factura.FechaCreacion = DateTime.Now;
            factura.UsuarioId = Guid.Parse(usuario!.Id);

            _contexto.Factura.Add(factura);
        }

        public async Task DeleteFacturaById(int id)
        {
            var factura = await _contexto.Factura!.FirstOrDefaultAsync(f => f.Id == id);
            _contexto.Factura!.Remove(factura!);

        }

        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            return await _contexto.Factura!.ToListAsync();
        }

        public async Task<Factura?> GetFacturaById(int id)
        {
            return await _contexto.Factura.FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public bool SaveChanges()
        {
            return (_contexto.SaveChanges() >= 0);
        }

        
    }
}
