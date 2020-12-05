using System;
namespace MBS.Audio.Jack.Networking.Internal
{
	internal static class Constants
	{
		public static readonly System.Net.IPAddress DEFAULT_MULTICAST_IP = System.Net.IPAddress.Parse("225.3.19.154");
		public const int DEFAULT_PORT = 19000;
		public const int DEFAULT_MTU = 1500;
		public const int MASTER_NAME_SIZE = 256;
	}
}
