namespace BaseProject.Domain.Entities
{
    public class Book : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string? Image { get; set; } = string.Empty;
        public long? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<LovedBook> LovedBooks { get; set; } = new List<LovedBook>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<BorrowingDetail> BorrowingDetails { get; set; } = new List<BorrowingDetail>();
    }
}