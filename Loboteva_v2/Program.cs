using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LobotecaContext>(Options=>
    Options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"))
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inicio/Inicio");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");

app.Run();
