using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SanderSade.EpubPreviewer.VersOne.Epub.Utils
{
	internal static class XmlUtils
	{
		public static XDocument LoadDocument(Stream stream)
		{
			using (var ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				ms.Position = 0;
				return LoadXDocument(ms);
			}
		}

		private static XDocument LoadXDocument(MemoryStream memoryStream)
		{
			try
			{
				return XDocument.Load(memoryStream);
			}
			catch (XmlException xx)
			{
				if (xx.Message.Contains("'1.1'")) // try to solve the known problem that .NET framework does not support XML version 1.1
				{
					memoryStream.Seek(0, SeekOrigin.Begin);
					var buffer = new byte[512];
					var read = memoryStream.Read(buffer, 0, buffer.Length); // read first 512 byte

					for (var i = 2; i < read; ++i) // search for "1.1" in the buffer
						if (buffer[i - 2] == 0x31 && buffer[i - 1] == 0x2E && buffer[i] == 0x31) // if string is "1.1"
						{
							memoryStream.Seek(i, SeekOrigin.Begin); // seek to index i
							memoryStream.WriteByte(0x30); // replace by '0' to get version number "1.0"
							memoryStream.Seek(0, SeekOrigin.Begin); // rewind memory stream
							return XDocument.Load(memoryStream);
						}
				}

				throw;
			}
		}
	}
}
