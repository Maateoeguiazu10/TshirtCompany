using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace CompanyTshirt.Infrastructure.Repositories
{
    public class TshirtRepository : ITshirtRepository
    {
        private readonly TshirtCompanyDbContext _context;
        public TshirtRepository(TshirtCompanyDbContext context)
        {
            _context = context;
        }
        public void Add(Tshirt tshirt)
        {
            _context.Tshirts.Add(tshirt);
        }

        public void Remove(Tshirt tshirt) => _context.Tshirts.Remove(tshirt);

        public async Task<IReadOnlyList<Tshirt>> GetAllAsync(CancellationToken cancellationToken = default) => await _context.Tshirts.ToListAsync(cancellationToken);

        public Task<Tshirt?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Tshirts.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
