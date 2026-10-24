namespace CompanyTshirt.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
    }
}
