using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using YEE.Identity.API.Extensions;
using YEE.Identity.API.Models;
using YEE.Identity.Application;
using YEE.Identity.Application.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("YEE"));

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.MaxEndpointVersion = 1;
        o.DocumentSettings = s =>
        {
            s.DocumentName = "Release 1.0";
            s.Title = "YEE API";
            s.Version = "V1.0";
        };
        o.ShortSchemaNames = true;
    });

builder.Services.AddJWTBearerAuth(builder.Configuration.GetValue<string>("YEE:JWTKey"), bearerEvents: (x) =>
{
    x.OnForbidden = context =>
    {
        return context.Response.WriteAsJsonAsync(ApiResult<object>.Failure("Yetkisiz eriþim."));
    };
}).AddAuthorization();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddHelpers();
builder.Services.AddServices();
builder.Services.AddMapper();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(x =>
{
    x.Endpoints.RoutePrefix = "api";
    x.Versioning.Prefix = "v";
    x.Versioning.PrependToRoute = true;

    x.Endpoints.Configurator = ep =>
    {
        ep.DontCatchExceptions();
    };

    x.Errors.UseProblemDetails();
    x.Serializer.UseCustomResponse();
});

app.UseSwaggerGen();
app.UseErrorHandler();
app.Run();
