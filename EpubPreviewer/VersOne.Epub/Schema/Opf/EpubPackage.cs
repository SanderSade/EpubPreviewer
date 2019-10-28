using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public class EpubPackage
	{
		public EpubVersion EpubVersion { get; set; }
		public EpubMetadata Metadata { get; set; }
		public EpubManifest Manifest { get; set; }
		public EpubSpine Spine { get; set; }
		public EpubGuide Guide { get; set; }


		public string GetVersionString()
		{
			return VersionUtils.GetVersionString(EpubVersion);
		}
	}
}
