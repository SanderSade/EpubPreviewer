namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public enum PageProgressionDirection
	{
		Default = 1,
		LeftToRight,
		RightToLeft,
		Unknown
	}

	internal static class PageProgressionDirectionParser
	{
		public static PageProgressionDirection Parse(string stringValue)
		{
			switch (stringValue.ToLowerInvariant())
			{
				case "default":
					return PageProgressionDirection.Default;
				case "ltr":
					return PageProgressionDirection.LeftToRight;
				case "rtl":
					return PageProgressionDirection.RightToLeft;
				default:
					return PageProgressionDirection.Unknown;
			}
		}
	}
}
