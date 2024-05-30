using BaseProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Application.Models.Requests
{
    public class BorrowingRequest
    {
        [Required]
        public string RequestorId { get; set; } = string.Empty;

        public ICollection<BorrowingDetail> BorrowingDetails { get; set; } = new List<BorrowingDetail>();
    }
}