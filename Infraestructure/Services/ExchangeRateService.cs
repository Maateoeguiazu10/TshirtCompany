using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Responses;
using System.Net.Http.Json;

namespace CompanyTshirt.Infrastructure.Services
{
    public class ExchangeRateService(HttpClient httpClient)
        : IExchangeRateService
    {
        public async Task<ExchangeRateResponse> GetUsdToArsAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetFromJsonAsync<ExchangeRateApiResponse>(
                "v6/latest/USD",
                cancellationToken);

            if (response == null || response.Result != "success")
                throw new HttpRequestException(
                    "No se pudo obtener la cotización de la API externa.");

            if (!response.Rates.TryGetValue("ARS", out var rate))
                throw new HttpRequestException(
                    "La cotización ARS no está disponible.");

            return new ExchangeRateResponse(
                response.BaseCode,
                "ARS",
                rate,
                response.LastUpdateUtc);
        }
    }
}