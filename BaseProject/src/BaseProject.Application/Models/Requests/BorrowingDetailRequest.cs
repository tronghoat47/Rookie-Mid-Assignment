using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Requests
{
    public class BorrowingDetailRequest
    {
        public long BorrowingId { get; set; }
        public long BookId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ReturnedAt { get; set; } = DateTime.Now.AddDays(7);
        public string Status { get; set; } = string.Empty;
    }
}