namespace YEE.Inventory.API.Database
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
