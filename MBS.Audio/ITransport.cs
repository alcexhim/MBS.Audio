//
//  ITransport.cs - interface for defining the minimum functionality required for an audio transport
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020-2021 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace MBS.Audio
{
	/// <summary>
	/// Defines the minimum functionality required for an audio transport that
	/// can play, pause, stop, and seek within the audio.
	/// </summary>
	public interface ITransport
	{
		/// <summary>
		/// Gets or sets the <see cref="AudioTimestamp" /> position of the
		/// transport.
		/// </summary>
		/// <value>The timestamp to get or set.</value>
		AudioTimestamp Timestamp { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="ITransport" /> is
		/// currently playing (rolling).
		/// </summary>
		/// <value><c>true</c> if the transport is playing (rolling);
		/// otherwise, <c>false</c>.</value>
		bool IsPlaying { get; }

		/// <summary>
		/// Gets a value indicating whether the transport is currently
		/// stopped, playing, or paused.
		/// </summary>
		/// <value>The state of the transport.</value>
		AudioPlayerState State { get; }

		/// <summary>
		/// Stops playback of the audio stream associated with this
		/// <see cref="ITransport" />. This is equivalent to calling
		/// <see cref="Pause" /> and setting <see cref="Timestamp" /> to the
		/// beginning of the stream.
		/// </summary>
		/// <remarks>
		/// There is no concept of "pausing" with respect to "stopping" in the
		/// JACK transport. Therefore, JACK's definition of "stopping" is
		/// equivalent to "pausing" here, and "stopping" here refers to
		/// "pausing" followed by resetting the <see cref="Timestamp" /> to the
		/// beginning of the audio stream (if possible).
		/// </remarks>
		void Stop();
		/// <summary>
		/// Starts playback of the audio stream associated with this
		/// <see cref="ITransport" />. If the audio stream is currently
		/// paused, this MAY result in simply resuming the audio stream where
		/// it left off.
		/// </summary>
		void Play();
		/// <summary>
		/// Pauses playback of the audio stream associated with this
		/// <see cref="ITransport" />. This essentially stops the playback
		/// but does not reset the <see cref="Timestamp" /> to the beginning
		/// of the stream.
		/// </summary>
		/// <remarks>
		/// There is no concept of "pausing" with respect to "stopping" in the
		/// JACK transport. Therefore, JACK's definition of "stopping" is
		/// equivalent to "pausing" here, and "stopping" here refers to
		/// "pausing" followed by resetting the <see cref="Timestamp" /> to the
		/// beginning of the audio stream (if possible).
		/// </remarks>
		void Pause();

		/// <summary>
		/// Occurs when the state of the <see cref="ITransport" /> changes.
		/// </summary>
		event EventHandler<AudioPlayerStateChangedEventArgs> StateChanged;
	}
}
