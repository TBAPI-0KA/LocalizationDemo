using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LocalizationDemo
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// All routes without language parameter should go first
			routes.MapRoute(
				null,
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = 1 },
				new { controller = @"\w{3,}", action = @"\w{3,}", id = @"\d+" }
			);

			routes.MapRoute(
				null,
				"{controller}/{action}",
				new { controller = "Home", action = "Index" },
				new { controller = @"\w{3,}", action = @"\w{3,}" }
			);

			// Duplicates of routes upper with language parameter added
			routes.MapRoute(
				null,
				"{language}/{controller}/{action}/{id}",
				new { language = "En", controller = "Home", action = "Index", id = 1 },
				// We suggest, that language always consists from uppercase and lowercase letters, i.e. En, Uk, Ru, etc
				// Controller and action name consist from three or more characters
				new { language = @"[A-Z][a-z]", controller = @"\w{3,}", action = @"\w{3,}", id = @"\d+" }
			);

			routes.MapRoute(
				null,
				"{language}/{controller}/{action}",
				new { language = "En", controller = "Home", action = "Index" },
				new { language = @"[A-Z][a-z]", controller = @"\w{3,}", action = @"\w{3,}" }
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}