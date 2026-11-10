using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Domain.Interfaces
{
    public interface ISizeRepository
    {
        void Add(Size size);

        Task<IReadOnlyList<Size>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Size?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        void Remove(Size size);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}