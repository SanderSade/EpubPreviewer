using System;
using System.Collections.Generic;
using System.IO.Compression;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.Readers;

namespace SanderSade.EpubPreviewer.VersOne.Epub.RefEntities
{
	public class EpubBookRef : IDisposable
	{
		private bool _isDisposed;


		public EpubBookRef(ZipArchive epubArchive)
		{
			EpubArchive = epubArchive;
			_isDisposed = false;
		}


		public string FilePath { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public List<string> AuthorList { get; set; }
		public EpubSchema Schema { get; set; }
		public EpubContentRef Content { get; set; }

		internal ZipArchive EpubArchive { get; }


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		~EpubBookRef()
		{
			Dispose(false);
		}


		public List<EpubTextContentFileRef> GetReadingOrder()
		{
			return SpineReader.GetReadingOrder(this);
		}


		public List<EpubNavigationItemRef> GetNavigation()
		{
			return NavigationReader.GetNavigationItems(this);
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					EpubArchive?.Dispose();
				}

				_isDisposed = true;
			}
		}
	}
}
