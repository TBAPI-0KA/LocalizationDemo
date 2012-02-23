using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalizationDemo.Models;

namespace LocalizationDemo.Controllers
{
	public class WidgetController : BaseController
	{
		[ChildActionOnly]
		public ActionResult LanguageSwitcher()
		{
			string url = Request.RawUrl;
			if (url.Length > 3 && url[3] == '/')
			{
				url = url.Substring(3);
			}

			LanguageSwitcherModel model = new LanguageSwitcherModel
			{
				RenderForms = (bool)ControllerContext.ParentActionViewContext.Controller.ViewBag.RenderForms,
				Url = url,
				Languages = LanguagesList.Get()
			};

			return PartialView(model);
		}
	}
}