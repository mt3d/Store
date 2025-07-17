using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts => {
	opts.UseSqlServer(builder.Configuration["ConnectionStrings:OnlineStoreConnection"]);
});
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index" });
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();