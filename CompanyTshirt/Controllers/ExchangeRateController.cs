using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ExchangeRateController(
        IExchangeRateService exchangeRateService) : ControllerBase
    {
        [HttpGet("usd-ars")]
        public async Task<ActionResult<ExchangeRateResponse>> GetUsdToArs(
            CancellationToken cancellationToken)
        {
            try
            {
                var exchangeRate = await exchangeRateService.GetUsdToArsAsync(
                    cancellationToken);

                return Ok(exchangeRate);
            }
            catch (HttpRequestException)
            {
                return StatusCode(
                    StatusCodes.Status502BadGateway,
                    "No se pudo consultar la API de cotizaciones.");
            }
            catch (TaskCanceledException)
            {
                return StatusCode(
                    StatusCodes.Status504GatewayTimeout,
                    "La API de cotizaciones tardó demasiado en responder.");
            }
        }
    }
}