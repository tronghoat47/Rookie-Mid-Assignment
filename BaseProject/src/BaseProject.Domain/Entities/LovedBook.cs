namespace BaseProject.Domain.Entities
{
    public class LovedBook
    {
        public long BookId { get; set; }
        public Book? Book { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}