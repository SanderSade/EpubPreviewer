namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common
{
	public enum ManifestProperty
	{
		CoverImage = 1,
		Mathml,
		Nav,
		RemoteResources,
		Scripted,
		Svg,
		Unknown
	}

	internal static class ManifestPropertyParser
	{
		public static ManifestProperty Parse(string stringValue)
		{
			switch (stringValue.ToLowerInvariant())
			{
				case "cover-image":
					return ManifestProperty.CoverImage;
				case "mathml":
					return ManifestProperty.Mathml;
				case "nav":
					return ManifestProperty.Nav;
				case "remote-resources":
					return ManifestProperty.RemoteResources;
				case "scripted":
					return ManifestProperty.Scripted;
				case "svg":
					return ManifestProperty.Svg;
				default:
					return ManifestProperty.Unknown;
			}
		}
	}
}
