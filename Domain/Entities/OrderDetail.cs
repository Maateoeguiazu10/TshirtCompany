using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Domain.Entities
{
public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public Guid TshirtId { get; set; }
        public Tshirt Tshirt { get; set; } = null!;

        
    }
}
