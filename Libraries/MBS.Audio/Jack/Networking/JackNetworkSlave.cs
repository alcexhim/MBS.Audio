using System;
namespace MBS.Audio.Jack.Networking
{
	public class JackNetworkSlave
	{
		public IntPtr Handle { get; private set; } = IntPtr.Zero;

		public void Open(System.Net.IPAddress ipAddress, int port, string name)
		{
			Internal.Structures.jack_slave_t request = new Internal.Structures.jack_slave_t();
			Internal.Structures.jack_master_t result = new Internal.Structures.jack_master_t();

			IntPtr handle = Internal.Methods.jack_net_slave_open(ipAddress.ToString(), port, name, ref request, ref result);
			if (handle != IntPtr.Zero)
			{
				Handle = handle;
			}
		}
	}
}
