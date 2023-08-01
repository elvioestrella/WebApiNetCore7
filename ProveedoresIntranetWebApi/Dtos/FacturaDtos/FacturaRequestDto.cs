namespace ProveedoresIntranetWebApi.Dtos.FacturaDtos
{
    public class FacturaRequestDto
    {
        public string? FacturaNo { get; set; }
        public DateTime FacturaFecha { get; set; }

        public string? NumeroNotaDeCredito { get; set; }

        public string? Moneda { get; set; }
        public decimal MontoSinITBIS { get; set; }
        public decimal MontoConITBIS { get; set; }
        public string? UrlFactura { get; set; }
    }
}
