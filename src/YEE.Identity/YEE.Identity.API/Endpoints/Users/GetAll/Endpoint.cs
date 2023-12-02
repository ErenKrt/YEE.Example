using FastEndpoints;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Models.Services.Users.DTOs;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Users.GetAll
{
    public class Endpoint : Endpoint<GetAllUserRequest, PagedResult<UserDTO>>
    {
        public IUserService _userService { get; set; }
        public ICustomMapper _customMapper { get; set; }
        public override void Configure()
        {
            Get("/users/getAll");
            Version(1);
            Permissions("UserList");
        }

        public override async Task HandleAsync(GetAllUserRequest r, CancellationToken c)
        {
            var users = await _userService.GetAll(r);
            var mapped = _customMapper.Map<PagedResult<UserDTO>>(users);
            await SendAsync(mapped);
        }
    }
}
