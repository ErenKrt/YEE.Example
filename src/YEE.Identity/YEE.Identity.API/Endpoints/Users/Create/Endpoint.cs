using FastEndpoints;
using YEE.Identity.Application.Helpers;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Entities.Users;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Users.Create
{
    public class Endpoint : Endpoint<CreateOrUpdateUserRequest, bool>
    {

        public IUserService _userService { get; set; }
        public ICustomMapper _customMapper { get; set; }
        public IHasher _hasher { get; set; }
        public override void Configure()
        {
            Post("/users/create");
            Version(1);
            Permissions("UserCreate");
        }

        public override async Task HandleAsync(CreateOrUpdateUserRequest data, CancellationToken ct)
        {
            var mapped = _customMapper.Map<User>(data);
            mapped.Password = _hasher.Hash(data.Password);
            await _userService.Create(mapped);
            await SendAsync(true);
        }
    }
}
