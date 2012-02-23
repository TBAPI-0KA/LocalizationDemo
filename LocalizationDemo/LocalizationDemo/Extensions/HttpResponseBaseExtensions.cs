using System.Web;
using LocalizationDemo.Utils;

namespace LocalizationDemo.Extensions
{
	public static class HttpResponseBaseExtensions
	{
		public static void SetLanguageCookies(this HttpResponseBase response, string language, bool isFirstTime)
		{
			response.Cookies.Add(new HttpCookie(CookieConstants.Language, language));
			response.Cookies.Add(new HttpCookie(CookieConstants.IsFirstTime, isFirstTime.ToString()));
		}
	}
}