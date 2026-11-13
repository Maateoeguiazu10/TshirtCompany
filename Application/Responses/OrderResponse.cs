

namespace CompanyTshirt.Application.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
            = new();
    }
}
