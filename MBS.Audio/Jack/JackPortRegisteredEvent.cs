using System;
namespace MBS.Audio.Jack
{
	public class JackPortRegisteredEventArgs : EventArgs
	{
		public IntPtr Handle { get; private set; } = IntPtr.Zero;

		public JackPortRegisteredEventArgs(IntPtr handle)
		{
			Handle = handle;
		}
	}
}
