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
    public class RatingRequest
    {
        public string UserId { get; set; } = string.Empty;
        public long BookId { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5")]
        public int Rate { get; set; }

        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}