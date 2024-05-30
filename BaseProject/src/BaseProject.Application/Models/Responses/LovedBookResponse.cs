namespace BaseProject.Application.Models.Responses
{
    public class LovedBookResponse : BaseResponse
    {
        public long BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}