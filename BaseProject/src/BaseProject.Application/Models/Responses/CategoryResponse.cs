namespace BaseProject.Application.Models.Responses
{
    public class CategoryResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<BookResponse> Books { get; set; } = new List<BookResponse>();
    }
}