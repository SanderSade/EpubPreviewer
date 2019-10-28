using System.Collections.Generic;
using SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public class EpubManifestItem
	{
		public string Id { get; set; }
		public string Href { get; set; }
		public string MediaType { get; set; }
		public string RequiredNamespace { get; set; }
		public string RequiredModules { get; set; }
		public string Fallback { get; set; }
		public string FallbackStyle { get; set; }
		public List<ManifestProperty> Properties { get; set; }


		public override string ToString()
		{
			return $"Id: {Id}, Href = {Href}, MediaType = {MediaType}";
		}
	}
}
