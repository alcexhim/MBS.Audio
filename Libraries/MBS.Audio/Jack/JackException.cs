using System;
using System.Runtime.Serialization;

namespace MBS.Audio.Jack
{
	/// <summary>
	/// The base class for all JACK <see cref="Exception" />s that are not
	/// provided by the un
	/// </summary>
	public class JackException : Exception
	{
		public JackException()
		{
		}

		protected JackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public JackException(string message) : base(message)
		{
		}

		public JackException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
