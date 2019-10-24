﻿using System.Collections.Generic;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Ncx
{
	public class Epub2NcxNavigationTarget
	{
		public string Id { get; set; }
		public string Class { get; set; }
		public string Value { get; set; }
		public string PlayOrder { get; set; }
		public List<Epub2NcxNavigationLabel> NavigationLabels { get; set; }
		public Epub2NcxContent Content { get; set; }
	}
}
