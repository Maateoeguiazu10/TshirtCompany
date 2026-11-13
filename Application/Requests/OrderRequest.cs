using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Requests
{
    public class OrderRequest
    {
        public List<OrderDetailRequest> OrderDetails { get; set; }
            = new();
    }
}
