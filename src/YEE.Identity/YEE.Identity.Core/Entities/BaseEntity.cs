using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Core.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }

    public class BaseEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey ID { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int> { }

    public abstract class CreationAuditedEntity : BaseEntity
    {
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
