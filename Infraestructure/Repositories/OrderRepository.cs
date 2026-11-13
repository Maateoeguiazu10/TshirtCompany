using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace CompanyTshirt.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TshirtCompanyDbContext _context;

        public OrderRepository(TshirtCompanyDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<Order?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tshirt)
                .FirstOrDefaultAsync(
                    o => o.Id == id && !o.IsDeleted,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && !o.IsDeleted)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tshirt)
                .ToListAsync(cancellationToken);
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
