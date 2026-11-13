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
            var exchangeRate = await exchangeRateService.GetUsdToArsAsync(
                cancellationToken);

            return Ok(exchangeRate);
        }
    }
}