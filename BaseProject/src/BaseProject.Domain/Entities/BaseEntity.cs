﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}