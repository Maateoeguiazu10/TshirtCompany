using CompanyTshirt.Domain;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;

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

        public void Delete(Tshirt tshirt)
        {
            _context.Tshirts.Remove(tshirt);
        }

        public IReadOnlyList<Tshirt> GetAll()
        {
            return _context.Tshirts.ToList();
        }

        public Tshirt? GetById(Guid id)
        {
             return _context.Tshirts.FirstOrDefault(t => t.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
