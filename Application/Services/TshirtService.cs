using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Exceptions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;


namespace CompanyTshirt.Application.Services
{
    public class TshirtService : ITshirtService
    {
        private readonly ITshirtRepository _tshirtRepository;

        public TshirtService(ITshirtRepository tshirtRepository)
        {
            _tshirtRepository = tshirtRepository;
        }


        public async Task<TshirtResponse> CreateAsync(
            TshirtRequest request,
            CancellationToken cancellationToken = default)
        {
            var tshirt = new Tshirt
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Season = request.Season,
                ImageUrl = request.ImageUrl,
                BrandId = request.BrandId,
                SizeId = request.SizeId,
                ClubId = request.ClubId
            };

            _tshirtRepository.Add(tshirt);
            await _tshirtRepository.SaveChangesAsync(cancellationToken);

            return TshirtResponse.FromTshirt(tshirt);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var tshirt = await _tshirtRepository.GetByIdAsync(id, cancellationToken);

            if (tshirt == null)
                return false;

            _tshirtRepository.Remove(tshirt);
            await _tshirtRepository.SaveChangesAsync(cancellationToken);

            return true;
        }


        public async Task<List<TshirtResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var tshirts = await _tshirtRepository.GetAllAsync(cancellationToken);

            return tshirts
                .Select(TshirtResponse.FromTshirt)
                .ToList();
        }

        public async Task<TshirtResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var tshirt = await _tshirtRepository.GetByIdAsync(id, cancellationToken);

            return tshirt == null
                ? null
                : TshirtResponse.FromTshirt(tshirt);
        }

        public async Task<TshirtResponse> UpdateAsync(
            Guid id,
            TshirtRequest request,
            CancellationToken cancellationToken = default)
        {
            var tshirtToUpdate = await _tshirtRepository.GetByIdAsync(id, cancellationToken);

            if (tshirtToUpdate == null)
            {
                throw new NotFoundException(
                    $"This ID was not found:  '{id}'.");
            }

            tshirtToUpdate.Name = request.Name;
            tshirtToUpdate.Description = request.Description;
            tshirtToUpdate.Price = request.Price;
            tshirtToUpdate.Stock = request.Stock;
            tshirtToUpdate.Season = request.Season;
            tshirtToUpdate.ImageUrl = request.ImageUrl;
            tshirtToUpdate.BrandId = request.BrandId;
            tshirtToUpdate.SizeId = request.SizeId;
            tshirtToUpdate.ClubId = request.ClubId;

            await _tshirtRepository.SaveChangesAsync(cancellationToken);

            return TshirtResponse.FromTshirt(tshirtToUpdate);
        }
    }
}


  
