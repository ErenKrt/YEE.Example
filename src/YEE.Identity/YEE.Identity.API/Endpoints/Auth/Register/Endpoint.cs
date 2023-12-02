using FastEndpoints;
using YEE.Identity.Application.Models.Services.Auth;
using YEE.Identity.Application.Services.Interfaces;

namespace YEE.Identity.API.Endpoints.Auth.Register
{
    public class Endpoint : Endpoint<RegisterRequest, bool>
    {
        public IAuthService _authService { get; set; }
        public override void Configure()
        {
            Post("/auth/register");
            Version(1);
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            await SendAsync(await _authService.Register(req));
        }
    }
}
