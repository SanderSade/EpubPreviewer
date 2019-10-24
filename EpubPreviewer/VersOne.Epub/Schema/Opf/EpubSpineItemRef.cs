using System.Collections.Generic;
using System.Text;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public class EpubSpineItemRef
	{
		public string Id { get; set; }
		public string IdRef { get; set; }
		public bool IsLinear { get; set; }
		public List<SpineProperty> Properties { get; set; }

		public override string ToString()
		{
			var resultBuilder = new StringBuilder();
			if (Id != null)
			{
				resultBuilder.Append("Id: ");
				resultBuilder.Append(Id);
				resultBuilder.Append("; ");
			}

			resultBuilder.Append("IdRef: ");
			resultBuilder.Append(IdRef ?? "(null)");
			return resultBuilder.ToString();
		}
	}
}
