using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Application.Responses
{
    public record TshirtResponse(
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string Season,
        string ImageUrl,
        Guid Id,
        Guid BrandId,
        Guid SizeId,
        Guid ClubId)
    {
        public static TshirtResponse FromTshirt(Tshirt tshirt) => new(
               tshirt.Name,
               tshirt.Description,
               tshirt.Price,
               tshirt.Stock,
               tshirt.Season,
               tshirt.ImageUrl,
               tshirt.Id,
               tshirt.BrandId,
               tshirt.SizeId,
               tshirt.ClubId
           );
    }
 }
