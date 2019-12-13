using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops;
using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class Epub3NavDocumentReader
	{
		public static Epub3NavDocument ReadEpub3NavDocument(ZipArchive epubArchive, string contentDirectoryPath, EpubPackage package)
		{
			var result = new Epub3NavDocument();
			var navManifestItem =
				package.Manifest.FirstOrDefault(item => item.Properties != null && item.Properties.Contains(ManifestProperty.Nav));

			if (navManifestItem == null)
			{
				if (package.EpubVersion == EpubVersion.Epub2)
				{
					return null;
				}

				throw new Exception("EPUB parsing error: NAV item not found in EPUB manifest.");
			}

			var navFileEntryPath = ZipPathUtils.Combine(contentDirectoryPath, navManifestItem.Href);
			var navFileEntry = epubArchive.GetEntry(navFileEntryPath);
			if (navFileEntry == null)
			{
				throw new Exception($"EPUB parsing error: navigation file {navFileEntryPath} not found in archive.");
			}

			if (navFileEntry.Length > int.MaxValue)
			{
				throw new Exception($"EPUB parsing error: navigation file {navFileEntryPath} is larger than 2 Gb.");
			}

			XDocument navDocument;
			using (var containerStream = navFileEntry.Open())
			{
				navDocument = XmlUtils.LoadDocument(containerStream);
			}

			var xhtmlNamespace = navDocument.Root.Name.Namespace;
			var htmlNode = navDocument.Element(xhtmlNamespace + "html");
			if (htmlNode == null)
			{
				throw new Exception("EPUB parsing error: navigation file does not contain html element.");
			}

			var bodyNode = htmlNode.Element(xhtmlNamespace + "body");
			if (bodyNode == null)
			{
				throw new Exception("EPUB parsing error: navigation file does not contain body element.");
			}

			result.Navs = new List<Epub3Nav>();
			var folder = ZipPathUtils.GetDirectoryPath(navManifestItem.Href);

			foreach (var navNode in bodyNode.Elements(xhtmlNamespace + "nav"))
			{
				var epub3Nav = ReadEpub3Nav(navNode);
				AdjustRelativePath(epub3Nav.Ol, folder);
				result.Navs.Add(epub3Nav);
			}

			return result;
		}


		private static void AdjustRelativePath(Epub3NavOl nav, string rootFolder)
		{
			if (nav == null)
			{
				return;
			}

			foreach (var li in nav.Lis)
			{
				if (li.Anchor?.Href != null && li.Anchor.Href.Length > 0 && li.Anchor.Href[0] != '/')
				{
					var relativeHref = $"{rootFolder}/{li.Anchor.Href}";
					li.Anchor.Href = relativeHref;
				}

				AdjustRelativePath(li.ChildOl, rootFolder);
			}
		}


		private static Epub3Nav ReadEpub3Nav(XElement navNode)
		{
			var epub3Nav = new Epub3Nav();
			foreach (var navNodeAttribute in navNode.Attributes())
			{
				var attributeValue = navNodeAttribute.Value;
				switch (navNodeAttribute.GetLowerCaseLocalName())
				{
					case "type":
						epub3Nav.Type = StructuralSemanticsPropertyParser.Parse(attributeValue);
						break;
					case "hidden":
						epub3Nav.IsHidden = true;
						break;
				}
			}

			foreach (var navChildNode in navNode.Elements())
			{
				switch (navChildNode.GetLowerCaseLocalName())
				{
					case "h1":
					case "h2":
					case "h3":
					case "h4":
					case "h5":
					case "h6":
						epub3Nav.Head = navChildNode.Value.Trim();
						break;
					case "ol":
						var epub3NavOl = ReadEpub3NavOl(navChildNode);
						epub3Nav.Ol = epub3NavOl;
						break;
				}
			}

			return epub3Nav;
		}


		private static Epub3NavOl ReadEpub3NavOl(XElement epub3NavOlNode)
		{
			var epub3NavOl = new Epub3NavOl();
			foreach (var navOlNodeAttribute in epub3NavOlNode.Attributes())
			{
				var attributeValue = navOlNodeAttribute.Value;
				switch (navOlNodeAttribute.GetLowerCaseLocalName())
				{
					case "hidden":
						epub3NavOl.IsHidden = true;
						break;
				}
			}

			epub3NavOl.Lis = new List<Epub3NavLi>();
			foreach (var navOlChildNode in epub3NavOlNode.Elements())
			{
				switch (navOlChildNode.GetLowerCaseLocalName())
				{
					case "li":
						var epub3NavLi = ReadEpub3NavLi(navOlChildNode);
						epub3NavOl.Lis.Add(epub3NavLi);
						break;
				}
			}

			return epub3NavOl;
		}


		private static Epub3NavLi ReadEpub3NavLi(XElement epub3NavLiNode)
		{
			var epub3NavLi = new Epub3NavLi();
			foreach (var navLiChildNode in epub3NavLiNode.Elements())
			{
				switch (navLiChildNode.GetLowerCaseLocalName())
				{
					case "a":
						var epub3NavAnchor = ReadEpub3NavAnchor(navLiChildNode);
						epub3NavLi.Anchor = epub3NavAnchor;
						break;
					case "span":
						var epub3NavSpan = ReadEpub3NavSpan(navLiChildNode);
						epub3NavLi.Span = epub3NavSpan;
						break;
					case "ol":
						var epub3NavOl = ReadEpub3NavOl(navLiChildNode);
						epub3NavLi.ChildOl = epub3NavOl;
						break;
				}
			}

			return epub3NavLi;
		}


		private static Epub3NavAnchor ReadEpub3NavAnchor(XElement epub3NavAnchorNode)
		{
			var epub3NavAnchor = new Epub3NavAnchor();
			foreach (var navAnchorNodeAttribute in epub3NavAnchorNode.Attributes())
			{
				var attributeValue = navAnchorNodeAttribute.Value;
				switch (navAnchorNodeAttribute.GetLowerCaseLocalName())
				{
					case "href":
						epub3NavAnchor.Href = attributeValue;
						break;
					case "title":
						epub3NavAnchor.Title = attributeValue;
						break;
					case "alt":
						epub3NavAnchor.Alt = attributeValue;
						break;
					case "type":
						epub3NavAnchor.Type = StructuralSemanticsPropertyParser.Parse(attributeValue);
						break;
				}
			}

			epub3NavAnchor.Text = epub3NavAnchorNode.Value.Trim();
			return epub3NavAnchor;
		}


		private static Epub3NavSpan ReadEpub3NavSpan(XElement epub3NavSpanNode)
		{
			var epub3NavSpan = new Epub3NavSpan();
			foreach (var navSpanNodeAttribute in epub3NavSpanNode.Attributes())
			{
				var attributeValue = navSpanNodeAttribute.Value;
				switch (navSpanNodeAttribute.GetLowerCaseLocalName())
				{
					case "title":
						epub3NavSpan.Title = attributeValue;
						break;
					case "alt":
						epub3NavSpan.Alt = attributeValue;
						break;
				}
			}

			epub3NavSpan.Text = epub3NavSpanNode.Value.Trim();
			return epub3NavSpan;
		}
	}
}
