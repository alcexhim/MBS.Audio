using System;
namespace MBS.Audio
{
	public abstract class AudioDevice
	{
		public abstract int MaximumInputChannels { get; }
		public abstract int MaximumOutputChannels { get; }

		public abstract double DefaultLowInputLatency { get; }
		public abstract double DefaultLowOutputLatency { get; }
		public abstract double DefaultHighInputLatency { get; }
		public abstract double DefaultHighOutputLatency { get; }

		public abstract double DefaultSampleRate { get; }

		public static AudioDevice None { get; } = null;
	}
}
