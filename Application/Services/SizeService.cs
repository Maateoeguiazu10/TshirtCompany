using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Application.Exceptions;

namespace CompanyTshirt.Application.Services
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<SizeResponse> CreateAsync(
            SizeRequest request,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(request);

            var size = new Size
            {
                Type = request.Type,
                UpdatedDateTime = DateTime.UtcNow
            };

            _sizeRepository.Add(size);

            await _sizeRepository.SaveChangesAsync(cancellationToken);

            return SizeResponse.FromSize(size);
        }

        public async Task<List<SizeResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var sizes = await _sizeRepository.GetAllAsync(cancellationToken);

            return sizes
                .Select(SizeResponse.FromSize)
                .ToList();
        }

        public async Task<SizeResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeRepository.GetByIdAsync(
                id, cancellationToken);

            if (size == null)
                return null;

            return SizeResponse.FromSize(size);
        }

        public async Task<SizeResponse> UpdateAsync(
            Guid id,
            SizeRequest request,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(request);

            var size = await _sizeRepository.GetByIdAsync(
                id, cancellationToken);

            if (size == null)
                throw new NotFoundException(
                    $"No se encontró el talle con ID: {id}.");

            size.Type = request.Type;
            size.UpdatedDateTime = DateTime.UtcNow;

            await _sizeRepository.SaveChangesAsync(cancellationToken);

            return SizeResponse.FromSize(size);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeRepository.GetByIdAsync(
                id, cancellationToken);

            if (size == null)
                throw new NotFoundException(
                    $"No se encontró el talle con ID: {id}.");

            size.IsDeleted = true;
            size.DeletedDateTime = DateTime.UtcNow;
            size.UpdatedDateTime = DateTime.UtcNow;

            await _sizeRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static void ValidateRequest(SizeRequest request)
        {
            if (request == null)
                throw new ValidationException(
                    "La solicitud no puede ser nula.");

            if (string.IsNullOrWhiteSpace(request.Type))
                throw new ValidationException(
                    "El tipo de talle es obligatorio.");
        }
    }
}