using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
	public class Product
	{
		public long? ProductId { get; set; }
		public string Name { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;

		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }
		public string Category { get; set; } = String.Empty;

		// public originalStore name & link (below Product Name)
		// byline

		// public averageCustomerReviews
		// public int number of ratings;
		// public [] images;

		// public SpecialProductOverview
		// PressureCooker (Brand, Capacity, Material, Color, FinishType, ProductDimensions, Wattage, Item Weight)b
	}
}
