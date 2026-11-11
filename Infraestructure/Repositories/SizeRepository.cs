using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyTshirt.Infrastructure.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly TshirtCompanyDbContext _context;

        public SizeRepository(TshirtCompanyDbContext context)
        {
            _context = context;
        }

        public void Add(Size size)
        {
            _context.Sizes.Add(size);
        }

        public void Remove(Size size) => _context.Sizes.Remove(size);

        public async Task<IReadOnlyList<Size>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Sizes
                .Where(s => !s.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public Task<Size?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return _context.Sizes.FirstOrDefaultAsync(
                s => s.Id == id && !s.IsDeleted,
                cancellationToken);
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}