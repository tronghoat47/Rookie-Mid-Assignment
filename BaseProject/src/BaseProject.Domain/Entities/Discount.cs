namespace BaseProject.Domain.Entities
{
    public class Discount
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}