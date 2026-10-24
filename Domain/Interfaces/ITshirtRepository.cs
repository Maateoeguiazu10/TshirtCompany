using CompanyTshirt.Domain;

namespace CompanyTshirt.Domain.Interfaces
{
    public interface ITshirtRepository
    {
        void Add(Tshirt tshirt);

        IReadOnlyList<Tshirt> GetAll();

        Tshirt? GetById(Guid id);

        void Delete(Tshirt tshirt);

        void SaveChanges();
    }
}
