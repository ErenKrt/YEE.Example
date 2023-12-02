using FastEndpoints;
using FastEndpoints.Security;
using Mapster;
using YEE.Identity.Application.Models.Services.Auth;
using YEE.Identity.Application.Models.Services.Users.DTOs;
using YEE.Identity.Application.Services.Impl;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.API.Endpoints.MQ.Get
{
    public class Endpoint : EndpointWithoutRequest<bool>
    {
        public IMQService _mqService { get; set; }
        public override void Configure()
        {
            Get("/mq/test");
            Version(1);
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            _mqService.CreateQueue("test");
            _mqService.SendMessage("test", "hi");

            await SendAsync(true);
        }
    }
}
