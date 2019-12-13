using System.Collections.Generic;
using System.Linq;
using System.Text;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class TocBuilder
	{
		private readonly EpubBookRef _ebook;
		private readonly StringBuilder _sb;


		public TocBuilder(EpubBookRef ebook)
		{
			_ebook = ebook;
			_sb = new StringBuilder(2048);
		}


		internal string FirstLink { get; set; }


		internal string Parse()
		{
			var navigation = _ebook.GetNavigation();
			var readingOrder = _ebook.GetReadingOrder();

			if (navigation.Count == 0 && readingOrder.Count == 0)
			{
				return BuildFromContent(_ebook.Content.Html.Values.ToList());
			}

			if (readingOrder.Count == 0)
			{
				return BuildFromNavigation(navigation);
			}

			if (navigation.Count == 0)
			{
				return BuildFromContent(readingOrder);
			}

			return BuildFullToc(readingOrder, navigation);
		}


		/// <summary>
		///     Build full TOC, containing entries from both navigation and reading order
		/// </summary>
		private string BuildFullToc(List<EpubTextContentFileRef> readingOrder, List<EpubNavigationItemRef> navigation)
		{
			var readingFiles = readingOrder.Select(x => x.FileName).ToList();


			_sb.AppendLine("<ul>");

			foreach (var item in navigation)
			{
				BuildNavigationWithRecursion(readingFiles, item);
			}

			if (readingFiles.Count > 0)
			{
				_sb.AppendLine("<ul>");
				readingFiles.ForEach(contentFile => { _sb.AppendLine($"<li><a href=\"{contentFile}\">{contentFile}</a></li>"); });
				_sb.AppendLine("</ul>");
			}

			_sb.AppendLine("</ul>");
			return _sb.ToString();
		}


		private void BuildNavigationWithRecursion(List<string> readingFiles, EpubNavigationItemRef item)
		{
			BuildNavigationWithReadingFile(readingFiles, item);
			if (item.NestedItems != null && item.NestedItems.Count > 0)
			{
				_sb.AppendLine("<ul>");
				foreach (var nestedItem in item.NestedItems)
				{
					BuildNavigationWithRecursion(readingFiles, nestedItem);
				}

				_sb.AppendLine("</ul>");
			}
		}


		/// <summary>
		///     Ensure that all entries in reading order are represented in file
		/// </summary>
		private void BuildNavigationWithReadingFile(List<string> readingFiles, EpubNavigationItemRef item)
		{
			var fileName = (item?.Link?.ContentFileName ?? item?.HtmlContentFileRef?.FileName)?.TrimStart('/');

			var readingFile = readingFiles.IndexOf(fileName);

			switch (readingFile)
			{
				case -1: //no matching entry in reading order
					BuidNavigation(item);
					break;

				case 0: //entry is where we expected
				{
					readingFiles.RemoveAt(0);
					BuidNavigation(item);
					break;
				}
				default: //extra entries in the reading order, add to previous entry as subentries
				{
					readingFiles.RemoveAt(readingFile);
					_sb.AppendLine("<ul>");
					for (var i = 0; i < readingFile; i++)
					{
						var contentFile = readingFiles[i];
						if (_ebook.Content.Html.Values.Any(x => x.FileName == contentFile))
						{
							_sb.AppendLine($"<li><a href=\"{contentFile}\">{contentFile}</a></li>");
						}
					}

					_sb.AppendLine("</ul>");
					readingFiles.RemoveRange(0, readingFile);
					BuidNavigation(item);
					break;
				}
			}
		}


		/// <summary>
		///     TOC/navigation is missing - build from reading order or html files
		/// </summary>
		private string BuildFromContent(List<EpubTextContentFileRef> contentFiles)
		{
			_sb.AppendLine("<ul>");
			FirstLink = contentFiles[0].FileName;

			foreach (var contentFile in contentFiles)
			{
				_sb.AppendLine($"<li><a href=\"{contentFile.FileName}\">{contentFile.FileName}</a></li>");
			}

			_sb.AppendLine("</ul>");

			return _sb.ToString();
		}


		/// <summary>
		///     Navigation exists, but reading order is missing
		/// </summary>
		private string BuildFromNavigation(List<EpubNavigationItemRef> navigation)
		{
			_sb.AppendLine("<ul>");
			foreach (var item in navigation.ToList())
			{
				BuildWithRecursion(item);
			}

			_sb.AppendLine("</ul>");
			return _sb.ToString();
		}


		private void BuildWithRecursion(EpubNavigationItemRef item)
		{
			BuidNavigation(item);
			if (item.NestedItems != null && item.NestedItems.Count > 0)
			{
				foreach (var nestedItem in item.NestedItems)
				{
					BuildWithRecursion(nestedItem);
				}
			}
		}


		private void BuidNavigation(EpubNavigationItemRef item)
		{
			string BuildContentLink()
			{
				if (string.IsNullOrWhiteSpace(item.Link.Anchor))
				{
					return item.Link.ContentFileName.TrimStart('/', '\\');
				}

				return $"{item.Link.ContentFileName.TrimStart('/', '\\')}#{item.Link.Anchor}";
			}

			if (!string.IsNullOrWhiteSpace(item.Title))
			{
				if (item.Link != null && !string.IsNullOrWhiteSpace(item.Link.ContentFileName))
				{
					var buildContentLink = BuildContentLink();
					if (FirstLink == null)
					{
						FirstLink = buildContentLink;
						_sb.AppendLine($"<li><a href=\"{buildContentLink}\" class=\"highlight\">{item.Title}</a></li>");
					}
					else
					{
						_sb.AppendLine($"<li><a href=\"{buildContentLink}\">{item.Title}</a></li>");
					}
				}
				else
				{
					_sb.AppendLine($"<li><h3>{item.Title}</h3></li>");
				}
			}
		}
	}
}
