using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;

namespace CompanyTshirt.Application.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<ClubResponse> CreateAsync(
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = new Club
            {
                Name = request.Name,
                Country = request.Country,
                LogoUrl = request.LogoUrl
            };

            _clubRepository.Add(club);
            await _clubRepository.SaveChangesAsync(cancellationToken);

            return ClubResponse.FromClub(club);
        }

        public async Task<List<ClubResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var clubs = await _clubRepository.GetAllAsync(cancellationToken);

            return clubs
                .Select(ClubResponse.FromClub)
                .ToList();
        }

        public async Task<ClubResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubRepository.GetByIdAsync(id, cancellationToken);

            return club is null ? null : ClubResponse.FromClub(club);
        }

        public async Task<ClubResponse?> UpdateAsync(
            Guid id,
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubRepository.GetByIdAsync(id, cancellationToken);

            if (club is null)
                return null;

            club.Name = request.Name;
            club.Country = request.Country;
            club.LogoUrl = request.LogoUrl;

            await _clubRepository.SaveChangesAsync(cancellationToken);

            return ClubResponse.FromClub(club);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubRepository.GetByIdAsync(id, cancellationToken);

            if (club is null)
                return false;

            _clubRepository.Remove(club);
            await _clubRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}