using ProveedoresIntranetWebApi.Models;

namespace ProveedoresIntranetWebApi.Data.Facturas
{
    public interface IFacturaRepository
    {
        bool SaveChanges();

        Task<IEnumerable<Factura>> GetAllFacturas();
        Task<Factura?> GetFacturaById(int id);
        Task CreateFactura(Factura factura);
        Task DeleteFacturaById(int id);

    }
}
