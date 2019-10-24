using System.IO.Compression;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class SchemaReader
	{
		public static EpubSchema ReadSchema(ZipArchive epubArchive)
		{
			var result = new EpubSchema();
			var rootFilePath = RootFilePathReader.GetRootFilePath(epubArchive);
			var contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
			result.ContentDirectoryPath = contentDirectoryPath;
			var package = PackageReader.ReadPackage(epubArchive, rootFilePath);
			result.Package = package;
			result.Epub2Ncx = Epub2NcxReader.ReadEpub2Ncx(epubArchive, contentDirectoryPath, package);
			result.Epub3NavDocument = Epub3NavDocumentReader.ReadEpub3NavDocument(epubArchive, contentDirectoryPath, package);
			return result;
		}
	}
}
