//
//  Structures.cs
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
namespace MBS.Audio.Jack.Internal
{
	internal class Structures
	{
		public struct jack_position_t
		{
			/* these four cannot be set from clients: the server sets them */
			/// <summary>
			/// unique ID
			/// </summary>
			public ulong /*jack_unique_t*/ unique_1;
			/// <summary>
			/// monotonic, free-rolling
			/// </summary>
			public ulong /*jack_time_t*/ usecs;
			/// <summary>
			/// current frame rate (per second)
			/// </summary>
			public uint /*jack_nframes_t*/ frame_rate;
			/// <summary>
			/// frame number, always present
			/// </summary>
			public uint /*jack_nframes_t*/ frame;

			/// <summary>
			/// which other fields are valid
			/// </summary>
			public Constants.JackPositionBits valid;

			/* JackPositionBBT fields: */
			/// <summary>
			/// Current bar.
			/// </summary>
			public int bar;
			/// <summary>
			/// Current beat-within-bar.
			/// </summary>
			public int beat;
			/// <summary>
			/// Current tick-within-beat.
			/// </summary>
			public int tick;
			public double bar_start_tick;

			/// <summary>
			/// Time signatue numerator.
			/// </summary>
			public float beats_per_bar;
			/// <summary>
			/// Timee signature denominator.
			/// </summary>
			public float beat_type;
			public double ticks_per_beat;
			public double beats_per_minute;

			/* JackPositionTimecode fields:     (EXPERIMENTAL: could change) */
			/// <summary>
			/// Current time in seconds.
			/// </summary>
			public double frame_time;
			/// <summary>
			/// Next sequential frame_time (unless repositioned).
			/// </summary>
			public double next_time;

			/* JackBBTFrameOffset fields: */
			/// <summary>
			/// frame offset for the BBT fields (the given bar, beat, and tick
			/// values actually refer to a time <c>frame_offset</c> frames
			/// before the start of the cycle), should be assumed to be 0 if
			/// JackBBTFrameOffset is not set.If JackBBTFrameOffset is set and
			/// this value is zero, the BBT time refers to the first frame of
			/// this cycle. If the value is positive, the BBT time refers to
			/// a frame that many frames before the start of the cycle.
			/// </summary>
			public uint /*jack_nframes_t*/ bbt_offset;

			/* JACK video positional data (experimental) */
			/// <summary>
			/// number of audio frames per video frame. Should be assumed
			/// zero if JackAudioVideoRatio is not set. If
			/// JackAudioVideoRatio is set and the value is zero, no video
			/// data exists within the JACK graph
			/// </summary>
			public float audio_frames_per_video_frame;

			/// <summary>
			/// audio frame at which the first video frame in this cycle
			/// occurs. Should be assumed to be 0 if JackVideoFrameOffset
			/// is not set. If JackVideoFrameOffset is set, but the value is
			/// zero, there is no video frame within this cycle.
			/// </summary>
			public uint /*jack_nframes_t*/ video_offset;

			/// <summary>
			/// For binary compatibility, new fields should be allocated from
			/// this padding area with new valid bits controlling access, so
			/// the existing structure size and offsets are preserved.
			/// </summary>
			public int padding1 /*[7]*/;
			public int padding2 /*[7]*/;


			public int padding3 /*[7]*/;
			public int padding4 /*[7]*/;
			public int padding5 /*[7]*/;
			public int padding6 /*[7]*/;
			public int padding7 /*[7]*/;

			/// <summary>
			/// When (<see cref="unique_1" /> == <see cref="unique_2" />) the
			/// contents are consistent.
			/// </summary>
			public ulong /*jack_unique_t*/ unique_2;
		}
	}
}
