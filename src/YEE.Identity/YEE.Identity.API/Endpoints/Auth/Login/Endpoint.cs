using FastEndpoints;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Services.Interfaces;

namespace YEE.Identity.API.Endpoints.Auth.Login
{
    public class Endpoint : Endpoint<LoginRequest, LoginResponse>
    {
        public IAuthService _authService { get; set; }
        public override void Configure()
        {
            Post("/auth/login");
            Version(1);
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var res = await _authService.Login(req);
            await SendAsync(res);
        }
    }
}
