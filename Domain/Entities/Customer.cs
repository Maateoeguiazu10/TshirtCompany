
namespace CompanyTshirt.Domain.Entities
{
    public class Customer : User
    {
        private Customer()
        {
        }

        public Customer(string name, string surname, string email, string passwordHash)
            : base(name, surname, email, passwordHash)
        {
        }
    }
}

