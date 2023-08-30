//
//  JackClient.cs
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
	public class JackClient
	{
		public JackTransport Transport { get; private set; } = null;

		public JackClient()
		{
			_registration_callback_d = new Internal.Delegates.JackPortRegistrationCallback(_registration_callback);
			_process_callback_d = new Internal.Delegates.JackProcessCallback(_process_callback);
			Transport = new JackTransport(this);
		}
		public JackClient(string name) : this()
		{
			if (name.Length > MaximumNameLength)
			{

			}
			Name = name;
			Transport = new JackTransport(this);
		}

		private int? _MaximumNameLength = null;
		public int MaximumNameLength
		{
			get
			{
				if (_MaximumNameLength == null)
				{
					_MaximumNameLength = Internal.Methods.jack_client_name_size();
				}
				return _MaximumNameLength.GetValueOrDefault(0);
			}
		}

		public IntPtr Handle { get; private set; } = IntPtr.Zero;
		public string Name { get; private set; } = null;

		/// <summary>
		/// Gets the actual name of the client as assigned by the JACK server.
		/// </summary>
		/// <value>The name of the client.</value>
		public string ClientName
		{
			get
			{
				if (Handle == IntPtr.Zero)
					return null;

				IntPtr hName = Internal.Methods.jack_get_client_name(Handle);
				string value = System.Runtime.InteropServices.Marshal.PtrToStringAuto(hName);

				Internal.Methods.jack_free(hName);
				return value;
			}
		}

		/// <summary>
		/// Open an external client session with a JACK server.  Clients may
		/// choose which of several servers to connect, and control
		/// whether and how to start the server automatically, if it was not
		/// already running.  There is also an option for JACK to generate a
		/// unique client name, when necessary.
		/// </summary>
		/// <param name="options"><see cref="JackOpenOptions" /> for opening an external client.</param>
		public void Open(JackOpenOptions options = JackOpenOptions.None)
		{
			string clientName = Name;
			if (clientName == null)
			{
				throw new ArgumentNullException(nameof(Name));
			}

			Internal.Constants.JackStatus status = Internal.Constants.JackStatus.Success;
			IntPtr handle = Internal.Methods.jack_client_open(clientName, options, ref status);

			// set up the event handlers
			// Internal.Methods.jack_set_process_callback(handle, _process_callback_d, IntPtr.Zero);
			Internal.Methods.jack_set_port_registration_callback(handle, _registration_callback_d, IntPtr.Zero);

			Internal.Methods.jack_status_to_exception(status);
			Handle = handle;
		}

		private Internal.Delegates.JackPortRegistrationCallback _registration_callback_d;
		private void _registration_callback(uint /*jack_port_id_t*/ port, int register, IntPtr arg)
		{
			IntPtr hPort = Internal.Methods.jack_port_by_id(Handle, port);
			if (hPort != IntPtr.Zero)
			{
				JackPortRegisteredEventArgs e = new JackPortRegisteredEventArgs(hPort);
				OnPortRegistered(e);
			}
		}

		private Internal.Delegates.JackProcessCallback _process_callback_d;
		private int _process_callback(uint nframes, IntPtr arg)
		{
			JackProcessEventArgs ee = new JackProcessEventArgs(nframes, arg);
			OnProcess(ee);
			return ee.ReturnValue;
		}

		/// <summary>
		/// Tell the Jack server that the program is ready to start
		/// processing audio.
		/// </summary>
		public void Activate()
		{
			Internal.Methods.jack_activate(Handle);
		}

		public event EventHandler<JackProcessEventArgs> Process;
		protected virtual void OnProcess(JackProcessEventArgs e)
		{
			Process?.Invoke(this, e);
		}

		public event EventHandler<JackPortRegisteredEventArgs> PortRegistered;
		protected virtual void OnPortRegistered(JackPortRegisteredEventArgs e)
		{
			PortRegistered?.Invoke(this, e);
		}

		public void Connect(string sourcePortName, string destinationPortName)
		{
			if (Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException("please Open() the JackClient first!");
			}
			Internal.Methods.jack_connect(Handle, sourcePortName, destinationPortName);
		}

		/// <summary>
		/// Create a new port for the client. This is an object used for moving
		/// data of any type in or out of the client. Ports may be connected
		/// in various ways.
		/// 
		/// Each port has a short name.  The port's full name contains the name
		/// of the client concatenated with a colon (:) followed by its short
		/// name. The jack_port_name_size() is the maximum length of this full
		/// name.  Exceeding that will cause the port registration to fail and
		/// return NULL.
		/// 
		/// The @a port_name must be unique among all ports owned by this client.
		/// If the name is not unique, the registration will fail.
		/// 
		/// All ports have a type, which may be any non-NULL and non-zero
		/// length string, passed as an argument.  Some port types are built
		/// into the JACK API, currently only JACK_DEFAULT_AUDIO_TYPE.
		/// </summary>
		/// <param name="portName">
		/// Non-empty short name for the new port, not including the leading
		/// <c>"client_name:"</c>. Must be unique within the client.
		/// </param>
		/// <param name="portType">
		/// Port type name. If longer than
		/// <see cref="JackPort.MaximumTypeNameLength" />, only that many
		/// characters are significant.
		/// </param>
		/// <param name="flags">Flags.</param>
		/// <param name="bufferSize">
		/// Must be non-zero if this is not a built-in <c>port_type</c>.
		/// Otherwise, it is ignored
		/// </param>
		/// <returns>
		/// A <see cref="JackInputPort" /> or <see cref="JackOutputPort" />,
		/// depending on the value of <paramref name="flags" />, on success;
		/// otherwise, <see langword="null" />.
		/// </returns>
		private JackPort RegisterPort(string portName, string portType, JackPortFlags flags, long bufferSize)
		{
			IntPtr handle = Internal.Methods.jack_port_register(Handle, portName, portType, flags, (uint)bufferSize);
			if (handle == IntPtr.Zero)
			{
				throw new InvalidOperationException("jack client not valid");
			}

			if ((flags & JackPortFlags.IsInput) == JackPortFlags.IsInput)
			{
				JackInputPort port = new JackInputPort(this, handle, portName, portType, bufferSize, (flags & JackPortFlags.CanMonitor) == JackPortFlags.CanMonitor, (flags & JackPortFlags.IsPhysical) == JackPortFlags.IsPhysical, (flags & JackPortFlags.IsTerminal) == JackPortFlags.IsTerminal);
				return port;
			}
			else if ((flags & JackPortFlags.IsOutput) == JackPortFlags.IsOutput)
			{
				JackOutputPort port = new JackOutputPort(this, handle, portName, portType, bufferSize, (flags & JackPortFlags.CanMonitor) == JackPortFlags.CanMonitor, (flags & JackPortFlags.IsPhysical) == JackPortFlags.IsPhysical, (flags & JackPortFlags.IsTerminal) == JackPortFlags.IsTerminal);
				return port;
			}
			throw new InvalidOperationException("port must be either input or output");
		}

		/// <summary>
		/// Create a new input port for the client. This is an object used for
		/// moving data of any type into the client. Ports may be connected
		/// in various ways.
		/// 
		/// Each port has a short name.  The port's full name contains the name
		/// of the client concatenated with a colon (:) followed by its short
		/// name. The jack_port_name_size() is the maximum length of this full
		/// name.  Exceeding that will cause the port registration to fail and
		/// return NULL.
		/// 
		/// The <paramref name="portName" /> must be unique among all ports
		/// owned by this client. If the name is not unique, the registration
		/// will fail.
		/// 
		/// All ports have a type, which may be any non-NULL and non-zero
		/// length string, passed as an argument. Some port types are built
		/// into the JACK API, currently only
		/// <see cref="JackPortTypes.DefaultAudioType"/> and
		/// <see cref="JackPortTypes.DefaultMidiType" />.
		/// </summary>
		/// <param name="portName">
		/// Non-empty short name for the new port, not including the leading
		/// <c>"client_name:"</c>. Must be unique within the client.
		/// </param>
		/// <param name="portType">
		/// Port type name. If longer than
		/// <see cref="JackPort.MaximumTypeNameLength" />, only that many
		/// characters are significant.
		/// </param>
		/// <param name="flags">Flags.</param>
		/// <param name="bufferSize">
		/// Must be non-zero if this is not a built-in <c>port_type</c>.
		/// Otherwise, it is ignored
		/// </param>
		/// <returns>
		/// A <see cref="JackInputPort" /> or <see cref="JackOutputPort" />,
		/// depending on the value of <paramref name="flags" />, on success;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public JackInputPort RegisterInput(string portName, string portType = null, long bufferSize = 0, bool isMonitor = false, bool isPhysical = false, bool isTerminal = false)
		{
			JackPortFlags flags = JackPortFlags.IsInput;
			if (isMonitor) flags |= JackPortFlags.CanMonitor;
			if (isPhysical) flags |= JackPortFlags.IsPhysical;
			if (isTerminal) flags |= JackPortFlags.IsTerminal;
			if (portType == null) portType = JackPort.DefaultPortType;
			return (JackInputPort)RegisterPort(portName, portType, flags, bufferSize);
		}
		public JackOutputPort RegisterOutput(string portName, string portType = null, long bufferSize = 0, bool isMonitor = false, bool isPhysical = false, bool isTerminal = false)
		{
			JackPortFlags flags = JackPortFlags.IsOutput;
			if (isMonitor) flags |= JackPortFlags.CanMonitor;
			if (isPhysical) flags |= JackPortFlags.IsPhysical;
			if (isTerminal) flags |= JackPortFlags.IsTerminal;
			if (portType == null) portType = JackPort.DefaultPortType;
			return (JackOutputPort)RegisterPort(portName, portType, flags, bufferSize);
		}

		public void UnregisterPort(JackPort port)
		{
			Internal.Methods.jack_port_unregister(Handle, port.Handle);
		}
	}
}
