using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Entities
{
    public class OrderDetail
    {
        public long OrderId { get; set; }
        public Order? Order { get; set; }
        public long BookId { get; set; }
        public Book? Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}