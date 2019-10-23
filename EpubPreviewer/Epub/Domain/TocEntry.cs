namespace SanderSade.EpubPreviewer.Epub.Domain
{
	internal struct TocEntry
	{
		public string Title { get; }
		public string FilePath { get; }

		public TocEntry(string title, string filePath)
		{
			Title = title;
			FilePath = filePath;
		}
	}
}
