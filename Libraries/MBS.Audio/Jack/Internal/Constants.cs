//
//  Constants.cs
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
	internal static class Constants
	{
		public enum JackStatus
		{
			Success = 0x00,
			/// <summary>
			/// Overall operation failed.
			/// </summary>
			Failure = 0x01,
			/// <summary>
			/// The operation contained an invalid or unsupported option.
			/// </summary>
			InvalidOption = 0x02,
			/// <summary>
			/// The desired client name was not unique.  With the @ref
			/// JackUseExactName option this situation is fatal.  Otherwise,
			/// the name was modified by appending a dash and a two-digit
			/// number in the range "-01" to "-99".  The
			/// jack_get_client_name() function will return the exact string
			/// that was used. If the specified @a client_name plus these
			/// extra characters would be too long, the open fails instead.
			/// </summary>
			NameNotUnique = 0x04,
			/// <summary>
			/// The JACK server was started as a result of this operation.
			/// Otherwise, it was running already.  In either case the caller
			/// is now connected to jackd, so there is no race condition.
			/// When the server shuts down, the client will find out.
			/// </summary>
			ServerStarted = 0x08,
			/// <summary>
			/// Unable to connect to the JACK server.
			/// </summary>
			ServerFailed = 0x10,
			/// <summary>
			/// Communication error with the JACK server.
			/// </summary>
			ServerError = 0x20,
			/// <summary>
			/// Requested client does not exist.
			/// </summary>
			NoSuchClient = 0x40,
			/// <summary>
			/// Unable to load internal client
			/// </summary>
			LoadFailure = 0x80,
			/// <summary>
			/// Unable to initialize client
			/// </summary>
			InitFailure = 0x100,
			/// <summary>
			/// Unable to access shared memory
			/// </summary>
			ShmFailure = 0x200,
			/// <summary>
			/// Client's protocol version does not match
			/// </summary>
			VersionError = 0x400,
			/// <summary>
			/// Backend error
			/// </summary>
			BackendError = 0x800,
			/// <summary>
			/// Client zombified failure
			/// </summary>
			ClientZombie = 0x1000
		}

		public enum JackPositionBits
		{
			/// <summary>
			/// Bar, Beat, Tick
			/// </summary>
			PositionBBT = 0x10,
			/// <summary>
			/// External timecode
			/// </summary>
			PositionTimecode = 0x20,
			/// <summary>
			/// Frame offset of BBT information
			/// </summary>
			BBTFrameOffset = 0x40,
			/// <summary>
			/// Audio frames per video frame.
			/// </summary>
			AudioVideoRatio = 0x80,
			/// <summary>
			/// Frame offset of first video frame.
			/// </summary>
			VideoFrameOffset = 0x100
		}
	}
}
