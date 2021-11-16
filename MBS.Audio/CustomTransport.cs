//
//  CustomTransport.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2021 ${CopyrightHolder}
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
	public class CustomTransport : ITransport
	{
		public CustomTransport(EventHandler play_handler, EventHandler stop_handler, EventHandler pause_handler)
		{
			_play_handler = play_handler;
			_stop_handler = stop_handler;
			_pause_handler = pause_handler;
		}

		public AudioTimestamp Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public bool IsPlaying => throw new NotImplementedException();

		public AudioPlayerState State => throw new NotImplementedException();

		public event EventHandler<AudioPlayerStateChangedEventArgs> StateChanged;

		private EventHandler _play_handler = null, _stop_handler = null, _pause_handler = null;

		public void Pause()
		{
			_pause_handler?.Invoke(this, EventArgs.Empty);
		}

		public void Play()
		{
			_play_handler?.Invoke(this, EventArgs.Empty);
		}

		public void Stop()
		{
			_stop_handler?.Invoke(this, EventArgs.Empty);
		}
	}
}
