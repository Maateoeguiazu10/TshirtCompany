using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
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
        public async Task<ActionResult<SizeResponse>> CreateAsync(
        SizeRequest request,
        CancellationToken cancellationToken = default)
        {
            var size = await _sizeService.CreateAsync(request, cancellationToken);

            return Ok(size);
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
        public async Task<ActionResult<SizeResponse>> UpdateAsync(
            Guid id,
            SizeRequest request,
            CancellationToken cancellationToken = default)
        {
            var size = await _sizeService.UpdateAsync(id, request, cancellationToken);

            if (size == null)
                return NotFound();

            return Ok(size);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var deleted = await _sizeService.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}