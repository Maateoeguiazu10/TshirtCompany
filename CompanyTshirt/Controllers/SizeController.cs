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
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpPost]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<SizeResponse>> CreateAsync(
            SizeRequest request,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = size.Id },
                size);
        }

        [HttpGet]
        public async Task<ActionResult<List<SizeResponse>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var sizes = await _sizeService.GetAllAsync(cancellationToken);

            return Ok(sizes);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SizeResponse>> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeService.GetByIdAsync(id, cancellationToken);

            if (size == null)
                return NotFound();

            return Ok(size);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<SizeResponse>> UpdateAsync(
            Guid id,
            SizeRequest request,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeService.UpdateAsync(id, request, cancellationToken);

            return Ok(size);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            await _sizeService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}