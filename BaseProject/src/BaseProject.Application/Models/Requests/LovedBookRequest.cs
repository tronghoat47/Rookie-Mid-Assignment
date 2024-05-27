using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Requests
{
    public class LovedBookRequest
    {
        public string UserId { get; set; } = string.Empty;
        public long BookId { get; set; }
    }
}