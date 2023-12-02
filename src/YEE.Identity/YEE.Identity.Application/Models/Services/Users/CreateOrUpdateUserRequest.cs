using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Core.Entities;
using YEE.Identity.Core.Entities.Users;

namespace YEE.Identity.Application.Models.Services.Users
{
    public class CreateOrUpdateUserRequest : BaseEntity<int?>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
    }
}
