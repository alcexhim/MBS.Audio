using System;
using System.Runtime.InteropServices;

namespace MBS.Audio.Jack.Networking.Internal
{
	internal static class Methods
	{
		[DllImport(Jack.Internal.Methods.LIBRARY_FILENAME)]
		public static extern IntPtr /*jack_net_slave_t*/ jack_net_slave_open(string ip, int port, string name, ref Internal.Structures.jack_slave_t request, ref Internal.Structures.jack_master_t result);
	}
}
