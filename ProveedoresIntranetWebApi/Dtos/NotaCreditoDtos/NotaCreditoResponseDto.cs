using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProveedoresIntranetWebApi.Dtos.NotaCreditoDtos
{
    public class NotaCreditoResponseDto
    {
        public int Id { get; set; }
        public string? FacturaNo { get; set; }
        public string? NumeroNotaDeCredito { get; set; }
        public decimal MontoNotaDeCredito { get; set; }
        public string? UrlNotaDeCredito { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
