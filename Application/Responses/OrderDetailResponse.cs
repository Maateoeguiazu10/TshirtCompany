namespace CompanyTshirt.Application.Responses
{
    public class OrderDetailResponse
    {
        public Guid TshirtId { get; set; }
        public string TshirtName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
