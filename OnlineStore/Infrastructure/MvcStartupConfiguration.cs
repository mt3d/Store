﻿namespace OnlineStore.Infrastructure
{
	public class MvcStartupConfiguration : IStartupConfiguration
	{
		public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllersWithViews();

			// TODO: Configure AddSessionStateTempDataProvider or AddCookieTempDataProvider

			services.AddRazorPages();

			// TODO: AddMvcOptions
			// TODO: Add fluent validation
			// TODO: Add all validators
			// TODO: AddControllersAsServices()

			// TODO: Add Web Encoders
		}

		public void ConfigureApp(IApplicationBuilder app)
		{
			// No need to configure anything
		}

		public StartupOrder Order => StartupOrder.Mvc;
	}
}
