using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Models;
using ProveedoresIntranetWebApi.Token;

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

            NotaDeCredito.FechaCreacion = DateTime.Now;
            NotaDeCredito.UsuarioId = Guid.Parse(usuario!.Id);

            _contexto.NotaDeCredito.Add(NotaDeCredito);
        }

        public async Task DeleteNotaDeCreditoById(int id)
        {
            var notaDeCredito = await _contexto.NotaDeCredito!.FirstOrDefaultAsync(x => x.Id == id);
            _contexto.NotaDeCredito!.Remove(notaDeCredito!);
        }

        public async Task<IEnumerable<NotaDeCredito>> GetAllNotasDeCreditos()
        {
            return await _contexto.NotaDeCredito!.ToListAsync();
        }

        public async Task<NotaDeCredito?> GetNotasDeCreditosById(int id)
        {
            return await _contexto.NotaDeCredito.FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public bool SaveChanges()
        {
            return (_contexto.SaveChanges() >= 0);
        }
    }
}
