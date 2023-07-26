using Microsoft.AspNetCore.Identity;
using ProveedoresIntranetWebApi.Models;

namespace ProveedoresIntranetWebApi.Data
{
    public class LoadDatabase
    {
        public static async Task InsertarData(AppDbContext context, UserManager<Usuario> userManager )
        {
            if(!userManager.Users.Any())
            {
                var usuario = new Usuario { 
                    Nombres = "Elvio Miguel",
                    Apellidos = "Estrella",
                    Email = "elvio.estrella@hiperole.com.do",
                    UserName = "elvio.estrella",
                    Telefono = "809-968-6867"
                };

                await userManager.CreateAsync(usuario, "Tecnologia@4321");
            }

            if (!context.Factura!.Any())
            {
                context.Factura!.AddRange(
                        new Factura
                        {
                            FacturaNo = "0051425-54",
                            FacturaFecha = DateTime.Now,
                            NumeroNotaDeCredito = "54542",
                            Moneda = "RD$",
                            MontoSinITBIS = 5452.25M,
                            MontoConITBIS = 6200.35M,
                            FechaCreacion = DateTime.Now,
                            UsuarioId = null

                        },
                        new Factura
                        {
                            FacturaNo = "2251425-54",
                            FacturaFecha = DateTime.Now,
                            NumeroNotaDeCredito = "55555",
                            Moneda = "RD$",
                            MontoSinITBIS = 5452.25M,
                            MontoConITBIS = 6200.35M,
                            FechaCreacion = DateTime.Now,
                            UsuarioId = null

                        }

                    );

            }
            if (!context.NotaDeCredito!.Any())
            {
                context.NotaDeCredito!.AddRange(
                    new NotaDeCredito
                    {
                        FacturaNo = "0051425-54",
                        NumeroNotaDeCredito = "54542",
                        MontoNotaDeCredito = 5452.25M,
                        UrlNotaDeCredito = "https://economipedia.com/wp-content/uploads/nota-de-cr%C3%A9dito.jpg",
                        FechaCreacion = DateTime.Now,
                        UsuarioId = null

                    }

                );

            }

            context.SaveChanges();
        }
    }
}
