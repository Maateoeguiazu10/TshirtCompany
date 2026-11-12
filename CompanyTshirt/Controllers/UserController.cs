using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetAll(
            CancellationToken cancellationToken)
        {
            var users = await userService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await userService.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserResponse>> Update(
            Guid id,
            [FromBody] UserUpdateRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await userService.UpdateAsync(
                    id,
                    request.Name,
                    request.Surname,
                    request.Email,
                    cancellationToken);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}