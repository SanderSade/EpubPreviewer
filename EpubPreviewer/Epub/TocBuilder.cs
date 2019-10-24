using System.Collections.Generic;
using System.Linq;
using System.Text;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.RefEntities;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class TocBuilder
	{
		private readonly EpubBookRef _ebook;

		public TocBuilder(EpubBookRef ebook)
		{
			_ebook = ebook;
		}

		internal string FirstLink { get; set; }

		internal string Parse()
		{
			var navigation = _ebook.GetNavigation();
			if (navigation?.Count > 0)
				return BuildFromNavigation(navigation);

			var readingOrder = _ebook.GetReadingOrder();
			if (readingOrder?.Count > 0)
				return BuildFromContent(readingOrder);

			if (_ebook.Content?.Html?.Count > 0)
				return BuildFromContent(_ebook.Content.Html.Values.ToList());

			return null;
		}

		private string BuildFromContent(List<EpubTextContentFileRef> contentFiles)
		{
			var sb = new StringBuilder("<ul>");

			FirstLink = contentFiles[0].FileName;

			foreach (var contentFile in contentFiles) sb.AppendLine($"<li><a href=\"{contentFile.FileName}\">{contentFile.FileName}</a></li>");

			sb.AppendLine("</ul>");

			return sb.ToString();
		}

		private string BuildFromNavigation(List<EpubNavigationItemRef> navigation)
		{
			var sb = new StringBuilder(navigation.Count * 128);
			sb.AppendLine("<ul>");
			foreach (var item in navigation.ToList())
				if (item.NestedItems != null && item.NestedItems.Count > 0)
					foreach (var nestedItem in item.NestedItems)
						BuidNavigation(sb, nestedItem);
				else
					BuidNavigation(sb, item);

			sb.AppendLine("</ul>");
			return sb.ToString();
		}

		private void BuidNavigation(StringBuilder sb, EpubNavigationItemRef item)
		{
			string BuildContentLink()
			{
				if (string.IsNullOrWhiteSpace(item.Link.Anchor))
					return item.Link.ContentFileName.TrimStart('/', '\\');

				return $"{item.Link.ContentFileName.TrimStart('/', '\\')}#{item.Link.Anchor}";
			}

			if (!string.IsNullOrWhiteSpace(item.Title))
			{
				if (item.Link != null && !string.IsNullOrWhiteSpace(item.Link.ContentFileName))
				{
					sb.AppendLine($"<li><a href=\"{BuildContentLink()}\">{item.Title}</a></li>");
					if (FirstLink == null)
						FirstLink = BuildContentLink();
				}
				else
				{
					sb.AppendLine($"<li><h3>{item.Title}</h3></li>");
				}
			}

			if (item.NestedItems != null && item.NestedItems.Count > 0)
			{
				sb.AppendLine("<ul>");
				foreach (var sub in item.NestedItems) BuidNavigation(sb, sub);

				sb.AppendLine("</ul>");
			}
		}
	}
}
