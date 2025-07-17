using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						Name = "IPhone 16",
						Description = "",
						Category = "Phones",
						Price = 1000
					},
					new Product
					{
						Name = "Samsung S25",
						Description = "",
						Category = "Phones",
						Price = 800
					},
					new Product
					{
						Name = "Samsung S25 Ultra",
						Description = "",
						Category = "Phones",
						Price = 900
					},
					new Product
					{
						Name = "Google Pixel 9a",
						Description = "",
						Category = "Phones",
						Price = 400
					},
					new Product
					{
						Name = "Samsung Galaxy Watch Ultra",
						Description = "",
						Category = "Smart Watches",
						Price = 486
					}
				);
				context.SaveChanges();

				Console.WriteLine(context.Products);
			}

			Console.WriteLine(context.Products.ToArray().Length);
		}
	}
}
