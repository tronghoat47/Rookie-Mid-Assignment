namespace BaseProject.Application.Models.Responses
{
    public class DiscountResponse
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
    }
}