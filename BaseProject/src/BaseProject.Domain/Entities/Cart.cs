using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Entities
{
    public class Cart
    {
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int DaysForBorrow { get; set; }
    }
}