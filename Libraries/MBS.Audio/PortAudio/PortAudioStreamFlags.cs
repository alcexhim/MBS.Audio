using System;
namespace MBS.Audio.PortAudio
{
	[Flags()]
	public enum PortAudioStreamFlags : uint
	{
		None,
		ClipOff,
		DitherOff,
		NeverDropInput = 4u,
		PrimeOutputBuffersUsingStreamCallback = 8u,
		PlatformSpecificFlags = 4294901760u
	}
}
