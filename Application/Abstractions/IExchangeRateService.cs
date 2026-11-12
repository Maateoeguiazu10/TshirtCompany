using CompanyTshirt.Application.Responses;

namespace CompanyTshirt.Application.Abstractions
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> GetUsdToArsAsync(
            CancellationToken cancellationToken = default);
    }
}