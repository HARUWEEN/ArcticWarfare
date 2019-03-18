using System;
using System.Collections.Generic;
using System.Text;

namespace ArctucEmu.Server.Net.PacketTypes
{
	public class PacketProcessedEventArgs : EventArgs
	{
		public PacketProcessedEventArgs(ClientPacket packetIn)
		{
			ClientPacket = packetIn;
		}

		public ClientPacket ClientPacket { get; }
	}
}
