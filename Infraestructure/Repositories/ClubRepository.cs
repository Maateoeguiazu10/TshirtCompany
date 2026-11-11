using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyTshirt.Infrastructure.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly TshirtCompanyDbContext _context;

        public ClubRepository(TshirtCompanyDbContext context)
        {
            _context = context;
        }

        public void Add(Club club)
        {
            _context.Clubs.Add(club);
        }

        public void Remove(Club club)
        {
            _context.Clubs.Remove(club);
        }

        public async Task<IReadOnlyList<Club>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Clubs.ToListAsync(cancellationToken);
        }

        public Task<Club?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return _context.Clubs.FirstOrDefaultAsync(
                c => c.Id == id,
                cancellationToken);
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}