namespace SanderSade.EpubPreviewer.VersOne.Epub.Utils
{
	internal static class ZipPathUtils
	{
		public static string GetDirectoryPath(string filePath)
		{
			var lastSlashIndex = filePath.LastIndexOf('/');
			if (lastSlashIndex == -1)
				return string.Empty;

			return filePath.Substring(0, lastSlashIndex);
		}


		public static string Combine(string directory, string fileName)
		{
			if (string.IsNullOrEmpty(directory)) return fileName;

			while (fileName.StartsWith("../"))
			{
				var idx = directory.LastIndexOf("/");
				directory = idx > 0 ? directory.Substring(0, idx) : string.Empty;
				fileName = fileName.Substring(3);
			}

			return string.IsNullOrEmpty(directory) ? fileName : string.Concat(directory, "/", fileName);
		}
	}
}
