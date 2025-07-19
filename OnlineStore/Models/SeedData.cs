﻿using Microsoft.EntityFrameworkCore;

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
					Name = "iPhone 15 Pro",
					Description = "Apple's latest flagship smartphone with A17 Pro chip and titanium design.",
					Category = "Smartphone",
					Price = 999
				},
				new Product
				{
					Name = "Samsung Galaxy S24 Ultra",
					Description = "High-end Android phone with 200MP camera and S Pen support.",
					Category = "Smartphone",
					Price = 1199
				},
				new Product
				{
					Name = "Google Pixel 8 Pro",
					Description = "Google’s premium phone with Tensor G3 chip and advanced AI features.",
					Category = "Smartphone",
					Price = 999
				},
				new Product
				{
					Name = "OnePlus 12",
					Description = "Flagship killer with Snapdragon 8 Gen 3 and ultra-fast charging.",
					Category = "Smartphone",
					Price = 799
				},
				new Product
				{
					Name = "Apple Watch Series 9",
					Description = "Smartwatch with S9 chip, double tap gesture, and improved Siri.",
					Category = "Smartwatch",
					Price = 399
				},
				new Product
				{
					Name = "Samsung Galaxy Watch 6",
					Description = "Wear OS-powered smartwatch with health tracking and sleek design.",
					Category = "Smartwatch",
					Price = 329
				},
				new Product
				{
					Name = "Fitbit Versa 4",
					Description = "Fitness-focused smartwatch with heart rate monitoring and sleep tracking.",
					Category = "Smartwatch",
					Price = 229
				},
				new Product
				{
					Name = "Garmin Venu 3",
					Description = "Premium smartwatch with GPS, health monitoring, and AMOLED display.",
					Category = "Smartwatch",
					Price = 449
				},
				new Product
				{
					Name = "Sony WF-1000XM5",
					Description = "Noise-canceling wireless earbuds with superior sound quality.",
					Category = "Earbuds",
					Price = 299
				},
				new Product
				{
					Name = "Apple AirPods Pro (2nd Gen)",
					Description = "Wireless earbuds with active noise cancellation and MagSafe case.",
					Category = "Earbuds",
					Price = 249
				},
				new Product
				{
					Name = "Samsung Galaxy Buds2 Pro",
					Description = "Hi-Fi wireless earbuds with ANC and 360 Audio.",
					Category = "Earbuds",
					Price = 229
				},
				new Product
				{
					Name = "Nothing Ear (2)",
					Description = "Stylish wireless earbuds with great audio and unique transparent design.",
					Category = "Earbuds",
					Price = 149
				},
				new Product
				{
					Name = "Dell XPS 13 (2024)",
					Description = "Compact and powerful Windows ultrabook with Intel Core Ultra processor.",
					Category = "Laptop",
					Price = 1199
				},
				new Product
				{
					Name = "MacBook Air 15\" (M2, 2023)",
					Description = "Slim and lightweight laptop with Apple's M2 chip and 18-hour battery.",
					Category = "Laptop",
					Price = 1299
				},
				new Product
				{
					Name = "ASUS ROG Zephyrus G14",
					Description = "Powerful gaming laptop with AMD Ryzen 9 and RTX 4060 GPU.",
					Category = "Laptop",
					Price = 1599
				},
				new Product
				{
					Name = "Lenovo Yoga 9i",
					Description = "2-in-1 premium laptop with rotating soundbar and 4K OLED display.",
					Category = "Laptop",
					Price = 1399
				},
				new Product
				{
					Name = "Amazon Kindle Paperwhite (11th Gen)",
					Description = "E-reader with 6.8\" display, adjustable warm light, and weeks-long battery.",
					Category = "E-Reader",
					Price = 139
				},
				new Product
				{
					Name = "Logitech MX Master 3S",
					Description = "High-performance wireless mouse with ultra-quiet clicks and ergonomic design.",
					Category = "Accessories",
					Price = 99
				},
				new Product
				{
					Name = "Anker 737 Power Bank (PowerCore 24K)",
					Description = "Portable charger with 140W output and fast-charging support.",
					Category = "Accessories",
					Price = 159
				},
				new Product
				{
					Name = "Google Nest Hub (2nd Gen)",
					Description = "Smart display with Google Assistant and sleep sensing.",
					Category = "Smart Home",
					Price = 99
				}
				);
				context.SaveChanges();

				Console.WriteLine(context.Products);
			}

			Console.WriteLine(context.Products.ToArray().Length);
		}
	}
}
