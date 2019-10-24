using System.Collections.Generic;
using System.Linq;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ncx;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Readers
{
	internal static class NavigationReader
	{
		public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef)
		{
			if (bookRef.Schema.Package.EpubVersion == EpubVersion.Epub2)
			{
				if (null != bookRef.Schema.Epub2Ncx)
					return GetNavigationItems(bookRef, bookRef.Schema.Epub2Ncx);
				return new List<EpubNavigationItemRef>(); // if Ncx is missing, return an empty list
			}

			return GetNavigationItems(bookRef, bookRef.Schema.Epub3NavDocument);
		}

		public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub2Ncx epub2Ncx)
		{
			return GetNavigationItems(bookRef, epub2Ncx.NavMap);
		}

		public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3NavDocument epub3NavDocument)
		{
			return GetNavigationItems(bookRef, epub3NavDocument.Navs.FirstOrDefault(nav => nav.Type == StructuralSemanticsProperty.Toc));
		}

		private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, List<Epub2NcxNavigationPoint> navigationPoints)
		{
			var result = new List<EpubNavigationItemRef>();
			if (navigationPoints != null)
				foreach (var navigationPoint in navigationPoints)
				{
					var navigationItemRef = EpubNavigationItemRef.CreateAsLink();
					navigationItemRef.Title = navigationPoint.NavigationLabels.First().Text;
					navigationItemRef.Link = new EpubNavigationItemLink(navigationPoint.Content.Source);
					navigationItemRef.HtmlContentFileRef = GetHtmlContentFileRef(bookRef, navigationItemRef.Link.ContentFileName);
					navigationItemRef.NestedItems = GetNavigationItems(bookRef, navigationPoint.ChildNavigationPoints);
					result.Add(navigationItemRef);
				}

			return result;
		}

		private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3Nav epub3Nav)
		{
			List<EpubNavigationItemRef> result;
			if (epub3Nav != null)
			{
				if (epub3Nav.Head != null)
				{
					result = new List<EpubNavigationItemRef>();
					var navigationItemRef = EpubNavigationItemRef.CreateAsHeader();
					navigationItemRef.Title = epub3Nav.Head;
					navigationItemRef.NestedItems = GetNavigationItems(bookRef, epub3Nav.Ol);
					result.Add(navigationItemRef);
				}
				else
				{
					result = GetNavigationItems(bookRef, epub3Nav.Ol);
				}
			}
			else
			{
				result = new List<EpubNavigationItemRef>();
			}

			return result;
		}

		private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3NavOl epub3NavOl)
		{
			var result = new List<EpubNavigationItemRef>();
			if (epub3NavOl != null && epub3NavOl.Lis != null)
				foreach (var epub3NavLi in epub3NavOl.Lis)
					if (epub3NavLi != null && (epub3NavLi.Anchor != null || epub3NavLi.Span != null))
					{
						if (epub3NavLi.Anchor != null)
						{
							var navAnchor = epub3NavLi.Anchor;
							var navigationItemRef = EpubNavigationItemRef.CreateAsLink();
							navigationItemRef.Title = GetFirstNonEmptyHeader(navAnchor.Text, navAnchor.Title, navAnchor.Alt);
							navigationItemRef.Link = new EpubNavigationItemLink(navAnchor.Href);
							navigationItemRef.HtmlContentFileRef = GetHtmlContentFileRef(bookRef, navigationItemRef.Link.ContentFileName);
							navigationItemRef.NestedItems = GetNavigationItems(bookRef, epub3NavLi.ChildOl);
							result.Add(navigationItemRef);
						}
						else if (epub3NavLi.Span != null)
						{
							var navSpan = epub3NavLi.Span;
							var navigationItemRef = EpubNavigationItemRef.CreateAsHeader();
							navigationItemRef.Title = GetFirstNonEmptyHeader(navSpan.Text, navSpan.Title, navSpan.Alt);
							navigationItemRef.NestedItems = GetNavigationItems(bookRef, epub3NavLi.ChildOl);
							result.Add(navigationItemRef);
						}
					}

			return result;
		}

		private static EpubTextContentFileRef GetHtmlContentFileRef(EpubBookRef bookRef, string contentFileName)
		{
			if (contentFileName == null) return null;
			if (!bookRef.Content.Html.TryGetValue(contentFileName, out var htmlContentFileRef)) return null;
			return htmlContentFileRef;
		}

		private static string GetFirstNonEmptyHeader(params string[] options)
		{
			foreach (var option in options)
				if (!string.IsNullOrEmpty(option))
					return option;
			return string.Empty;
		}
	}
}
