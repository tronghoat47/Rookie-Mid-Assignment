﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Entities
{
    public class LovedBook
    {
        public long BookId { get; set; }
        public Book? Book { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
    }
}