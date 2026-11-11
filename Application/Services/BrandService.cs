using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;

namespace CompanyTshirt.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BrandResponse> CreateAsync(
            BrandRequest request,
            CancellationToken cancellationToken = default)
        {
            var brand = new Brand
            {
                Name = request.Name,
                UpdatedDateTime = DateTime.UtcNow
            };

            _brandRepository.Add(brand);

            await _brandRepository.SaveChangesAsync(cancellationToken);

            return BrandResponse.FromBrand(brand);
        }

        public async Task<List<BrandResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var brands = await _brandRepository.GetAllAsync(cancellationToken);

            return brands
                .Select(BrandResponse.FromBrand)
                .ToList();
        }

        public async Task<BrandResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return null;

            return BrandResponse.FromBrand(brand);
        }

        public async Task<BrandResponse> UpdateAsync(
            Guid id,
            BrandRequest request,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);

            if (brand == null)
                throw new KeyNotFoundException("Brand not found.");

            brand.Name = request.Name;
            brand.UpdatedDateTime = DateTime.UtcNow;

            await _brandRepository.SaveChangesAsync(cancellationToken);
            await _brandRepository.SaveChangesAsync(cancellationToken);

            return BrandResponse.FromBrand(brand);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return false;

            brand.IsDeleted = true;
            brand.DeletedDateTime = DateTime.UtcNow;
            brand.UpdatedDateTime = DateTime.UtcNow;

            await _brandRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
