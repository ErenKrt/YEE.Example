﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Core.Entities.Users
{
    public class Permission : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
    }
}
