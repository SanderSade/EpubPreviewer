using System;
using System.Collections.Generic;
using System.Linq;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Opf
{
	public enum SpineProperty
	{
		PageSpreadLeft = 1,
		PageSpreadRight,
		Unknown
	}

	internal static class SpinePropertyParser
	{
		public static List<SpineProperty> ParsePropertyList(string stringValue)
		{
			return stringValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(propertyString => ParseProperty(propertyString.Trim()))
				.ToList();
		}


		public static SpineProperty ParseProperty(string stringValue)
		{
			switch (stringValue.ToLowerInvariant())
			{
				case "page-spread-left":
					return SpineProperty.PageSpreadLeft;
				case "page-spread-right":
					return SpineProperty.PageSpreadRight;
				default:
					return SpineProperty.Unknown;
			}
		}
	}
}
