using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Entities
{
	public class EpubNavigationItemLink
	{
		public EpubNavigationItemLink()
		{
		}


		public EpubNavigationItemLink(string url)
		{
			var urlParser = new UrlParser(url);
			ContentFileName = urlParser.Path;
			Anchor = urlParser.Anchor;
		}


		public string ContentFileName { get; set; }
		public string Anchor { get; set; }
	}
}
