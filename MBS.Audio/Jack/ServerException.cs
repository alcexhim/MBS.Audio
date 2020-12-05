using System;
using System.Runtime.Serialization;

namespace MBS.Audio.Jack
{
	public class ServerException : JackException
	{
		public ServerException()
		{
		}

		protected ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ServerException(string message) : base(message)
		{
		}

		public ServerException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
