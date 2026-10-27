namespace CompanyTshirt.Domain.Entities
{
    public class Tshirt : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public int Stock { get; set; } = 0;
        public string Season { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }

        public Guid SizeId { get; set; }
        public Size? Size { get; set; }

        public Guid ClubId { get; set; }
        public Club? Club { get; set; }

    }
}
