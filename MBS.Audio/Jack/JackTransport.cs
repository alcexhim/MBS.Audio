//
//  JackTransport.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace MBS.Audio.Jack
{
	public class JackTransport : ITransport
	{
		public JackClient Client { get; private set; }

		internal JackTransport(JackClient client)
		{
			Client = client;
		}

		public JackTransportState TransportState
		{
			get
			{
				Internal.Structures.jack_position_t pos = new Internal.Structures.jack_position_t();
				JackTransportState state = Internal.Methods.jack_transport_query(Client.Handle, ref pos);

				return state;
			}
		}

		public AudioTimestamp Timestamp
		{
			get
			{
				Internal.Structures.jack_position_t pos = new Internal.Structures.jack_position_t();
				JackTransportState state = Internal.Methods.jack_transport_query(Client.Handle, ref pos);

				return AudioTimestamp.FromSamples((long)pos.frame, (int)pos.frame_rate, pos.bar, pos.beat, pos.tick, pos.beats_per_bar, pos.ticks_per_beat);
			}
			set
			{
				Internal.Structures.jack_position_t pos = new Internal.Structures.jack_position_t();
				Internal.Methods.jack_transport_query(Client.Handle, ref pos);

				pos.bar = value.Bars;
				pos.beat = value.Beats;
				pos.tick = value.Ticks;

				pos.frame_time = (uint)(value.TotalSamples);
				pos.valid = Internal.Constants.JackPositionBits.PositionTimecode;
				Internal.Methods.jack_transport_reposition(Client.Handle, pos);
			}
		}

		public bool IsPlaying => State != AudioPlayerState.Stopped;

		public AudioPlayerState State
		{
			get
			{
				switch (TransportState)
				{
					case JackTransportState.Looping:
					case JackTransportState.Rolling:
					{
						return AudioPlayerState.Playing;
					}
					case JackTransportState.NetworkStarting:
					case JackTransportState.Starting:
					{
						return AudioPlayerState.Stopped;
					}
					case JackTransportState.Stopped:
					{
						if (_paused)
							return AudioPlayerState.Paused;
						return AudioPlayerState.Stopped;
					}
				}
				return AudioPlayerState.Stopped;
			}
		}

		private bool _paused = false;

		public event EventHandler<AudioPlayerStateChangedEventArgs> StateChanged;

		/// <summary>
		/// Start the JACK transport rolling. Any client can make this
		/// request at any time.  It takes effect no sooner than the next
		/// process cycle, perhaps later if there are slow-sync clients.
		/// This function is realtime-safe.
		/// </summary>
		public void Play()
		{
			_paused = false;
			Internal.Methods.jack_transport_start(Client.Handle);
		}
		/// <summary>
		/// Stop the JACK transport. Any client can make this request at any
		/// time.  It takes effect no sooner than the next process cycle,
		/// perhaps later if there are slow-sync clients. This function is
		/// realtime-safe.
		/// </summary>
		public void Pause()
		{
			_paused = !_paused;
			Internal.Methods.jack_transport_stop(Client.Handle);
		}
		/// <summary>
		/// Stop the JACK transport and reset the position to the beginning.
		/// Any client can make this request at any time.  It takes effect no
		/// sooner than the next process cycle, perhaps later if there are
		/// slow-sync clients. This function is realtime-safe.
		/// </summary>
		public void Stop()
		{
			Pause();
			Seek(0);

			_paused = false;
		}

		public void Seek(long totalSamples)
		{
			// TODO: implement this
			Internal.Structures.jack_position_t pos = new Internal.Structures.jack_position_t();
			pos.valid = Internal.Constants.JackPositionBits.PositionTimecode;
			pos.frame_time = totalSamples;
			Internal.Methods.jack_transport_reposition(Client.Handle, pos);
		}
	}
}
