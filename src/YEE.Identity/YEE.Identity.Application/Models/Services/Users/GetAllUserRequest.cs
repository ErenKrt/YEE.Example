using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Application.Models.Services.Users
{
    public class GetAllUserRequest : PagedRequest
    {
        public int? UserID {  get; set; }
        public string? Term { get; set; }
        public string? Email { get; set; }
    }
}
