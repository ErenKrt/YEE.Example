using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Application.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
