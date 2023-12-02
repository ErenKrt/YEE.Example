using FastEndpoints;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Models.Services.Users;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.Users.Update
{
    public class Endpoint : Endpoint<CreateOrUpdateUserRequest, bool>
    {

        public IUserService _userService { get; set; }
        public ICustomMapper _customMapper { get; set; }
        public override void Configure()
        {
            Post("/users/update");
            Version(1);
            Permissions("UserUpdate");
        }

        public override async Task HandleAsync(CreateOrUpdateUserRequest data, CancellationToken ct)
        {
            var entity = await _userService.Get(data.ID.Value);
            if (entity == null)
            {
                throw new InfoException("There is no entity with this ID");
            }

            var mapped = _customMapper.Map(data, entity);
            await _userService.Update(mapped);
            await SendAsync(true);
        }
    }
}
