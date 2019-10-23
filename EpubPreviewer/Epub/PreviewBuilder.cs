using System;
using SanderSade.EpubPreviewer.Epub.Domain;

namespace SanderSade.EpubPreviewer.Epub
{
	internal sealed class PreviewBuilder
	{
		private readonly string _outFolder;
		private readonly Toc _toc;

		public PreviewBuilder(Toc toc, string outFolder)
		{
			_toc = toc;
			_outFolder = outFolder;
		}

		public string Build()
		{
			throw new NotImplementedException();
		}
	}
}
