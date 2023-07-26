using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProveedoresIntranetWebApi.Models
{
    public class NotaDeCredito
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? FacturaNo { get; set; }

        [Required]
        [StringLength(50)]
        public string? NumeroNotaDeCredito { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal MontoNotaDeCredito { get; set; }

        [Required]
        public string? UrlNotaDeCredito { get; set; }

        public DateTime FechaCreacion { get; set; }
        public Guid? UsuarioId { get; set; }




    }
}
