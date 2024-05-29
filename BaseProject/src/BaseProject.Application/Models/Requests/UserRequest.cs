using BaseProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Models.Requests
{
    public class UserRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Status { get; set; } = StatusUsersConstants.IN_ACTIVE;

        public byte? RoleId { get; set; } = 2;
    }
}