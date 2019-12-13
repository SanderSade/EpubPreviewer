using System;
using System.IO.Compression;
using System.Xml.Linq;
using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class RootFilePathReader
	{
		public static string GetRootFilePath(ZipArchive epubArchive)
		{
			const string epubContainerFilePath = "META-INF/container.xml";
			var containerFileEntry = epubArchive.GetEntry(epubContainerFilePath);
			if (containerFileEntry == null)
			{
				throw new Exception($"EPUB parsing error: {epubContainerFilePath} file not found in archive.");
			}

			XDocument containerDocument;
			using (var containerStream = containerFileEntry.Open())
			{
				containerDocument = XmlUtils.LoadDocument(containerStream);
			}

			XNamespace cnsNamespace = "urn:oasis:names:tc:opendocument:xmlns:container";
			var fullPathAttribute = containerDocument.Element(cnsNamespace + "container")?.Element(cnsNamespace + "rootfiles")
				?.Element(cnsNamespace + "rootfile")?.Attribute("full-path");

			if (fullPathAttribute == null)
			{
				throw new Exception("EPUB parsing error: root file path not found in the EPUB container.");
			}

			return fullPathAttribute.Value;
		}
	}
}
