using System.Collections.Generic;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public class EpubSpine : List<EpubSpineItemRef>
	{
		public string Id { get; set; }
		public PageProgressionDirection? PageProgressionDirection { get; set; }
		public string Toc { get; set; }
	}
}
