using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Xml;
using SanderSade.EpubPreviewer.Epub.Domain;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class Previewer: IDisposable
	{
		private ZipArchive _zipFile;
		private readonly string _file;

		public Previewer(string file)
		{
			_file = file;
		}

		/// <summary>
		/// Initiate preview
		/// </summary>
		internal void Preview()
		{

			if (!File.Exists(_file))
				throw new FileNotFoundException($"File \"{_file}\" does not exist!", _file);

			_zipFile = ZipFile.OpenRead(_file);
			var opf = GetOpfPath();
			if (string.IsNullOrWhiteSpace(opf))
				throw new EpubException($"{_file} does not have valid link to OPF!");

			var toc = GetToc(opf);

			var outFolder = ExtractAll();

			var previewBuilder = new PreviewBuilder(toc, outFolder);
			var outFilePath = previewBuilder.Build();

			Process.Start($"file://{outFilePath}");

		}

		private string ExtractAll()
		{
			//Adding guid in the end in case a different version of this file has already been extracted
			// ReSharper disable once AssignNullToNotNullAttribute
			var outFolder = Path.Combine(Path.GetTempPath(), nameof(EpubPreviewer), Path.GetFileNameWithoutExtension(_file), Guid.NewGuid().ToString("D"));
			Directory.CreateDirectory(outFolder);
			_zipFile.ExtractToDirectory(outFolder);

			return outFolder;
		}

		/// <summary>
		/// Read the OPF. From here we split to v3 and v2 epub (yay...)
		/// </summary>
		/// <param name="opfPath"></param>
		private Toc GetToc(string opfPath)
		{
			var opf = _zipFile.GetEntry(opfPath);

			if (opf == null)
				throw new EpubException($"Epub file {_file} does not contain required Open Packaging Format file \"{opfPath}\"");

			var xmlDoc = new XmlDocument();
			using (var stream = opf.Open())
			{
				xmlDoc.Load(stream);
			}

			var version = xmlDoc.GetElementsByTagName("package")[0]?.Attributes?["version"]?.Value;

			//assume v2 if not possible to determine
			if (version == null || !version.StartsWith("3"))
			{
				return new TocV2Reader(_file, _zipFile).Parse(xmlDoc);
			}

			return new TocV3Reader(_file, _zipFile).Parse(xmlDoc);
		}


		private string GetOpfPath()
		{
			var container = _zipFile.GetEntry(@"META-INF/container.xml");
			if (container == null)
				throw new EpubException($"Epub file {_file} does not contain required metainfo file \"META-INF/container.xml\"");

			var xmlDoc = new XmlDocument();
			using (var stream = container.Open())
			{
				xmlDoc.Load(stream);
			}
			
			//we can have multiple rootfiles, but for our purposes, let's just get first
			return xmlDoc.GetElementsByTagName("rootfile")[0]?.Attributes?["full-path"]?.Value;
		}

		public void Dispose()
		{
			_zipFile?.Dispose();
		}
	}
}
