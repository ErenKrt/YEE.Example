using YEE.Inventory.API.Database;

namespace YEE.Inventory.API.Models.Entities
{
    public class Item : BaseEntity
    {
        public virtual int OwnerID { get; set; }
        public virtual string Name {  get; set; }
        public virtual string Quantity { get; set; }
    }
}
