using CompanyTshirt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Requests
{
    public class TshirtRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public int Stock { get; set; } = 0;
        public string Season { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid BrandId { get; set; }
        public Guid SizeId { get; set; }
        public Guid ClubId { get; set; }

    }
}
