using Moq;
using OnlineStore.Models;
using OnlineStore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStore.Tests
{
	public class NavigationMenuViewComponentTests
	{
		[Fact]
		public void Can_Select_Categories()
		{
			// Arrange
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1", Category = "Apples"},
				new Product {ProductId = 2, Name = "P2", Category = "Apples"},
				new Product {ProductId = 3, Name = "P3", Category = "Plums"},
				new Product {ProductId = 4, Name = "P4", Category = "Oranges"},
			}).AsQueryable<Product>());

			NavigationMenuViewComponent menu = new NavigationMenuViewComponent(mock.Object);

			// Act
			//string[] results = ((IEnumerable<string>?)(menu.Invoke() as ViewComponentResult)?.ViewData.Model ?? Enumerable.Empty<string>()).ToArray();

			string[] results = ((IEnumerable<string>?)(menu.Invoke() as ViewViewComponentResult)?.ViewData?.Model ?? Enumerable.Empty<string>()).ToArray();

			// Assert
			Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
		}

		[Fact]
		public void Indicates_Selected_Category()
		{
			string categoryToSelect = "Apples";
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] {
				new Product {ProductId = 1, Name = "P1", Category = "Apples"},
				new Product {ProductId = 4, Name = "P2", Category = "Oranges"},
			}).AsQueryable<Product>());

			NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

			// This is how view components receive their context data.
			target.ViewComponentContext = new ViewComponentContext {
				// View-specific context data
				ViewContext = new ViewContext { 
					RouteData = new Microsoft.AspNetCore.Routing.RouteData()
				}
			};
			target.RouteData.Values["category"] = categoryToSelect;

			string? result = (string?)(target.Invoke() as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

			Assert.Equal(categoryToSelect, result);
		}
	}
}
