using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Models
{
    public class BookFilterDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int? ReleaseYearFrom { get; set; }
        public int? ReleaseYearTo { get; set; }
        public string CategoryName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}