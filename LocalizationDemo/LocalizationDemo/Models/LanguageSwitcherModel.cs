using System.Collections.Generic;

namespace LocalizationDemo.Models
{
	public class LanguageSwitcherModel
	{
		public string Url { get; set; }
		public IEnumerable<string> Languages { get; set; }
		public bool RenderForms { get; set; }
	}
}