using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Application.Models
{
    public class PagedRequest
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 15;
    }
}
