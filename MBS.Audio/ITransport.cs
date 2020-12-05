using System;
namespace MBS.Audio
{
	public interface ITransport
	{
		AudioTimestamp Timestamp { get; set; }

		bool IsPlaying { get; }

		AudioPlayerState State { get; }

		void Stop();
		void Play();
		void Pause();

		event EventHandler<AudioPlayerStateChangedEventArgs> StateChanged;
	}
}
