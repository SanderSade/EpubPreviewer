namespace SanderSade.EpubPreviewer.VersOne.Epub.Utils
{
	internal class UrlParser
	{
		public UrlParser(string url)
		{
			if (url == null)
			{
				Path = null;
				Anchor = null;
			}
			else
			{
				var anchorCharIndex = url.IndexOf('#');
				if (anchorCharIndex == -1)
				{
					Path = url;
					Anchor = null;
				}
				else
				{
					Path = url.Substring(0, anchorCharIndex);
					Anchor = url.Substring(anchorCharIndex + 1);
				}
			}
		}

		public string Path { get; }
		public string Anchor { get; }
	}
}
