using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Requests
{
    public class OrderDetailRequest
    {
        public Guid TshirtId { get; set; }
        public int Quantity { get; set; }
    }
}
