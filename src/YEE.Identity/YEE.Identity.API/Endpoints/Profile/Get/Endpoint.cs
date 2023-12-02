using FastEndpoints;
using FastEndpoints.Security;
using Mapster;
using YEE.Identity.Application.Models.Services.Auth;
using YEE.Identity.Application.Models.Services.Users.DTOs;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Profile.Get
{
    public class Endpoint : EndpointWithoutRequest<UserDTO>
    {
        public IUserService _userService { get; set; }
        public ICustomMapper _customMapper { get; set; }
        public override void Configure()
        {
            Get("/profile/get");
            Version(1);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var user = await _userService.Get(User.ClaimValue("ID").Adapt<int>());
            var mapped = _customMapper.Map<UserDTO>(user);
            await SendAsync(mapped);
        }
    }
}
