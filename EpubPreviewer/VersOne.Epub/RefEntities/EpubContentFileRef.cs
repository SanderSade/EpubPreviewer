﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using SanderSade.EpubPreviewer.VersOne.Epub.Entities;
using SanderSade.EpubPreviewer.VersOne.Epub.Utils;

namespace SanderSade.EpubPreviewer.VersOne.Epub.RefEntities
{
	public abstract class EpubContentFileRef
	{
		private readonly EpubBookRef _epubBookRef;

		protected EpubContentFileRef(EpubBookRef epubBookRef)
		{
			_epubBookRef = epubBookRef;
		}

		public string FileName { get; set; }
		public EpubContentType ContentType { get; set; }
		public string ContentMimeType { get; set; }

		public byte[] ReadContentAsBytes()
		{
			return ReadContentAsBytesAsync().Result;
		}

		public async Task<byte[]> ReadContentAsBytesAsync()
		{
			var contentFileEntry = GetContentFileEntry();
			var content = new byte[(int)contentFileEntry.Length];
			using (var contentStream = OpenContentStream(contentFileEntry))
			{
				using (var memoryStream = new MemoryStream(content))
				{
					await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
				}
			}

			return content;
		}

		public string ReadContentAsText()
		{
			return ReadContentAsTextAsync().Result;
		}

		public async Task<string> ReadContentAsTextAsync()
		{
			using (var contentStream = GetContentStream())
			{
				using (var streamReader = new StreamReader(contentStream))
				{
					return await streamReader.ReadToEndAsync().ConfigureAwait(false);
				}
			}
		}

		public Stream GetContentStream()
		{
			return OpenContentStream(GetContentFileEntry());
		}

		private ZipArchiveEntry GetContentFileEntry()
		{
			if (string.IsNullOrEmpty(FileName))
			{
				throw new Exception("EPUB parsing error: file name of the specified content file is empty.");
			}

			var contentFilePath = ZipPathUtils.Combine(_epubBookRef.Schema.ContentDirectoryPath, FileName);
			var contentFileEntry = _epubBookRef.EpubArchive.GetEntry(contentFilePath);
			if (contentFileEntry == null)
			{
				throw new Exception($"EPUB parsing error: file \"{contentFilePath}\" was not found in the archive.");
			}

			if (contentFileEntry.Length > int.MaxValue)
			{
				throw new Exception($"EPUB parsing error: file \"{contentFilePath}\" is larger than 2 Gb.");
			}

			return contentFileEntry;
		}

		private Stream OpenContentStream(ZipArchiveEntry contentFileEntry)
		{
			var contentStream = contentFileEntry.Open();
			if (contentStream == null)
			{
				throw new Exception($"Incorrect EPUB file: content file \"{FileName}\" specified in the manifest was not found in the archive.");
			}

			return contentStream;
		}
	}
}
