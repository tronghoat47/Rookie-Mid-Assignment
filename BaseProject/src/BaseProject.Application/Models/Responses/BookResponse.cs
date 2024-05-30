namespace BaseProject.Application.Models.Responses
{
    public class BookResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string? Image { get; set; } = string.Empty;
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public ICollection<LovedBookResponse> LovedBooks { get; set; } = new List<LovedBookResponse>();
    }
}