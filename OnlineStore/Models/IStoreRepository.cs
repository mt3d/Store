namespace OnlineStore.Models
{
	public interface IStoreRepository
	{
		IQueryable<Product> Products { get; }
	}
}
