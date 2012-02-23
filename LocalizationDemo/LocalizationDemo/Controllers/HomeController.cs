using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalizationDemo.Models;
using LocalizationDemo.Utils;

namespace LocalizationDemo.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ChangeLanguage(ChangeLanguageModel model)
		{
			Response.Cookies.Add(new HttpCookie(CookieConstants.Language, model.Language));
			Response.Cookies.Add(new HttpCookie(CookieConstants.IsFirstTime, Boolean.FalseString));
			return Redirect(model.ReturnUrl);
		}
	}
}