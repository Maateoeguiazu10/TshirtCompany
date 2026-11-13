using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;


namespace CompanyTshirt.Application.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(
            OrderRequest request,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<OrderResponse?> GetByIdAsync(
            Guid id,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderResponse>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
