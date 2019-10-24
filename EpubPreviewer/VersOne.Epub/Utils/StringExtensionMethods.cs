using System;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Utils
{
	public static class StringExtensionMethods
	{
		public static bool CompareOrdinalIgnoreCase(this string source, string value)
		{
			return string.Compare(source, value, StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}
