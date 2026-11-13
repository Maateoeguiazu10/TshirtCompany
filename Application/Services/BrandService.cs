using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Application.Exceptions;

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
            ValidateRequest(request);

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
            var brand = await _brandRepository.GetByIdAsync(
                id, cancellationToken);

            if (brand == null)
                return null;

            return BrandResponse.FromBrand(brand);
        }

        public async Task<BrandResponse> UpdateAsync(
            Guid id,
            BrandRequest request,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(request);

            var brand = await _brandRepository.GetByIdAsync(
                id, cancellationToken);

            if (brand == null)
                throw new NotFoundException(
                    $"No se encontró la marca con ID: {id}.");

            brand.Name = request.Name;
            brand.UpdatedDateTime = DateTime.UtcNow;

            await _brandRepository.SaveChangesAsync(cancellationToken);

            return BrandResponse.FromBrand(brand);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandRepository.GetByIdAsync(
                id, cancellationToken);

            if (brand == null)
                throw new NotFoundException(
                    $"No se encontró la marca con ID: {id}.");

            brand.IsDeleted = true;
            brand.DeletedDateTime = DateTime.UtcNow;
            brand.UpdatedDateTime = DateTime.UtcNow;

            await _brandRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static void ValidateRequest(BrandRequest request)
        {
            if (request == null)
                throw new ValidationException(
                    "La solicitud no puede ser nula.");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException(
                    "El nombre de la marca es obligatorio.");
        }
    }
}