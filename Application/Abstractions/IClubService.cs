using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;

namespace CompanyTshirt.Application.Abstractions
{
    public interface IClubService
    {
        Task<ClubResponse> CreateAsync(
            ClubRequest request,
            CancellationToken cancellationToken = default);

        Task<List<ClubResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ClubResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<ClubResponse> UpdateAsync(
            Guid id,
            ClubRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}