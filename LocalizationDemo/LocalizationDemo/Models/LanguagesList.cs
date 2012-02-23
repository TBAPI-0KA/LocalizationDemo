using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalizationDemo.Models
{
	public static class LanguagesList
	{
		public static List<string> Get()
		{
			return new List<string> { "en", "ru", "uk" }; // Get the list from DB, cache, or any other data source
		}
	}
}