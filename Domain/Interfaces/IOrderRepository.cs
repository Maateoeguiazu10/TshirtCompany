using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);

        Task<Order?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Order>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
