using System;
using System.Runtime.Serialization;

namespace MBS.Audio.Jack
{
	public class VersionMismatchException : JackException
	{
		public VersionMismatchException() : base("client protocol version mismatch")
		{
		}

		protected VersionMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public VersionMismatchException(string message) : base(message)
		{
		}

		public VersionMismatchException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
