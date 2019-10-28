namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ncx
{
	public class Epub2NcxContent
	{
		public string Id { get; set; }
		public string Source { get; set; }


		public override string ToString()
		{
			return string.Concat("Source: " + Source);
		}
	}
}
