using FastEndpoints;
using FastEndpoints.Security;
using Mapster;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Users.Delete
{
    public class Endpoint : Endpoint<DeleteUserRequest, bool>
    {
        public IUserService _userService { get; set; }
        public override void Configure()
        {
            Delete("/users/delete");
            Version(1);
        }

        public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        {
            await _userService.Delete(req.ID);
            await SendAsync(true);
        }
    }
}
