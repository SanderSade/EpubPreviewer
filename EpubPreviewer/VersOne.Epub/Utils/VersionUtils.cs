using System.Reflection;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Utils
{
	internal static class VersionUtils
	{
		public static string GetVersionString(EpubVersion epubVersion)
		{
			var epubVersionType = typeof(EpubVersion);
			var fieldInfo = epubVersionType.GetRuntimeField(epubVersion.ToString());
			if (fieldInfo != null)
			{
				return fieldInfo.GetCustomAttribute<VersionStringAttribute>().Version;
			}

			return epubVersion.ToString();
		}
	}
}
