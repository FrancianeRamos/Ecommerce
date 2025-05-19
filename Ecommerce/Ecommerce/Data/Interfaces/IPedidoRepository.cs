using Ecommerce.Models;

namespace Ecommerce.Data.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> Get();
        Task<Pedido> GetById(int id);
        Task Add(Pedido pedido);
        Task Update(Pedido pedido);
        Task<bool> SaveChanges();
    }
}
