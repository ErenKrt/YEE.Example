﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Core.Entities;
using YEE.Identity.Core.Entities.Users;

namespace YEE.Identity.Application.Models.Services.Users.DTOs
{
    public class UserDTO : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual UserStatus Status { get; set; }
    }
}
