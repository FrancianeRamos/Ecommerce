using Ecommerce.Data.Interfaces;
using Ecommerce.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecommerce.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Pedido> _dbSet;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Pedido>();
        }

        public async Task<IEnumerable<Pedido>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Pedido> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Add(Pedido pedido)
        {
            await _dbSet.AddAsync(pedido);
            await SaveChanges();
        }

        public async Task Update(Pedido pedido)
        {
 
            var pedidoId = _context.Entry(pedido).Property("Id").CurrentValue;

            var trackedEntity = _context.ChangeTracker.Entries<Pedido>()
                .FirstOrDefault(e => e.Property("Id").CurrentValue.Equals(pedidoId));

            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            _context.Entry(pedido).State = EntityState.Modified;

            await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
