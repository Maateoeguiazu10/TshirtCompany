using CompanyTshirt.Domain.Entities;


namespace CompanyTshirt.Domain.Interfaces
{
    public interface ITshirtRepository
    {
        void Add(Tshirt tshirt);

        Task<IReadOnlyList<Tshirt>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Tshirt?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default); 
        void Remove(Tshirt tshirt);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
