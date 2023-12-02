using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Core.Entities.Users;

namespace YEE.Identity.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<PagedResult<User>> GetAll(GetAllUserRequest data);
        public Task<User> Get(int id);
        public Task Create(User entity);
        public Task Delete(int id);
        public Task Update(User entity);
    }
}
