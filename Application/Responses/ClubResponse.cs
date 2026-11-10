using CompanyTshirt.Domain.Entities;

namespace CompanyTshirt.Application.Responses
{
    public record ClubResponse(
        string Name,
        string Country,
        string LogoUrl,
        Guid Id)
    {
        public static ClubResponse FromClub(Club club) => new(
            club.Name,
            club.Country,
            club.LogoUrl,
            club.Id
        );
    }
}
