using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Requests
{
    public class UserAddMoneyRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}