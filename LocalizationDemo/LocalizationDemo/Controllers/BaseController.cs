using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LocalizationDemo.Extensions;
using LocalizationDemo.Utils;
using LocalizationDemo.Models;

namespace LocalizationDemo.Controllers
{
	public abstract class BaseController : Controller
	{
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);

			if (!ControllerContext.IsChildAction)
			{
				bool renderForms = false;

				HttpCookie languageCookie = Request.Cookies.Get(CookieConstants.Language);
				object languageParameterObj = requestContext.RouteData.Values[CookieConstants.Language.ToLower()];

				IEnumerable<string> languagesList = LanguagesList.Get().Select(x => x.ToLower());
				string language = languagesList.First(); // Default language;

				if (languageCookie == null)
				{
					// No cookie, no param
					if (languageParameterObj == null)
					{
						if (Request.UserLanguages != null)
						{
							string userLanguage = Request.UserLanguages
								.Select(x => x.Substring(0, 2).ToLower()) // Get first two letters of user language
								.FirstOrDefault(languagesList.Contains); // Get appropriate, defined by user, language, or null
							// This loop code is human-readable version of LINQ-expression upper
							/*foreach (string userLanguage in Request.UserLanguages.Select(x => x.Substring(0, 2).ToLower()))
							{
								if (languagesList.Contains(userLanguage))
								{
									userLanguage = language;
									break;
								}
							}*/
							if (!String.IsNullOrEmpty(userLanguage))
							{
								language = userLanguage;
							}

						}
						Response.SetLanguageCookies(language, true);
					}
					// No cookie, param
					else
					{
						string languageParameter = languageParameterObj.ToString().ToLower();
						if (languagesList.Contains(languageParameter))
						{
							language = languageParameter;
						}
						Response.SetLanguageCookies(language, true);
					}
				}
				else
				{
					renderForms = true;
					// Cookie, no param
					if (languageParameterObj == null)
					{
						string languageCookieValue = languageCookie.Value.ToLower();
						if (languagesList.Contains(languageCookieValue))
						{
							language = languageCookieValue;
						}
					}
					// Cookie, param
					else
					{
						HttpCookie isFirstTimeCookie = Request.Cookies.Get(CookieConstants.IsFirstTime);
						bool isFirstTime = isFirstTimeCookie != null ? Boolean.Parse(isFirstTimeCookie.Value) : true;
						if (isFirstTime)
						{
							string languageParameter = languageParameterObj.ToString().ToLower();
							if (languagesList.Contains(languageParameter))
							{
								language = languageParameter;
							}
							Response.SetLanguageCookies(language, false);
						}
						else
						{
							string languageCookieValue = languageCookie.Value.ToLower();
							if (languagesList.Contains(languageCookieValue))
							{
								language = languageCookieValue;
							}
						}
					}
				}

				CultureInfo cultureInfo = new CultureInfo(language);
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
				Thread.CurrentThread.CurrentCulture = cultureInfo;

				ViewBag.RenderForms = renderForms;
			}
		}
	}
}