using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using OnlineStore.Infrastructure;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests
{
	public class PageLinkTagHelperTests
	{
		[Fact]
		public void Can_Generate_Page_Links()
		{
			// Arrange
			var urlHelper = new Mock<IUrlHelper>();

			/*
			 * It.IsAny<UrlActionContext>() tells Moq: "this applies to any argument of type 
			 * UrlActionContext.
			 * 
			 * return different values on successive calls to urlHelper.Object.Action(...)
			 */
			urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>())).
				Returns("Test/Page1").
				Returns("Test/Page2").
				Returns("Test/Page3");

			var urlHelperFactory = new Mock<IUrlHelperFactory>();
			urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

			var viewContext = new Mock<ViewContext>();

			PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
			{
				PageModel = new PagingInfo
				{
					CurrentPage = 2,
					TotalItems = 28,
					ItemsPerPage = 10
				},
				ViewContext = viewContext.Object,
				PageAction = "Test"
			};

			/*
			 * The context in which a tag helper executes.
			 */
			TagHelperContext ctx = new TagHelperContext(
				new TagHelperAttributeList(), // An empty list of attributes on the HTML element
				new Dictionary<object, object>(), // A dictionary shared between tag helpers to pass data around during execution
				"");

			// the inner content of the tag (e.g., inside <div>inner content</div>)
			var content = new Mock<TagHelperContent>();

			TagHelperOutput output = new TagHelperOutput("div",
				new TagHelperAttributeList(),
				(cache, encoder) => Task.FromResult(content.Object));

			// Act
			helper.Process(ctx, output);

			// Assert
			Assert.Equal(@"<a href=""Test/Page1"">1</a>"
				+ @"<a href=""Test/Page2"">2</a>"
				+ @"<a href=""Test/Page3"">3</a>",
				output.Content.GetContent());
		}
	}
}
