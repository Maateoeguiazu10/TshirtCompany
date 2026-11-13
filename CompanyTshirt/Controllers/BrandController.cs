using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Constants;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<BrandResponse>> CreateAsync(
            BrandRequest request,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = brand.Id },
                brand);
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandResponse>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var brands = await _brandService.GetAllAsync(cancellationToken);

            return Ok(brands);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BrandResponse>> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandService.GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<BrandResponse>> UpdateAsync(
            Guid id,
            BrandRequest request,
            CancellationToken cancellationToken = default)
        {
            var brand = await _brandService.UpdateAsync(id, request, cancellationToken);

            return Ok(brand);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            await _brandService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}


