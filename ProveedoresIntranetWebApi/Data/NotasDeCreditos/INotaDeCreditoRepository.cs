using ProveedoresIntranetWebApi.Models;

namespace ProveedoresIntranetWebApi.Data.NotasDeCreditos
{
    public interface INotaDeCreditoRepository
    {
        bool SaveChanges();

        Task<IEnumerable<NotaDeCredito>> GetAllNotasDeCreditos();

        Task<NotaDeCredito?> GetNotasDeCreditosById(int id);
        Task CreateNotaDeCredito(NotaDeCredito NotaDeCredito);
        Task DeleteNotaDeCreditoById(int id);
    }
}
