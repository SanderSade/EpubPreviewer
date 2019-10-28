using System;
using System.IO;
using System.Reflection;

namespace SanderSade.EpubPreviewer.Epub
{
	internal static class ResourceReader
	{
		/// <summary>
		///     Read embedded resource
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		internal static string Read(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var path = $"SanderSade.EpubPreviewer.Epub.Embedded.{name}";

			using (var stream = assembly.GetManifestResourceStream(path))
			{
				using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}
