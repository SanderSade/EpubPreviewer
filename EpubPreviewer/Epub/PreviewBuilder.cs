using System;
using System.IO;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class PreviewBuilder
	{
		private readonly EpubBookRef _ebook;
		private readonly string _outFolder;


		public PreviewBuilder(EpubBookRef ebook, string outFolder)
		{
			_ebook = ebook;
			_outFolder = outFolder;
		}


		public string Build()
		{
			var tocBuilder = new TocBuilder(_ebook);
			var toc = tocBuilder.Parse();

			var html = ResourceReader.Read("EpubPreview.html");
			html = html.Replace("{{title}}", _ebook.Title);
			html = html.Replace("{{author}}", _ebook.Author);


			html = html.Replace("{{toc}}", toc);
			var path = Path.Combine(_outFolder, _ebook.Schema.ContentDirectoryPath).Replace("\\", "\\\\");
			html = html.Replace("{{path}}", path);
			html = html.Replace("{{framesrc}}", $"file://{Path.Combine(_outFolder, _ebook.Schema.ContentDirectoryPath, tocBuilder.FirstLink ?? string.Empty)}");
			var htmlPath = Path.Combine(_outFolder, "EpubPreview.html");
			File.WriteAllText(htmlPath, html);
			File.WriteAllText(Path.Combine(_outFolder, "EpubPreview.css"), ResourceReader.Read("EpubPreview.css"));


			var customCssPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EpubPreviewer.css");
			//custom CSS
			if (File.Exists(customCssPath))
				File.Copy(customCssPath, Path.Combine(_outFolder, "EpubPreviewer.css"));

			return htmlPath;
		}
	}
}
