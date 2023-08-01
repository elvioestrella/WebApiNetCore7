namespace ProveedoresIntranetWebApi.Dtos.NotaCreditoDtos
{
    public class NotaCreditoRequestDto
    {
        public string? FacturaNo { get; set; }
        public string? NumeroNotaDeCredito { get; set; }
        public decimal MontoNotaDeCredito { get; set; }
        public string? UrlNotaDeCredito { get; set; }
    }
}
