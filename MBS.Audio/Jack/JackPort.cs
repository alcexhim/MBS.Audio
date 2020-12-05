//
//  JackPort.cs
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
	public abstract class JackPort
	{
		public JackClient Client { get; private set; } = null;
		public IntPtr Handle { get; private set; } = IntPtr.Zero;

		public string Name { get; private set; } = null;
		public string Type { get; private set; } = null;
		public long BufferSize { get; private set; } = 0;

		public static string DefaultPortType = JackPortTypes.DefaultAudioType;

		/// <summary>
		/// Indicates that the port can receive data.
		/// </summary>
		/// <value><c>true</c> if the port can receive data; otherwise, <c>false</c>.</value>
		public bool IsInput { get; private set; } = false;
		/// <summary>
		/// Indicates that data can be read from the port.
		/// </summary>
		/// <value><c>true</c> if data can be read from the port; otherwise, <c>false</c>.</value>
		public bool IsOutput { get; private set; } = false;
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
		public bool CanMonitor { get; private set; } = false;
		/// <summary>
		/// Indicates that the port corresponds to some kind of physical I/O connector.
		/// </summary>
		/// <value><c>true</c> if is physical; otherwise, <c>false</c>.</value>
		public bool IsPhysical { get; private set; } = false;
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
		public bool IsTerminal { get; private set; } = false;

		internal JackPort(JackClient client, IntPtr handle, string portName, string portType, long bufferSize, bool isInput, bool isOutput, bool canMonitor, bool isPhysical, bool isTerminal)
		{
			Client = client;
			Handle = handle;
			Name = portName;
			Type = portType;
			BufferSize = bufferSize;
			IsInput = isInput;
			IsOutput = isOutput;
			CanMonitor = canMonitor;
			IsPhysical = isPhysical;
			IsTerminal = isTerminal;
		}
	}
}
