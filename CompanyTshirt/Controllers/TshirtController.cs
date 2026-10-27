using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TShirtController : ControllerBase
    {
        private readonly ITshirtService _tshirtService;

        public TShirtController(ITshirtService tshirtService)
        {
            _tshirtService = tshirtService;
        }

        [HttpPost]
        public async Task<ActionResult<TshirtResponse>> Create(
            [FromBody] TshirtRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var tshirt = await _tshirtService.CreateAsync(request, cancellationToken);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = tshirt.Id },
                    tshirt);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TshirtResponse>>> GetAll(
            CancellationToken cancellationToken)
        {
            var tshirts = await _tshirtService.GetAllAsync(cancellationToken);

            if (!tshirts.Any())
            {
                return NotFound("No hay camisetas registradas.");
            }

            return Ok(tshirts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TshirtResponse>> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var tshirt = await _tshirtService.GetByIdAsync(
                id, cancellationToken);

            if (tshirt == null)
            {
                return NotFound(
                    $"No existe una camiseta con el id {id}.");
            }

            return Ok(tshirt);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TshirtResponse>> Update(
            [FromRoute] Guid id,
            [FromBody] TshirtRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var tshirt = await _tshirtService.UpdateAsync(id, request, cancellationToken);

                return Ok(tshirt);
            }
            catch (CompanyTshirt.Application.Exceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var deleted = await _tshirtService.DeleteAsync(
                id, cancellationToken);

            if (!deleted)
            {
                return NotFound(
                    $"No existe una camiseta con el id {id}.");
            }

            return NoContent();
        }
    }
}