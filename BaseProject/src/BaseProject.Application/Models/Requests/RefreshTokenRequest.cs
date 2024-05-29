namespace BaseProject.Application.Models.Requests
{
    public class RefreshTokenRequest
    {
        public string? UserId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}