using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyTshirt.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly TshirtCompanyDbContext _context;

        public BrandRepository(TshirtCompanyDbContext context)
        {
            _context = context;
        }

        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
        }

        public void Remove(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task<IReadOnlyList<Brand>> GetAllAsync(
     CancellationToken cancellationToken = default)
        {
            return await _context.Brands
                .Where(b => !b.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public Task<Brand?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return _context.Brands.FirstOrDefaultAsync(
                b => b.Id == id && !b.IsDeleted,
                cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}