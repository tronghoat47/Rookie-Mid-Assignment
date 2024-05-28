using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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