//
//  JackPortFlags.cs
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
	/// A port has a set of flags that are formed by AND-ing together the
	/// desired values from this enum. The flags <see cref="IsInput" /> and
	/// <see cref="IsOutput" /> are mutually exclusive and it is an error to
	/// use them both.
	/// </summary>
	[Flags()]
	public enum JackPortFlags
	{
		/// <summary>
		/// The port can receive data.
		/// </summary>
		IsInput = 0x1,
		/// <summary>
		/// Data can be read from the port.
		/// </summary>
		IsOutput = 0x2,
		/// <summary>
		/// The port corresponds to some kind of physical I/O connector.
		/// </summary>
		IsPhysical = 0x4,

		/// <summary>
		/// Indicates that a call on this port to
		/// <see cref="JackPort.RequestMonitor"/> makes sense.
		/// 
		/// Precisely what this means is dependent on the client. A typical
		/// result of it being called with TRUE as the second argument is
		/// that data that would be available from an output port (with
		/// <see cref="IsPhysical" /> set) is sent to a physical output connector
		/// as well, so that it can be heard/seen/whatever.
		/// 
		/// Clients that do not control physical interfaces
		/// should never create ports with this bit set.
		/// </summary>
		CanMonitor = 0x8,
		/// <summary>
		/// For an input port, indicates that data received by the port will
		/// not be passed on or made available at any other port.
		/// 
		/// For an output port, indicates that data available at the port
		/// does not originate from any other port.
		/// 
		/// Audio synthesizers, I/O hardware interface clients, HDR
		/// systems are examples of clients that would set this flag for
		/// their ports.
		/// </summary>
		IsTerminal = 0x10,
	}
}
