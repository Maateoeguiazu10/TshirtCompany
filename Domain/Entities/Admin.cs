using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Domain.Entities
{
    public class Admin : User
    {
        private Admin()
        {
        }
        public Admin(string name, string surname, string email, string passwordHash)
         : base(name, surname, email, passwordHash)
        {
        }
    }
}

