using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing; // IUrlHelperFactory
using Microsoft.AspNetCore.Mvc.Rendering; // ViewContext
using Microsoft.AspNetCore.Mvc; // IUrlHelper
using OnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace OnlineStore.Infrastructure
{
	// Tag helper: Generates HTML markup for links.
	// Classes that transform/manipulate HTML elements in a view.
	// Needs to be registered before use.

	/*
	 * The attribute selects the HTML element to which the tag helper is applied.
	 * The range of elements that are transformed by a tag helper can be controlled using the
	 * HtmlTargetElement element
	 */
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		// A factory for creating IUrlHelper instances.
		// IUrlHelper: Defines the contract for the helper to build URLs for ASP.NET MVC within an application.
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
		{
			this.urlHelperFactory = urlHelperFactory;
		}

		// Context for view execution.
		// Uses a context object to get context data.
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext? ViewContext { get; set; }
		public PagingInfo? PageModel { get; set; }

		/*
		 * The name of the attribute is automatically converted from the default HTML style,
		 * bg-color, to the C# style, BgColor. */
		public string PageAction { get; set; }

		public bool PageClassesEnabled { get; set; } = false;
		public string PageClass { get; set; } = String.Empty;
		public string PageClassNormal { get; set; } = String.Empty;
		public string PageClassSelected { get; set; } = String.Empty;

		/*
		 * Populates a "div" element with "a" elements.
		 * TagHelper defines a Process method, which is overridden by subclasses to implement the behavior that transforms elements.
		 * 
		 * Tag helpers receive information about the element they are transforming through
		 * an instance of the TagHelperContext class, which is received as an argument to the
		 * Process method.
		 * 
		 * The Process method transforms an element by configuring the TagHelperOutput
		 * object that is received as an argument.
		 */
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (ViewContext != null && PageModel != null)
			{
				IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

				// Use the TagBuilder class to create elements programmatically.
				TagBuilder result = new TagBuilder("div");

				for (int i = 1; i <= PageModel.TotalPages; i++)
				{
					TagBuilder tag = new TagBuilder("a");

					/*
					 * IUrlHelper.Action(): Generates a URL with an absolute path for an action method, 
					 * which contains the action name, controller name, route values, protocol to use,
					 * host name
					 */
					tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
					if (PageClassesEnabled)
					{
						tag.AddCssClass(PageClass);
						tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
					}
					tag.InnerHtml.Append(i.ToString());
					result.InnerHtml.AppendHtml(tag);
				}

				output.Content.AppendHtml(result.InnerHtml);
			}
		}
	}
}
