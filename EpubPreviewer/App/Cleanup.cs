using System;
using System.IO;

namespace SanderSade.EpubPreviewer.App
{
	internal static class Cleanup
	{
		internal static void Run(int days)
		{
			var now = DateTimeOffset.UtcNow;
			var fromDays = TimeSpan.FromDays(days);
			var tempPath = Path.Combine(Path.GetTempPath(), nameof(EpubPreviewer));
			var bookFolders = Directory.GetDirectories(tempPath);
			foreach (var path in bookFolders)
			{
				var age = Directory.GetCreationTimeUtc(path);

				if (now - age > fromDays)
				{
					Directory.Delete(path, true);
				}
			}
		}
	}
}
