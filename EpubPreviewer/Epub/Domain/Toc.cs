using System.Collections.Generic;

namespace SanderSade.EpubPreviewer.Epub.Domain
{
	/// <summary>
	/// Table of contents & book info
	/// </summary>
	internal sealed class Toc
	{
		internal string Author { get; set; }
		internal string Title { get; set; }
		internal List<TocEntry>  TocEntries { get; set; }
	}
}
