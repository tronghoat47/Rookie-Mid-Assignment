﻿using BaseProject.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        public string Status { get; set; } = StatusUsersConstants.IN_ACTIVE;

        public decimal Balance { get; set; } = 0;

        public byte RoleId { get; set; } = 0;
        public Role? Role { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<LovedBook> LovedBook { get; set; } = new List<LovedBook>();
    }
}