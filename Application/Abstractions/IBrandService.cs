using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;


namespace CompanyTshirt.Application.Abstractions
{
    public interface IBrandService
    {
        Task<BrandResponse> CreateAsync(
         BrandRequest request,
         CancellationToken cancellationToken = default);

        Task<List<BrandResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<BrandResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<BrandResponse> UpdateAsync(
            Guid id,
            BrandRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
