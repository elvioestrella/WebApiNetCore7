using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProveedoresIntranetWebApi.Dtos.FacturaDtos
{
    public class FacturaResponseDto
    {
        public int Id { get; set; }
        public string? FacturaNo { get; set; }
        public DateTime FacturaFecha { get; set; }

        public string? NumeroNotaDeCredito { get; set; }

        public string? Moneda { get; set; }
        public decimal MontoSinITBIS { get; set; }
        public decimal MontoConITBIS { get; set; }
        public string? UrlFactura { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
