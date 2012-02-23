using System;
using System.Web;
using System.Web.Mvc;
using LocalizationDemo.Utils;

namespace LocalizationDemo.Controllers
{
	// The only purpose of redirecting use is cookies cleanup
	// You don't need to use it real application
	public class RedirectController : Controller
	{
		public ActionResult Index()
		{
			HttpCookie language = Request.Cookies[CookieConstants.Language];
			HttpCookie isFirstTime = Request.Cookies[CookieConstants.IsFirstTime];
			if (language != null)
			{
				language.Expires = DateTime.Now.AddDays(-1);
				Response.Cookies.Add(language);
			}
			if (isFirstTime != null)
			{
				isFirstTime.Expires = DateTime.Now.AddDays(-1);
				Response.Cookies.Add(isFirstTime);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}