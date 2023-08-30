using System;
namespace MBS.Audio.Jack.Internal
{
	public class Delegates
	{
		/// <summary>
		/// Prototype for the client supplied function that is called
		/// whenever a port is registered or unregistered.
		/// </summary>
		/// <param name="port">the ID of the port</param>
		/// <param name="register">
		/// non-zero if the port is being registered, zero if the port is
		/// being unregistered
		/// </param>
		/// <param name="arg">pointer to a client supplied data</param>
		public delegate void JackPortRegistrationCallback(uint /*jack_port_id_t*/ port, int register, IntPtr arg);
		/// <summary>
		/// Prototype for the client supplied function that is called
		/// by the engine anytime there is work to be done.
		/// </summary>
		/// <param name="nframes">number of frames to process</param>
		/// <param name="arg">pointer to a client supplied structure</param>
		/// <returns>zero on success; non-zero on error</returns>
		public delegate int JackProcessCallback(uint /*jack_nframes_t*/ nframes, IntPtr arg);
	}
}
