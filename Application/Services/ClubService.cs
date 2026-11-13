using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Application.Exceptions;

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
            ValidateRequest(request);

            var club = new Club
            {
                Name = request.Name,
                Country = request.Country,
                LogoUrl = request.LogoUrl,
                UpdatedDateTime = DateTime.UtcNow
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
            var club = await _clubRepository.GetByIdAsync(
                id, cancellationToken);

            return club is null ? null : ClubResponse.FromClub(club);
        }

        public async Task<ClubResponse> UpdateAsync(
            Guid id,
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(request);

            var club = await _clubRepository.GetByIdAsync(
                id, cancellationToken);

            if (club is null)
                throw new NotFoundException(
                    $"No se encontró el club con ID: {id}.");

            club.Name = request.Name;
            club.Country = request.Country;
            club.LogoUrl = request.LogoUrl;
            club.UpdatedDateTime = DateTime.UtcNow;

            await _clubRepository.SaveChangesAsync(cancellationToken);

            return ClubResponse.FromClub(club);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubRepository.GetByIdAsync(
                id, cancellationToken);

            if (club is null)
                throw new NotFoundException(
                    $"No se encontró el club con ID: {id}.");

            club.IsDeleted = true;
            club.DeletedDateTime = DateTime.UtcNow;
            club.UpdatedDateTime = DateTime.UtcNow;

            await _clubRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static void ValidateRequest(ClubRequest request)
        {
            if (request == null)
                throw new ValidationException(
                    "La solicitud no puede ser nula.");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException(
                    "El nombre del club es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Country))
                throw new ValidationException(
                    "El país del club es obligatorio.");
        }
    }
}