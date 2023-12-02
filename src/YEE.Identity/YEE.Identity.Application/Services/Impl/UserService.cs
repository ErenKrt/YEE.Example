using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.DataAccess.EntityFramework.Interfaces;
using YEE.Identity.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using YEE.Identity.Core.Entities.Users;

namespace YEE.Identity.Application.Services.Impl
{
    public class UserService : IUserService
    {
        public IGenericRepository<User> _userRepository { get; set; }

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResult<User>> GetAll(GetAllUserRequest r)
        {
            var filteredUsers = _userRepository.GetAll();
            var pagedUsers = filteredUsers.PageBy(r);

            var totalCount = filteredUsers.Count();
            var items = await pagedUsers.ToListAsync();

            return new PagedResult<User>(items, totalCount);
        }

        public async Task<User> Get(int ID)
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task Create(User user)
        {
            await _userRepository.InsertAsync(user);
        }

        public async Task Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task Update(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
