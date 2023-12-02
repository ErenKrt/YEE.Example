using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Core.Entities
{
    public class UserPermission : BaseEntity
    {
        public virtual int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual int PermissionID { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
