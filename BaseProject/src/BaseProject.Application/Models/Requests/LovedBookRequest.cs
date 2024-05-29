namespace BaseProject.Application.Models.Requests
{
    public class LovedBookRequest
    {
        public string UserId { get; set; } = string.Empty;
        public long BookId { get; set; }
    }
}