using System.Collections.Generic;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class ContentReader
	{
		public static EpubContentRef ParseContentMap(EpubBookRef bookRef)
		{
			var result = new EpubContentRef { Html = new Dictionary<string, EpubTextContentFileRef>() };
			foreach (var manifestItem in bookRef.Schema.Package.Manifest)
			{
				var fileName = manifestItem.Href;
				var contentMimeType = manifestItem.MediaType;
				var contentType = GetContentTypeByContentMimeType(contentMimeType);
				switch (contentType)
				{
					case EpubContentType.Xhtml11:
					case EpubContentType.Css:
					case EpubContentType.Oeb1Document:
					case EpubContentType.Oeb1Css:
					case EpubContentType.Xml:
					case EpubContentType.Dtbook:
					case EpubContentType.DtbookNcx:
						var epubTextContentFile = new EpubTextContentFileRef(bookRef)
						{
							FileName = fileName,
							ContentMimeType = contentMimeType,
							ContentType = contentType
						};

						if (contentType == EpubContentType.Xhtml11)
						{
							result.Html[fileName] = epubTextContentFile;
						}

						break;
				}
			}

			return result;
		}


		private static EpubContentType GetContentTypeByContentMimeType(string contentMimeType)
		{
			switch (contentMimeType.ToLowerInvariant())
			{
				case "application/xhtml+xml":
					return EpubContentType.Xhtml11;
				case "application/x-dtbook+xml":
					return EpubContentType.Dtbook;
				case "application/x-dtbncx+xml":
					return EpubContentType.DtbookNcx;
				case "text/x-oeb1-document":
					return EpubContentType.Oeb1Document;
				case "application/xml":
					return EpubContentType.Xml;
				case "text/css":
					return EpubContentType.Css;
				case "text/x-oeb1-css":
					return EpubContentType.Oeb1Css;
				case "image/gif":
					return EpubContentType.ImageGif;
				case "image/jpeg":
					return EpubContentType.ImageJpeg;
				case "image/png":
					return EpubContentType.ImagePng;
				case "image/svg+xml":
					return EpubContentType.ImageSvg;
				case "font/truetype":
				case "application/x-font-truetype":
					return EpubContentType.FontTruetype;
				case "font/opentype":
				case "application/vnd.ms-opentype":
					return EpubContentType.FontOpentype;
				default:
					return EpubContentType.Other;
			}
		}
	}
}
