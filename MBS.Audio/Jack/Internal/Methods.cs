//
//  Methods.cs
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
using System.Runtime.InteropServices;

namespace MBS.Audio.Jack.Internal
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "jack";

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*jack_client_t*/ jack_client_open(string client_name, JackOpenOptions options, ref Constants.JackStatus status);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr jack_get_client_name(IntPtr /*jack_client_t*/ client);
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_client_name_size();

		/// <summary>
		/// Start the JACK transport rolling. Any client can make this
		/// request at any time.  It takes effect no sooner than the next
		/// process cycle, perhaps later if there are slow-sync clients.
		/// This function is realtime-safe.
		/// </summary>
		/// <see cref="jack_set_sync_callback" />
		/// <param name="client">the JACK client structure</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_transport_start(IntPtr /*jack_client_t*/ client);

		/// <summary>
		/// Stop the JACK transport. Any client can make this request at any
		/// time.  It takes effect no sooner than the next process cycle,
		/// perhaps later if there are slow-sync clients. This function is
		/// realtime-safe.
		/// </summary>
		/// <param name="client">the JACK client structure</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void jack_transport_stop(IntPtr /*jack_client_t*/ client);

		/// <summary>
		/// Establish a connection between two ports. When a connection exists, data written to the source port will
		/// be available to be read at the destination port.
		/// </summary>
		/// <remarks>
		/// The port types must be identical.
		/// The <see cref="JackPortFlags" /> of the <paramref name="source_port" /> must include <see cref="JackPortFlags.IsOutput" />.
		/// The <see cref="JackPortFlags" /> of the <paramref name="destination_port" /> must include <see cref="JackPortFlags.IsInput" />.
		/// </remarks>
		/// <returns>0 on success, EEXIST if the connection is already made, otherwise a non-zero error code.</returns>
		/// <param name="client">Client.</param>
		/// <param name="source_port">Source port.</param>
		/// <param name="destination_port">Destination port.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_connect(IntPtr /*jack_client_t*/ client, string source_port, string destination_port);

		/// <summary>
		/// return JACK's current system time in microseconds, using the JACK clock source.
		/// </summary>
		/// <remarks>The value returned is guaranteed to be monotonic, but not linear.</remarks>
		[DllImport(LIBRARY_FILENAME)]
		public static extern ulong jack_get_time();

		/// <summary>
		/// Tell the JACK server to call
		/// <paramref name="registration_callback" /> whenever a port is
		/// registered or unregistered, passing <paramref name="arg" /> as a
		/// parameter.
		/// 
		/// All "notification events" are received in a separated non RT thread,
		/// the code in the supplied function does not need to be
		/// suitable for real-time execution.
		/// 
		/// NOTE: this function cannot be called while the client is activated
		/// (after jack_activate has been called.)
		/// </summary>
		/// <returns>0 on success, otherwise a non-zero error code</returns>
		/// <param name="client">Client.</param>
		/// <param name="registration_callback">Registration callback.</param>
		/// <param name="arg">Argument.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_set_port_registration_callback(IntPtr /*jack_client_t*/ client, Delegates.JackPortRegistrationCallback registration_callback, IntPtr arg);
		/// <summary>
		/// Tell the Jack server to call <paramref name="process_callback" />
		/// whenever there is work be done, passing <paramref name="arg" />
		/// as the second argument.
		/// 
		/// The code in the supplied function must be suitable for real-time
		/// execution.That means that it cannot call functions that might
		/// block for a long time. This includes malloc, free, printf,
		/// pthread_mutex_lock, sleep, wait, poll, select, pthread_join,
		/// pthread_cond_wait, etc, etc. See
		/// http://jackit.sourceforge.net/docs/design/design.html#SECTION00411000000000000000
		/// for more information.
		/// 
		/// NOTE: this function cannot be called while the client is activated
		/// (after jack_activate has been called.)
		/// </summary>
		/// <returns>The set process callback.</returns>
		/// <param name="client">Client.</param>
		/// <param name="process_callback">Process callback.</param>
		/// <param name="arg">Argument.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_set_process_callback(IntPtr /*jack_client_t*/ client, Delegates.JackProcessCallback process_callback, IntPtr arg);

		/// <summary>
		/// Tell the Jack server that the program is ready to start
		/// processing audio.
		/// </summary>
		/// <returns>0 on success, otherwise a non-zero error code</returns>
		/// <param name="handle">Handle.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_activate(IntPtr handle);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*jack_port_t*/ jack_port_register(IntPtr /*jack_client_t*/ client, string port_name, string port_type, JackPortFlags flags, uint buffer_size);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*jack_port_t*/ jack_port_by_id(IntPtr /*jack_client_t*/ client, uint /*jack_port_id_t*/ port_id);

		/// <summary>
		/// This returns a pointer to the memory area associated with the
		/// specified port. For an output port, it will be a memory area
		/// that can be written to; for an input port, it will be an area
		/// containing the data from the port's connection(s), or
		/// zero-filled. if there are multiple inbound connections, the data
		/// will be mixed appropriately.
		/// </summary>
		/// <remarks>
		/// Caching output ports is DEPRECATED in Jack 2.0, due to some new optimization(like "pipelining").
		/// Port buffers have to be retrieved in each callback for proper functioning.
		/// </remarks>
		/// <returns>A pointer to the memory area associated with the specified port.</returns>
		/// <param name="port">Port whose buffer is to be returned.</param>
		/// <param name="nframes">The number of frames to return.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*void */ jack_port_get_buffer(IntPtr /*jack_port_t*/ port, uint /*jack_nframes_t*/ nframes);

		[DllImport(LIBRARY_FILENAME, EntryPoint = "jack_port_get_buffer")]
		public static extern float[] jack_port_get_buffer_f(IntPtr /*jack_port_t*/ port, uint /*jack_nframes_t*/ nframes);

		public static void jack_status_to_exception(Constants.JackStatus status)
		{
			switch (status)
			{
				case Constants.JackStatus.BackendError: throw new InvalidOperationException("Backend error");
				case Constants.JackStatus.ClientZombie: throw new InvalidOperationException("Client zombified");
				case Constants.JackStatus.Failure: throw new Exception("General failure");
				case Constants.JackStatus.InitFailure: throw new Exception("Initialization failure");
				case Constants.JackStatus.InvalidOption: throw new ArgumentOutOfRangeException("Invalid operation", (Exception)null);
				case Constants.JackStatus.LoadFailure: throw new Exception("Load failure");
				case Constants.JackStatus.NameNotUnique: throw new ArgumentException("must be unique", "JackClient.Name");
				case Constants.JackStatus.NoSuchClient: throw new ArgumentException("no such client", "Client");
				case Constants.JackStatus.ServerError: throw new ServerException();
				case Constants.JackStatus.ServerFailed: throw new ServerException("unable to connect");
				case Constants.JackStatus.ServerStarted: break;
				case Constants.JackStatus.ShmFailure: throw new InsufficientMemoryException("unable to access shared memory");
				case Constants.JackStatus.Success: break;
				case Constants.JackStatus.VersionError: throw new VersionMismatchException();
			}
		}

		/// <summary>
		/// Remove the port from the client, disconnecting any existing connections.
		/// </summary>
		/// <param name="client">Client.</param>
		/// <param name="port">Port.</param>
		/// <returns>0 if successful; nonzero otherwise</returns>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_port_unregister(IntPtr /*jack_client_t*/ client, IntPtr /*jack_port_t*/ port);

		/// <summary>
		/// The free function to be used on memory returned by jack_port_get_connections,
		/// jack_port_get_all_connections, jack_get_ports and jack_get_internal_client_name functions.
		/// This is MANDATORY on Windows when otherwise all nasty runtime version related crashes can occur.
		/// Developers are strongly encouraged to use this function instead of the standard "free" function in new code.
		/// </summary>
		/// <param name="value">Value.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void jack_free(IntPtr value);

		/// <summary>
		/// Do not call this function; it is not implemented.
		/// </summary>
		/// <param name="client">Client on which to perform the operation.</param>
		[DllImport(LIBRARY_FILENAME), Obsolete("This function has never been implemented")]
		public static extern void jack_off(IntPtr /*jack_client_t*/ client);

		/// <summary>
		/// Query the current transport state and position.
		/// 
		/// This function is realtime-safe, and can be called from any
		/// thread. If called from the process thread,
		/// <paramref name="pos" /> corresponds to the first frame of the
		/// current cycle and the state returned is valid for the entire
		/// cycle.
		/// </summary>
		/// <param name="client">the JACK client structure</param>
		/// <param name="pos">
		/// pointer to structure for returning current transport position;
		/// <paramref name="pos" />-&gt;valid will show which fields contain
		/// valid data. If <paramref name="pos" /> is NULL, do not return
		/// position information.
		/// </param>
		/// <returns>Current transport state.</returns>
		[DllImport(LIBRARY_FILENAME)]
		public static extern JackTransportState jack_transport_query(IntPtr /*const jack_client_t*/ client, ref Structures.jack_position_t pos);

		/// <summary>
		/// Return an estimate of the current transport frame, including any
		/// time elapsed since the last transport positional update.
		/// </summary>
		/// <param name="client">the JACK client structure</param>
		/// <returns>an estimate of the current transport frame</returns>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint jack_get_current_transport_frame(IntPtr /*jack_client_t*/ client);

		/// <summary>
		/// Request a new transport position.
		/// 
		/// May be called at any time by any client. The new position takes
		/// effect in two process cycles.If there are slow-sync clients and
		/// the transport is already rolling, it will enter the
		/// ::JackTransportStarting state and begin invoking their @a
		/// sync_callbacks until ready.This function is realtime-safe.
		/// 
		/// </summary>
		/// <returns>0 if valid request, EINVAL if position structure rejected</returns>
		/// <param name="client">client the JACK client structure</param>
		/// <param name="position">requested new transport position</param>
		/// <see cref="jack_transport_locate" />
		/// <see cref="jack_set_sync_callback" />
		[DllImport(LIBRARY_FILENAME)]
		public static extern int jack_transport_reposition(IntPtr /*jack_client_t*/ client, Structures.jack_position_t position);
	}
}
