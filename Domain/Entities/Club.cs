using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Domain.Entities
{
    public class Club : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }
}
