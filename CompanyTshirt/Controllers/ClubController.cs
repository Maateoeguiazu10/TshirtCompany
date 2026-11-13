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
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpPost]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<ClubResponse>> CreateAsync(
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = club.Id },
                club);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClubResponse>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var clubs = await _clubService.GetAllAsync(cancellationToken);

            return Ok(clubs);
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
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<ActionResult<ClubResponse>> UpdateAsync(
            Guid id,
            ClubRequest request,
            CancellationToken cancellationToken = default)
        {
            var club = await _clubService.UpdateAsync(id, request, cancellationToken);

            return Ok(club);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Polices.SoloAdministradores)]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            await _clubService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}