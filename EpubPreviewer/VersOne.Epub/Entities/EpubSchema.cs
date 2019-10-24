using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ncx;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Entities
{
	public class EpubSchema
	{
		public EpubPackage Package { get; set; }
		public Epub2Ncx Epub2Ncx { get; set; }
		public Epub3NavDocument Epub3NavDocument { get; set; }
		public string ContentDirectoryPath { get; set; }
	}
}
