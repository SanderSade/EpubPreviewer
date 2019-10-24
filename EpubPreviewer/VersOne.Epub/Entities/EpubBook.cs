using System.Collections.Generic;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Entities
{
	public class EpubBook
	{
		public string FilePath { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public List<string> AuthorList { get; set; }
		public List<EpubTextContentFile> ReadingOrder { get; set; }
		public List<EpubNavigationItem> Navigation { get; set; }
		public EpubContent Content { get; set; }
		public EpubSchema Schema { get; set; }
	}
}
