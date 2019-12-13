using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.Readers;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;

namespace SanderSade.EpubPreviewer.VersOne.Epub
{
	public static class EpubReader
	{
		/// <summary>
		///     Opens the book synchronously without reading its whole content. Holds the handle to the EPUB file.
		/// </summary>
		/// <param name="filePath">path to the EPUB file</param>
		/// <returns></returns>
		public static EpubBookRef OpenBook(string filePath)
		{
			return OpenBookAsync(filePath);
		}


		/// <summary>
		///     Opens the book asynchronously without reading its whole content. Holds the handle to the EPUB file.
		/// </summary>
		/// <param name="filePath">path to the EPUB file</param>
		/// <returns></returns>
		public static EpubBookRef OpenBookAsync(string filePath)
		{
			if (!File.Exists(filePath))
			{
				if (!filePath.StartsWith(@"\\?\")) // handle large file name lengths
				{
					filePath = @"\\?\" + filePath;
				}

				if (!File.Exists(filePath))
				{
					throw new FileNotFoundException("Specified epub file not found.", filePath);
				}
			}

			return OpenBook(GetZipArchive(filePath));
		}


		/// <summary>
		///     Opens the book asynchronously without reading its whole content.
		/// </summary>
		/// <param name="stream">seekable stream containing the EPUB file</param>
		/// <returns></returns>
		public static EpubBookRef OpenBookAsync(Stream stream)
		{
			return OpenBook(GetZipArchive(stream));
		}


		/// <summary>
		///     Opens the book synchronously and reads all of its content into the memory. Does not hold the handle to the EPUB
		///     file.
		/// </summary>
		/// <param name="filePath">path to the EPUB file</param>
		/// <returns></returns>
		public static EpubBook ReadBook(string filePath)
		{
			return ReadBookI(filePath);
		}


		/// <summary>
		///     Opens the book asynchronously and reads all of its content into the memory. Does not hold the handle to the EPUB
		///     file.
		/// </summary>
		/// <param name="filePath">path to the EPUB file</param>
		/// <returns></returns>
		public static EpubBook ReadBookI(string filePath)
		{
			var epubBookRef = OpenBookAsync(filePath);
			return ReadBook(epubBookRef);
		}


		private static EpubBookRef OpenBook(ZipArchive zipArchive, string filePath = null)
		{
			EpubBookRef result = null;
			try
			{
				result = new EpubBookRef(zipArchive);
				result.FilePath = filePath;
				result.Schema = SchemaReader.ReadSchema(zipArchive);
				result.Title = result.Schema.Package.Metadata.Titles.FirstOrDefault() ?? string.Empty;
				result.AuthorList = result.Schema.Package.Metadata.Creators.Select(creator => creator.Creator).ToList();
				result.Author = string.Join(", ", result.AuthorList);
				result.Content = ContentReader.ParseContentMap(result);
				return result;
			}
			catch
			{
				result?.Dispose();
				throw;
			}
		}


		private static EpubBook ReadBook(EpubBookRef epubBookRef)
		{
			var result = new EpubBook();
			using (epubBookRef)
			{
				result.FilePath = epubBookRef.FilePath;
				result.Schema = epubBookRef.Schema;
				result.Title = epubBookRef.Title;
				result.AuthorList = epubBookRef.AuthorList;
				result.Author = epubBookRef.Author;
				result.Content = ReadContent(epubBookRef.Content);
				//result.CoverImage = await epubBookRef.ReadCoverAsync().ConfigureAwait(false);
				var htmlContentFileRefs = epubBookRef.GetReadingOrder();
				result.ReadingOrder = ReadReadingOrder(result, htmlContentFileRefs);
				var navigationItemRefs = epubBookRef.GetNavigation();
				result.Navigation = ReadNavigation(result, navigationItemRefs);
			}

			return result;
		}


		private static ZipArchive GetZipArchive(string filePath)
		{
			return ZipFile.OpenRead(filePath);
		}


		private static ZipArchive GetZipArchive(Stream stream)
		{
			return new ZipArchive(stream, ZipArchiveMode.Read);
		}


		private static EpubContent ReadContent(EpubContentRef contentRef)
		{
			return new EpubContent { Html = ReadTextContentFiles(contentRef.Html) };
		}


		private static Dictionary<string, EpubTextContentFile> ReadTextContentFiles(Dictionary<string, EpubTextContentFileRef> textContentFileRefs)
		{
			var result = new Dictionary<string, EpubTextContentFile>();
			foreach (var textContentFileRef in textContentFileRefs)
			{
				var textContentFile = new EpubTextContentFile
				{
					FileName = textContentFileRef.Value.FileName,
					ContentType = textContentFileRef.Value.ContentType,
					ContentMimeType = textContentFileRef.Value.ContentMimeType
				};

				//		textContentFile.Content = await textContentFileRef.Value.ReadContentAsTextAsync().ConfigureAwait(false);
				result.Add(textContentFileRef.Key, textContentFile);
			}

			return result;
		}


		private static List<EpubTextContentFile> ReadReadingOrder(EpubBook epubBook, List<EpubTextContentFileRef> htmlContentFileRefs)
		{
			return htmlContentFileRefs.Select(htmlContentFileRef => epubBook.Content.Html[htmlContentFileRef.FileName]).ToList();
		}


		private static List<EpubNavigationItem> ReadNavigation(EpubBook epubBook, List<EpubNavigationItemRef> navigationItemRefs)
		{
			var result = new List<EpubNavigationItem>();
			foreach (var navigationItemRef in navigationItemRefs)
			{
				var navigationItem = new EpubNavigationItem(navigationItemRef.Type)
				{
					Title = navigationItemRef.Title,
					Link = navigationItemRef.Link
				};

				if (navigationItemRef.HtmlContentFileRef != null)
				{
					navigationItem.HtmlContentFile = epubBook.Content.Html[navigationItemRef.HtmlContentFileRef.FileName];
				}

				navigationItem.NestedItems = ReadNavigation(epubBook, navigationItemRef.NestedItems);
				result.Add(navigationItem);
			}

			return result;
		}
	}
}
