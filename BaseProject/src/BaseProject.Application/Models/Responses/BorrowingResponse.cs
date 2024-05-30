using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Responses
{
    public class BorrowingResponse
    {
        public long Id { get; set; }

        [Required]
        public string RequestorId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusBorrowing.PENDING;
        public string? ApproverId { get; set; } = string.Empty;
        public ICollection<BorrowingDetailResponse> BorrowingDetails { get; set; } = new List<BorrowingDetailResponse>();
    }
}