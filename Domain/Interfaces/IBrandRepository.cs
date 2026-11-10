using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Domain.Interfaces
{
    public interface IBrandRepository
    {
        void Add(Entities.Brand brand);
        Task<IReadOnlyList<Entities.Brand>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Entities.Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Entities.Brand brand);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
