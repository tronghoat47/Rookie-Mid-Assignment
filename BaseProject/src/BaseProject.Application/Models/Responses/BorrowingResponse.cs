using BaseProject.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Application.Models.Responses
{
    public class BorrowingResponse : BaseResponse
    {
        public long Id { get; set; }

        [Required]
        public string RequestorId { get; set; } = string.Empty;

        public string RequestorName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusBorrowing.PENDING;
        public string? ApproverId { get; set; } = string.Empty;
        public string? ApproverName { get; set; } = string.Empty;
        public ICollection<BorrowingDetailResponse> BorrowingDetails { get; set; } = new List<BorrowingDetailResponse>();
    }
}