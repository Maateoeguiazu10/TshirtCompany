using CompanyTshirt.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyTshirt.Infrastructure.Persistence
{
    public class TshirtCompanyDbContext : DbContext
    {
        public TshirtCompanyDbContext(
            DbContextOptions<TshirtCompanyDbContext> options) : base(options)
        {
        }

        public DbSet<Tshirt> Tshirts => Set<Tshirt>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Size> Sizes => Set<Size>();
        public DbSet<Club> Clubs => Set<Club>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tshirt>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);
        }
    }
}