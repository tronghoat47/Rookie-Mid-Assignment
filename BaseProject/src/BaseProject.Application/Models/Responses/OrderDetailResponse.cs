namespace BaseProject.Application.Models.Responses
{
    public class OrderDetailResponse
    {
        public long OrderId { get; set; }
        public long BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}