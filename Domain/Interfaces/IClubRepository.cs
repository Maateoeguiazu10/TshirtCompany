using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Domain.Interfaces
{
    public interface IClubRepository
    {
        void Add(Club club);

        Task<IReadOnlyList<Club>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Club?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        void Remove(Club club);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}