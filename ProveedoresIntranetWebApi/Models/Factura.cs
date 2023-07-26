using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProveedoresIntranetWebApi.Models
{
    public class Factura
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? FacturaNo { get; set; }
        
        [Required]
        public DateTime FacturaFecha { get; set; }

        public string? NumeroNotaDeCredito { get; set; }

        [Required]
        public string? Moneda { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal MontoSinITBIS { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal MontoConITBIS { get; set; }

        [Required]
        public string? UrlFactura { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
