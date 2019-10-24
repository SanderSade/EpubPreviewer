using System.Collections.Generic;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ops
{
	public class Epub3NavOl
	{
		public bool IsHidden { get; set; }
		public List<Epub3NavLi> Lis { get; set; }
	}
}
