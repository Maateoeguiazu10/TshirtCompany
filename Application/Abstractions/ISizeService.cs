using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;

namespace CompanyTshirt.Application.Abstractions
{
    public interface ISizeService
    {
        Task<SizeResponse> CreateAsync(
            SizeRequest request,
            CancellationToken cancellationToken = default);

        Task<List<SizeResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<SizeResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<SizeResponse> UpdateAsync(
            Guid id,
            SizeRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}