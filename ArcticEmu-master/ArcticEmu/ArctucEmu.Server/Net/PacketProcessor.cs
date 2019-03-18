using ArctucEmu.Server.Net.PacketTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArctucEmu.Server.Net
{
    public class PacketProcessor
    {
		private readonly ClientHandlerBase _client;

		public byte[] Buffer { get; }

		public PacketProcessor(ClientHandlerBase client, int maxBufferSize)
		{
			_client = client;
			Buffer = new byte[maxBufferSize];
		}
		public void Process(int received)
		{
			var bytes = new byte[Buffer.Length + received];
			System.Buffer.BlockCopy(Buffer, 0, bytes, 0, received);

			using (var memoryStream = new MemoryStream(bytes))
			using (var binaryReader = new BinaryReader(memoryStream))
			{
				var position = 0;

				if(bytes.Length >= 4)
				{
					Int32 packetLength = binaryReader.ReadInt32();
					if(packetLength <= bytes.Length)
					{
						binaryReader.BaseStream.Seek(-sizeof(int), SeekOrigin.Current);
						var packet = new ClientPacket(binaryReader.ReadBytes(packetLength));

						OnPacketProcessed(packet);
					}

				}
			}
		}
		private void OnPacketProcessed(ClientPacket packet)
		{
			PacketProcessed?.Invoke(this, new PacketProcessedEventArgs(packet));
		}

		public event EventHandler<PacketProcessedEventArgs> PacketProcessed;
	}
}
