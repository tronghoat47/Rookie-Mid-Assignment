namespace BaseProject.Domain.Entities
{
    public class BorrowingDetail : BaseEntity
    {
        public long BorrowingId { get; set; }
        public Borrowing? Borrowing { get; set; }
        public long BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ReturnedAt { get; set; } = DateTime.Now.AddDays(7);
        public string Status { get; set; } = string.Empty;
    }
}