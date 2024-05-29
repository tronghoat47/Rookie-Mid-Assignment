using Microsoft.AspNetCore.Http;

namespace BaseProject.Application.Models.Requests
{
    public class BookRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int ReleaseYear { get; set; }
        public IFormFile Image { get; set; }
        public long? CategoryId { get; set; }
    }
}