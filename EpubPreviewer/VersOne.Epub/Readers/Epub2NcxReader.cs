using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ncx;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf;
using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class Epub2NcxReader
	{
		public static Epub2Ncx ReadEpub2Ncx(ZipArchive epubArchive, string contentDirectoryPath, EpubPackage package)
		{
			var result = new Epub2Ncx();
			var tocId = package.Spine.Toc;
			if (string.IsNullOrEmpty(tocId)) return null;

			var tocManifestItem = package.Manifest.FirstOrDefault(item => item.Id.CompareOrdinalIgnoreCase(tocId));
			if (tocManifestItem == null) throw new Exception($"EPUB parsing error: TOC item {tocId} not found in EPUB manifest.");

			var tocFileEntryPath = ZipPathUtils.Combine(contentDirectoryPath, tocManifestItem.Href);
			var tocFileEntry = epubArchive.GetEntry(tocFileEntryPath);
			if (tocFileEntry == null) throw new Exception($"EPUB parsing error: TOC file {tocFileEntryPath} not found in archive.");
			if (tocFileEntry.Length > int.MaxValue) throw new Exception($"EPUB parsing error: TOC file {tocFileEntryPath} is larger than 2 Gb.");

			XDocument containerDocument;
			using (var containerStream = tocFileEntry.Open())
			{
				containerDocument = XmlUtils.LoadDocument(containerStream);
			}

			XNamespace ncxNamespace = "http://www.daisy.org/z3986/2005/ncx/";
			var ncxNode = containerDocument.Element(ncxNamespace + "ncx");
			if (ncxNode == null) throw new Exception("EPUB parsing error: TOC file does not contain ncx element.");

			var headNode = ncxNode.Element(ncxNamespace + "head");
			if (headNode == null) throw new Exception("EPUB parsing error: TOC file does not contain head element.");

			var navigationHead = ReadNavigationHead(headNode);
			result.Head = navigationHead;
			var docTitleNode = ncxNode.Element(ncxNamespace + "docTitle");
			if (docTitleNode == null) throw new Exception("EPUB parsing error: TOC file does not contain docTitle element.");

			var navigationDocTitle = ReadNavigationDocTitle(docTitleNode);
			result.DocTitle = navigationDocTitle;
			result.DocAuthors = new List<Epub2NcxDocAuthor>();
			foreach (var docAuthorNode in ncxNode.Elements(ncxNamespace + "docAuthor"))
			{
				var navigationDocAuthor = ReadNavigationDocAuthor(docAuthorNode);
				result.DocAuthors.Add(navigationDocAuthor);
			}

			var navMapNode = ncxNode.Element(ncxNamespace + "navMap");
			if (navMapNode == null) throw new Exception("EPUB parsing error: TOC file does not contain navMap element.");

			var navMap = ReadNavigationMap(navMapNode);
			result.NavMap = navMap;
			var pageListNode = ncxNode.Element(ncxNamespace + "pageList");
			if (pageListNode != null)
			{
				var pageList = ReadNavigationPageList(pageListNode);
				result.PageList = pageList;
			}

			result.NavLists = new List<Epub2NcxNavigationList>();
			foreach (var navigationListNode in ncxNode.Elements(ncxNamespace + "navList"))
			{
				var navigationList = ReadNavigationList(navigationListNode);
				result.NavLists.Add(navigationList);
			}

			return result;
		}


		private static Epub2NcxHead ReadNavigationHead(XElement headNode)
		{
			var result = new Epub2NcxHead();
			foreach (var metaNode in headNode.Elements())
			{
				if (metaNode.CompareNameTo("meta"))
				{
					var meta = new Epub2NcxHeadMeta();
					foreach (var metaNodeAttribute in metaNode.Attributes())
					{
						var attributeValue = metaNodeAttribute.Value;
						switch (metaNodeAttribute.GetLowerCaseLocalName())
						{
							case "name":
								meta.Name = attributeValue;
								break;
							case "content":
								meta.Content = attributeValue;
								break;
							case "scheme":
								meta.Scheme = attributeValue;
								break;
						}
					}

					if (string.IsNullOrWhiteSpace(meta.Name)) throw new Exception("Incorrect EPUB navigation meta: meta name is missing.");
					if (meta.Content == null) throw new Exception("Incorrect EPUB navigation meta: meta content is missing.");

					result.Add(meta);
				}
			}

			return result;
		}


		private static Epub2NcxDocTitle ReadNavigationDocTitle(XElement docTitleNode)
		{
			var result = new Epub2NcxDocTitle();
			foreach (var textNode in docTitleNode.Elements())
			{
				if (textNode.CompareNameTo("text"))
					result.Add(textNode.Value);
			}

			return result;
		}


		private static Epub2NcxDocAuthor ReadNavigationDocAuthor(XElement docAuthorNode)
		{
			var result = new Epub2NcxDocAuthor();
			foreach (var textNode in docAuthorNode.Elements())
			{
				if (textNode.CompareNameTo("text"))
					result.Add(textNode.Value);
			}

			return result;
		}


		private static Epub2NcxNavigationMap ReadNavigationMap(XElement navigationMapNode)
		{
			var result = new Epub2NcxNavigationMap();
			foreach (var navigationPointNode in navigationMapNode.Elements())
			{
				if (navigationPointNode.CompareNameTo("navPoint"))
				{
					var navigationPoint = ReadNavigationPoint(navigationPointNode);
					if (null != navigationPoint) result.Add(navigationPoint);
				}
			}

			return result;
		}


		private static Epub2NcxNavigationPoint ReadNavigationPoint(XElement navigationPointNode)
		{
			var result = new Epub2NcxNavigationPoint();
			foreach (var navigationPointNodeAttribute in navigationPointNode.Attributes())
			{
				var attributeValue = navigationPointNodeAttribute.Value;
				switch (navigationPointNodeAttribute.GetLowerCaseLocalName())
				{
					case "id":
						result.Id = attributeValue;
						break;
					case "class":
						result.Class = attributeValue;
						break;
					case "playorder":
						result.PlayOrder = attributeValue;
						break;
				}
			}

			if (string.IsNullOrWhiteSpace(result.Id)) throw new Exception("Incorrect EPUB navigation point: point ID is missing.");

			result.NavigationLabels = new List<Epub2NcxNavigationLabel>();
			result.ChildNavigationPoints = new List<Epub2NcxNavigationPoint>();
			foreach (var navigationPointChildNode in navigationPointNode.Elements())
			{
				switch (navigationPointChildNode.GetLowerCaseLocalName())
				{
					case "navlabel":
						var navigationLabel = ReadNavigationLabel(navigationPointChildNode);
						result.NavigationLabels.Add(navigationLabel);
						break;
					case "content":
						var content = ReadNavigationContent(navigationPointChildNode);
						result.Content = content;
						break;
					case "navpoint":
						var childNavigationPoint = ReadNavigationPoint(navigationPointChildNode);
						if (null != childNavigationPoint) result.ChildNavigationPoints.Add(childNavigationPoint);
						break;
				}
			}

			if (!result.NavigationLabels.Any())
				throw new Exception($"EPUB parsing error: navigation point {result.Id} should contain at least one navigation label.");

			if (result.Content == null)
			{
#if STRICTEPUB
                throw new Exception($"EPUB parsing error: navigation point {result.Id} should contain content.");
#else
				return null;
#endif
			}

			return result;
		}


		private static Epub2NcxNavigationLabel ReadNavigationLabel(XElement navigationLabelNode)
		{
			var result = new Epub2NcxNavigationLabel();
			var navigationLabelTextNode = navigationLabelNode.Element(navigationLabelNode.Name.Namespace + "text");
			if (navigationLabelTextNode == null) throw new Exception("Incorrect EPUB navigation label: label text element is missing.");

			result.Text = navigationLabelTextNode.Value;
			return result;
		}


		private static Epub2NcxContent ReadNavigationContent(XElement navigationContentNode)
		{
			var result = new Epub2NcxContent();
			foreach (var navigationContentNodeAttribute in navigationContentNode.Attributes())
			{
				var attributeValue = navigationContentNodeAttribute.Value;
				switch (navigationContentNodeAttribute.GetLowerCaseLocalName())
				{
					case "id":
						result.Id = attributeValue;
						break;
					case "src":
						result.Source = attributeValue;
						break;
				}
			}

			if (string.IsNullOrWhiteSpace(result.Source)) throw new Exception("Incorrect EPUB navigation content: content source is missing.");

			return result;
		}


		private static Epub2NcxPageList ReadNavigationPageList(XElement navigationPageListNode)
		{
			var result = new Epub2NcxPageList();
			foreach (var pageTargetNode in navigationPageListNode.Elements())
			{
				if (pageTargetNode.CompareNameTo("pageTarget"))
				{
					var pageTarget = ReadNavigationPageTarget(pageTargetNode);
					result.Add(pageTarget);
				}
			}

			return result;
		}


		private static Epub2NcxPageTarget ReadNavigationPageTarget(XElement navigationPageTargetNode)
		{
			var result = new Epub2NcxPageTarget();
			foreach (var navigationPageTargetNodeAttribute in navigationPageTargetNode.Attributes())
			{
				var attributeValue = navigationPageTargetNodeAttribute.Value;
				switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
				{
					case "id":
						result.Id = attributeValue;
						break;
					case "value":
						result.Value = attributeValue;
						break;
					case "type":
						Epub2NcxPageTargetType type;
						if (Enum.TryParse(attributeValue, true, out type))
							result.Type = type;
						else
							result.Type = Epub2NcxPageTargetType.Unknown;

						break;
					case "class":
						result.Class = attributeValue;
						break;
					case "playorder":
						result.PlayOrder = attributeValue;
						break;
				}
			}

			if (result.Type == default) throw new Exception("Incorrect EPUB navigation page target: page target type is missing.");

			result.NavigationLabels = new List<Epub2NcxNavigationLabel>();
			foreach (var navigationPageTargetChildNode in navigationPageTargetNode.Elements())
			{
				switch (navigationPageTargetChildNode.GetLowerCaseLocalName())
				{
					case "navlabel":
						var navigationLabel = ReadNavigationLabel(navigationPageTargetChildNode);
						result.NavigationLabels.Add(navigationLabel);
						break;
					case "content":
						var content = ReadNavigationContent(navigationPageTargetChildNode);
						result.Content = content;
						break;
				}
			}

			if (!result.NavigationLabels.Any()) throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required.");

			return result;
		}


		private static Epub2NcxNavigationList ReadNavigationList(XElement navigationListNode)
		{
			var result = new Epub2NcxNavigationList();
			foreach (var navigationListNodeAttribute in navigationListNode.Attributes())
			{
				var attributeValue = navigationListNodeAttribute.Value;
				switch (navigationListNodeAttribute.GetLowerCaseLocalName())
				{
					case "id":
						result.Id = attributeValue;
						break;
					case "class":
						result.Class = attributeValue;
						break;
				}
			}

			foreach (var navigationListChildNode in navigationListNode.Elements())
			{
				switch (navigationListChildNode.GetLowerCaseLocalName())
				{
					case "navlabel":
						var navigationLabel = ReadNavigationLabel(navigationListChildNode);
						result.NavigationLabels.Add(navigationLabel);
						break;
					case "navTarget":
						var navigationTarget = ReadNavigationTarget(navigationListChildNode);
						result.NavigationTargets.Add(navigationTarget);
						break;
				}
			}

			if (!result.NavigationLabels.Any()) throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required.");

			return result;
		}


		private static Epub2NcxNavigationTarget ReadNavigationTarget(XElement navigationTargetNode)
		{
			var result = new Epub2NcxNavigationTarget();
			foreach (var navigationPageTargetNodeAttribute in navigationTargetNode.Attributes())
			{
				var attributeValue = navigationPageTargetNodeAttribute.Value;
				switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
				{
					case "id":
						result.Id = attributeValue;
						break;
					case "value":
						result.Value = attributeValue;
						break;
					case "class":
						result.Class = attributeValue;
						break;
					case "playorder":
						result.PlayOrder = attributeValue;
						break;
				}
			}

			if (string.IsNullOrWhiteSpace(result.Id)) throw new Exception("Incorrect EPUB navigation target: navigation target ID is missing.");

			foreach (var navigationTargetChildNode in navigationTargetNode.Elements())
			{
				switch (navigationTargetChildNode.GetLowerCaseLocalName())
				{
					case "navlabel":
						var navigationLabel = ReadNavigationLabel(navigationTargetChildNode);
						result.NavigationLabels.Add(navigationLabel);
						break;
					case "content":
						var content = ReadNavigationContent(navigationTargetChildNode);
						result.Content = content;
						break;
				}
			}

			if (!result.NavigationLabels.Any()) throw new Exception("Incorrect EPUB navigation target: at least one navLabel element is required.");

			return result;
		}
	}
}
