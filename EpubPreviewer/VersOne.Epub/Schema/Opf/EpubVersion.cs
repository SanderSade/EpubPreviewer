using System;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public enum EpubVersion
	{
		[VersionString("2.0")]
		Epub2 = 2,

		[VersionString("3.0")]
		Epub30,

		[VersionString("3.1")]
		Epub31
	}

	internal class VersionStringAttribute : Attribute
	{
		public VersionStringAttribute(string version)
		{
			Version = version;
		}

		public string Version { get; }
	}
}
