using System;
namespace MBS.Audio.Jack
{
	public class JackProcessEventArgs : EventArgs
	{
		public long FrameCount { get; private set; } = 0;
		public IntPtr UserData { get; private set; } = IntPtr.Zero;
		public int ReturnValue { get; set; } = 0;

		public JackProcessEventArgs(long nframes, IntPtr arg)
		{
			FrameCount = nframes;
			UserData = arg;
		}
	}
}
