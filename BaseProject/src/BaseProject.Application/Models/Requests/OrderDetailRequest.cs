namespace BaseProject.Application.Models.Requests
{
    public class OrderDetailRequest
    {
        public long OrderId { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}