using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using SanderSade.EpubPreviewer.VersOne.Epub;

namespace SanderSade.EpubPreviewer.Epub
{
	internal static class Previewer
	{
		/// <summary>
		///     Initiate preview
		/// </summary>
		internal static void Preview(string file)
		{
			if (!File.Exists(file))
			{
				throw new FileNotFoundException($"File \"{file}\" does not exist!", file);
			}

			// ReSharper disable once AssignNullToNotNullAttribute
			var cleaned = new string(Path.GetFileNameWithoutExtension(file).Where(char.IsLetterOrDigit).ToArray());

			//Adding guid in the end in case a different version of this file has already been extracted
			var outFolder = Path.Combine(Path.GetTempPath(), nameof(EpubPreviewer), cleaned, Guid.NewGuid().ToString("D"));
			Directory.CreateDirectory(outFolder);

#if DEBUG
			TimeSpan timeSpan = default, timeSpan2 = default;
#endif
			var t1 = Task.Run(() =>
			{
#if DEBUG
				var sw = Stopwatch.StartNew();
#endif
				ZipFile.ExtractToDirectory(file, outFolder);
#if DEBUG
				timeSpan = sw.Elapsed;
#endif
			});

			string outFilePath = null;
			var t2 = Task.Run(() =>
			{
#if DEBUG
				var sw = Stopwatch.StartNew();
#endif
				using (var epubBook = EpubReader.OpenBook(file))
				{
					var previewBuilder = new PreviewBuilder(epubBook, outFolder);
					outFilePath = previewBuilder.Build();
				}


#if DEBUG
				timeSpan2 = sw.Elapsed;
#endif
			});

			Task.WaitAll(t1, t2);

#if DEBUG
			Trace.WriteLine($"Unpacking: {timeSpan}\r\nPreview: {timeSpan2}");
#endif
			Process.Start($"file://{outFilePath}");
		}
	}
}
