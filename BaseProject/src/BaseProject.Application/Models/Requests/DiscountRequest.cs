namespace BaseProject.Application.Models.Requests
{
    public class DiscountRequest
    {
        public string Code { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}