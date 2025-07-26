using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models
{
	public class EFOrderRepository : IOrderRepository
	{
		private StoreDbContext context;

		public EFOrderRepository(StoreDbContext context)
		{
			this.context = context;
		}

		public IQueryable<Order> Orders => context.Orders
			.Include(o => o.Lines) // Eagrly load the Lines collection
			.ThenInclude(l => l.Product); // Load the Product associated with each Line

		public void SaveOrder(Order order)
		{
			// Tells EF that the entitites exist in the database.
			context.AttachRange(order.Lines.Select(l => l.Product));

			if (order.OrderId == 0)
			{
				context.Orders.Add(order);
			}
			context.SaveChanges();

		}
	}
}
