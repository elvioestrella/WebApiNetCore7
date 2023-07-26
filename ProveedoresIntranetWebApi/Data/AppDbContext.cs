using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProveedoresIntranetWebApi.Models;

namespace ProveedoresIntranetWebApi.Data
{
    public class AppDbContext:IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Factura> Factura { get; set; }

        public DbSet<NotaDeCredito> NotaDeCredito { get; set; }
    }
}
