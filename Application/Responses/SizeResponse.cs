using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Application.Responses
{
    public record SizeResponse(string Type, Guid Id)
    {
        public static SizeResponse FromSize(Size size)
        {
            return new SizeResponse(size.Type, size.Id);
        }
    }
}