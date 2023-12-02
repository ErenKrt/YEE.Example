using Microsoft.EntityFrameworkCore;
using YEE.Inventory.API.Database;
using YEE.Inventory.API.Models.Entities;

namespace YEE.Inventory.API.Services
{
    public interface IItemService
    {
        public Task<List<Item>> GetAll();
        public Task<Item> Get(int id);
        public Task Create(Item entity);
        public Task Delete(int id);
        public Task Update(Item entity);
        public Task DeleteFromUserID(int id);
    }

    public class ItemService : IItemService
    {
        public IGenericRepository<Item> _itemRepository { get; set; }

        public ItemService(IGenericRepository<Item> userRepository)
        {
            _itemRepository = userRepository;
        }

        public async Task<List<Item>> GetAll()
        {
            var items = await _itemRepository.GetAll().ToListAsync();
            return items;
        }

        public async Task<Item> Get(int ID)
        {
            return await _itemRepository.FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task Create(Item user)
        {
            await _itemRepository.InsertAsync(user);
        }

        public async Task Delete(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        public async Task Update(Item user)
        {
            await _itemRepository.UpdateAsync(user);
        }
        public async Task DeleteFromUserID(int id)
        {
            await _itemRepository.GetAll().Where(x => x.OwnerID == id).ExecuteDeleteAsync();
        }
    }
}
