using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Application.Responses
{
    public record BrandResponse(
        string Name,
        Guid Id)
    {
        public static BrandResponse FromBrand(Brand brand) => new(
               brand.Name,
               brand.Id
           );
    }
}
