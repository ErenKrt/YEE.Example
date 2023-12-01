using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using YEE.Identity.API.Models;
using YEE.Identity.Application.Models;

namespace YEE.Identity.API.Extensions
{
    public static class ErrorHandlerExtension
    {
        public static WebApplication UseErrorHandler(this WebApplication app)
        {

            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async ctx =>
                {
                    var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                    if (exHandlerFeature is not null)
                    {
                        var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                        var type = exHandlerFeature.Error.GetType();

                        var errors = new List<string>();
                        if (type == typeof(ValidationFailureException))
                        {
                            errors = ((ValidationFailureException)exHandlerFeature.Error)?.Failures?.Select(x => x.ErrorMessage).ToList();
                        }
                        else
                        {
                            errors.Add(exHandlerFeature.Error.Message);
                        }

                        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        ctx.Response.ContentType = "application/problem+json";

                        if (type == typeof(InfoException) || type == typeof(ValidationFailureException))
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.OK;
                            ctx.Response.ContentType = "application/json";
                        }

                        await ctx.Response.WriteAsJsonAsync(ApiResult<object>.Failure(errors));
                    }
                });
            });

            return app;
        }
    }
}
