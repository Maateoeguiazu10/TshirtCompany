using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Exceptions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain;
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

        public TshirtResponse Create(TshirtRequest request)
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
            _tshirtRepository.SaveChanges();
            return TshirtResponse.FromTshirt(tshirt);
        }

        public bool Delete(Guid id)
        {
            var tshirt = _tshirtRepository.GetById(id);
            if (tshirt == null){ 
                return false; }
            else
            {
                _tshirtRepository.Delete(tshirt);
                _tshirtRepository.SaveChanges();
                return true;
            }
        }

            
        public List<TshirtResponse> GetAll()
        {
           var tshirts = _tshirtRepository.GetAll();
           return tshirts.Select(TshirtResponse.FromTshirt).ToList();
        }

        public TshirtResponse? GetById(Guid id)
        {
            var tshirt = _tshirtRepository.GetById(id);
            return tshirt == null ? null : TshirtResponse.FromTshirt(tshirt);

        }

        public TshirtResponse Update(Guid id, TshirtRequest tshirt)
        {
            var request = _tshirtRepository.GetById(id);
            if (request == null)
            {
                throw new NotFoundException($"dont sherch a tshirt with this id'{id}'.");
            }

            request.Name = tshirt.Name;
            request.Description = tshirt.Description;
            request.Price = tshirt.Price;
            request.Stock = tshirt.Stock;
            request.Season = tshirt.Season;
            request.ImageUrl = tshirt.ImageUrl;

            _tshirtRepository.Update(request);


        }
    }
}

  
