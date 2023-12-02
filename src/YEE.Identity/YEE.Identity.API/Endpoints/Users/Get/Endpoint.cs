using FastEndpoints;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Models.Services.Users.DTOs;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Users.Get
{
    public class Endpoint : Endpoint<GetUserRequest, UserDTO>
    {
        public IUserService _userService { get; set; }
        public ICustomMapper _customMapper { get; set; }
        public override void Configure()
        {
            Get("/users/get");
            Version(1);
            //Permissions("UserGet");
        }

        public override async Task HandleAsync(GetUserRequest r, CancellationToken c)
        {
            var User = await _userService.Get(r.ID);
            var mapped = _customMapper.Map<UserDTO>(User);
            await SendAsync(mapped);
        }
    }
}
