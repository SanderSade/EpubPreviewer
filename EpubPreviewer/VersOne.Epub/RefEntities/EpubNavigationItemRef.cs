using System.Collections.Generic;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;

namespace SanderSade.EpubPreviewer.VersOne.Epub.RefEntities
{
	public class EpubNavigationItemRef
	{
		public EpubNavigationItemRef(EpubNavigationItemType type)
		{
			Type = type;
		}


		public EpubNavigationItemType Type { get; }
		public string Title { get; set; }
		public EpubNavigationItemLink Link { get; set; }
		public EpubTextContentFileRef HtmlContentFileRef { get; set; }
		public List<EpubNavigationItemRef> NestedItems { get; set; }


		public static EpubNavigationItemRef CreateAsHeader()
		{
			return new EpubNavigationItemRef(EpubNavigationItemType.Header);
		}


		public static EpubNavigationItemRef CreateAsLink()
		{
			return new EpubNavigationItemRef(EpubNavigationItemType.Link);
		}


		public override string ToString()
		{
			return $"Type: {Type}, Title: {Title}, NestedItems.Count: {NestedItems.Count}";
		}
	}
}
