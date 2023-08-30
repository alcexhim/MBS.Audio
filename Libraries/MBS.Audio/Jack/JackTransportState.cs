//
//  JackTransportState.cs
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
	/// <summary>
	/// Transport states.
	/// </summary>
	public enum JackTransportState
	{
		/* the order matters for binary compatibility */
		/// <summary>
		/// Transport is halted.
		/// </summary>
		Stopped = 0,
		/// <summary>
		/// Transport is playing.
		/// </summary>
		Rolling = 1,
		/// <summary>
		/// Ignored.
		/// </summary>
		Looping = 2,
		/// <summary>
		/// Waiting for sync ready.
		/// </summary>
		Starting = 3,
		/// <summary>
		/// Waiting for sync ready on the network.
		/// </summary>
		NetworkStarting = 4,
	}
}
