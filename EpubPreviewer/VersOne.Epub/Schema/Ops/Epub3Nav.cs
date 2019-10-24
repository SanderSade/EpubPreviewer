using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops
{
	public class Epub3Nav
	{
		public StructuralSemanticsProperty? Type { get; set; }
		public bool IsHidden { get; set; }
		public string Head { get; set; }
		public Epub3NavOl Ol { get; set; }
	}
}
