using BaseProject.Domain.Constants;

namespace BaseProject.Application.Models.Responses
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusOrderConstants.IS_PROCESSING;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal TotalPayment { get; set; }
        public long? DiscountId { get; set; }
        public decimal DiscountValue { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;

        public ICollection<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
    }
}