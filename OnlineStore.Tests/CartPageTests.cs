using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using OnlineStore.Models;
using OnlineStore.Pages;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Tests
{
	public class CartPageTests
	{
		[Fact]
		public void Can_Load_Cart()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };
			Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
			mockRepo.Setup(m => m.Products).Returns((new Product[] { p1, p2 }).AsQueryable<Product>());

			Cart testCart = new Cart();
			testCart.AddItem(p1, 2);
			testCart.AddItem(p2, 1);

			Mock<ISession> mockSession = new Mock<ISession>();
			byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
			mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data!));

			Mock<HttpContext> mockContext = new Mock<HttpContext>();
			mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);

			CartModel cartModel = new CartModel(mockRepo.Object)
			{
				PageContext = new PageContext(new ActionContext
				{
					HttpContext = mockContext.Object,
					RouteData = new RouteData(),
					ActionDescriptor = new PageActionDescriptor()
				})
			};
			cartModel.OnGet("myUrl");

			Assert.Equal(2, cartModel.Cart?.Lines.Count());
			Assert.Equal("myUrl", cartModel.ReturnUrl);
		}

		[Fact]
		public void Can_Update_Cart()
		{
			Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
			mockRepo.Setup(m => m.Products).Returns((new Product[] { new Product { ProductId = 1, Name = "P1" } }).AsQueryable());

			Cart? testCart = new Cart();

			Mock<ISession> mockSession = new Mock<ISession>();
			mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
				.Callback<string, byte[]>((key, val) =>
				{
					/*
					 * Converts the byte[] val into a JSON string, then deserialize it into a Cart object.
					 *
					 * SetJson(Cart) => SetString(Cart -> JsonString) => SetByteArray(JsonString -> byte[])
					 * 
					 * val -> String -> Cart
					 */
					testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val));
				}
			);

			Mock<HttpContext> mockContext = new Mock<HttpContext>();
			mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);

			CartModel cartModel = new CartModel(mockRepo.Object)
			{
				PageContext = new PageContext(new ActionContext
				{
					HttpContext = mockContext.Object,
					RouteData = new RouteData(),
					ActionDescriptor = new PageActionDescriptor()
				})
			};
			cartModel.OnPost(1, "myUrl");

			Assert.Single(testCart.Lines);
			Assert.Equal("P1", testCart.Lines.First().Product.Name);
			Assert.Equal(1, testCart.Lines.First().Quantity);
		}
	}
}
