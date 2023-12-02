using Microsoft.EntityFrameworkCore;
using YEE.Inventory.API.Background;
using YEE.Inventory.API.Database;
using YEE.Inventory.API.Models;
using YEE.Inventory.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("YEE"));

builder.Services.AddDbContext<DatabaseContext>(ops =>
{
    ops.UseNpgsql(builder.Configuration.GetSection("YEE").GetSection("Database").GetSection("ConnectionString").Value);
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddHostedService<RabbitMQConsumer>();

var app = builder.Build();

app.Run();
