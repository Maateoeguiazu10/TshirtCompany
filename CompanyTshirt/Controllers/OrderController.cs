using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Exceptions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyTshirt.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(
            [FromBody] OrderRequest request,
            CancellationToken cancellationToken)
        {
            var userId = GetAuthenticatedUserId();

            var order = await _orderService.CreateAsync(
                request, userId, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                order);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var userId = GetAuthenticatedUserId();

            var order = await _orderService.GetByIdAsync(
                id, userId, cancellationToken);

            if (order == null)
                return NotFound("No existe el pedido o no tenés permiso para consultarlo.");

            return Ok(order);
        }

        [HttpGet("MyOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetMyOrders(
            CancellationToken cancellationToken)
        {
            var userId = GetAuthenticatedUserId();

            var orders = await _orderService.GetByUserIdAsync(
                userId, cancellationToken);

            return Ok(orders);
        }

        private Guid GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedException("No se pudo identificar al usuario.");

            return userId;
        }
    }
}
