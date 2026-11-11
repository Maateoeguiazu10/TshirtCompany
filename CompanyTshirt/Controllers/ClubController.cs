using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpPost]
        public async Task<ActionResult<ClubResponse>> CreateAsync(
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubService.CreateAsync(request, cancellationToken);

            return Ok(club);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClubResponse>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return Ok(await _clubService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClubResponse>> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubService.GetByIdAsync(id, cancellationToken);

            if (club is null)
                return NotFound();

            return Ok(club);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClubResponse>> UpdateAsync(
            Guid id,
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubService.UpdateAsync(id, request, cancellationToken);

            if (club is null)
                return NotFound();

            return Ok(club);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var deleted = await _clubService.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}