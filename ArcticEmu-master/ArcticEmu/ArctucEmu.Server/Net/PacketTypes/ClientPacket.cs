using ArctucEmu.Server.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArctucEmu.Server.Net.PacketTypes
{
    public class ClientPacket
    {
		private readonly byte[] _packet;
		public ClientPacket(byte[] packet)
		{
			_packet = packet;
			Payload = new ClientPacketPayload(_packet);
			Length = NetUtils.ReadInt(new[] { packet[3], packet[2], packet[1], packet[0] });
			Unknown = NetUtils.ReadInt(new[] { packet[7], packet[6], packet[5], packet[4] });
			Opcode = (Opcode)NetUtils.ReadInt(new[] { packet[11], packet[10], packet[9], packet[8] });
		}

		public int Length { get; }
		public int Unknown;
		public Opcode Opcode;

		public ClientPacketPayload Payload { get; private set; }
	}
}
