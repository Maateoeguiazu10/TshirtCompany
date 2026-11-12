namespace CompanyTshirt.Application.Responses
{
    public record ExchangeRateResponse(
        string BaseCurrency,
        string TargetCurrency,
        decimal Rate,
        string LastUpdated);
}