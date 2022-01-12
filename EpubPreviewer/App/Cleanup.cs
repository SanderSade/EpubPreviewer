using System;
using System.IO;

namespace SanderSade.EpubPreviewer.App
{
	internal static class Cleanup
	{
		internal static void Run(int days)
		{
			var tempPath = Path.Combine(Path.GetTempPath(), nameof(EpubPreviewer));

			if (!Directory.Exists(tempPath))
				return;


			var now = DateTimeOffset.UtcNow;
			var fromDays = TimeSpan.FromDays(days);

			foreach (var path in Directory.GetDirectories(tempPath))
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
