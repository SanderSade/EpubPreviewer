using System;

namespace SanderSade.EpubPreviewer.Epub
{
	[Serializable]
	internal sealed class EpubException : Exception
	{
		internal EpubException(string message) : base(message)
		{
		}
	}
}
