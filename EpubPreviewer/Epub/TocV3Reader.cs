using System;
using System.IO.Compression;
using System.Xml;
using SanderSade.EpubPreviewer.Epub.Domain;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class TocV3Reader
	{
		private string _file;
		private ZipArchive _zipFile;

		public TocV3Reader(string file, ZipArchive zipFile)
		{
			_file = file;
			_zipFile = zipFile;
		}

		public Toc Parse(XmlDocument xmlDoc)
		{
			throw new NotImplementedException();
		}
	}
}
