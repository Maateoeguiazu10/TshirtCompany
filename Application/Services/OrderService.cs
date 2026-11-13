using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Application.Exceptions;

namespace CompanyTshirt.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITshirtRepository _tshirtRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ITshirtRepository tshirtRepository)
        {
            _orderRepository = orderRepository;
            _tshirtRepository = tshirtRepository;
        }

        public async Task<OrderResponse> CreateAsync(
            OrderRequest request,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (request.OrderDetails == null || !request.OrderDetails.Any())
                throw new ValidationException(
                    "El pedido debe contener al menos una camiseta.");

            var order = new Order
            {
                UserId = userId,
                TotalPrice = 0,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var detail in request.OrderDetails)
            {
                if (detail.Quantity <= 0)
                    throw new ValidationException(
                        "La cantidad debe ser mayor a cero.");

                var tshirt = await _tshirtRepository.GetByIdAsync(
                    detail.TshirtId,
                    cancellationToken);

                if (tshirt == null)
                    throw new NotFoundException(
                        $"No se encontró la camiseta {detail.TshirtId}.");

                if (tshirt.Stock < detail.Quantity)
                    throw new ConflictException(
                        $"Stock insuficiente para la camiseta {tshirt.Name}.");

                var orderDetail = new OrderDetail
                {
                    TshirtId = tshirt.Id,
                    Quantity = detail.Quantity,
                    UnitPrice = tshirt.Price
                };

                order.OrderDetails.Add(orderDetail);

                order.TotalPrice += tshirt.Price * detail.Quantity;

                tshirt.Stock -= detail.Quantity;
            }

            _orderRepository.Add(order);

            await _orderRepository.SaveChangesAsync(cancellationToken);

            return MapToResponse(order);
        }

        public async Task<OrderResponse?> GetByIdAsync(
            Guid id,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (order == null || order.UserId != userId)
                return null;

            return MapToResponse(order);
        }

        public async Task<IReadOnlyList<OrderResponse>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetByUserIdAsync(
                userId,
                cancellationToken);

            return orders.Select(MapToResponse).ToList();
        }

        private static OrderResponse MapToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,

                OrderDetails = order.OrderDetails.Select(detail =>
                    new OrderDetailResponse
                    {
                        TshirtId = detail.TshirtId,
                        TshirtName = detail.Tshirt?.Name ?? string.Empty,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        Subtotal = detail.UnitPrice * detail.Quantity
                    }).ToList()
            };
        }
    }
}