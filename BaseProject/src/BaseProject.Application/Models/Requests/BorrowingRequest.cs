using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Requests
{
    public class BorrowingRequest
    {
        [Required]
        public string RequestorId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<BorrowingDetail> BorrowingDetails { get; set; } = new List<BorrowingDetail>();
    }
}