using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops
{
	public class Epub3NavAnchor
	{
		public string Href { get; set; }
		public string Text { get; set; }
		public string Title { get; set; }
		public string Alt { get; set; }
		public StructuralSemanticsProperty? Type { get; set; }
	}
}
