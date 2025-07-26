using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
	public class OrderController : Controller
	{
		public ViewResult Checkout() => View(new Order());
	}
}
