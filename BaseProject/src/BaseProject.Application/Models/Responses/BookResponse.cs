using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Responses
{
    public class BookResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int ReleaseYear { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public ICollection<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
        public ICollection<LovedBookResponse> LovedBooks { get; set; } = new List<LovedBookResponse>();
    }
}