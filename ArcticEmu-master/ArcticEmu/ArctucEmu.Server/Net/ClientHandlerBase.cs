using ArctucEmu.Server.Net.PacketTypes;
using ArticEmu.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ArctucEmu.Server.Net
{
	public abstract class ClientHandlerBase
	{
		protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		protected readonly Socket Socket;
		protected readonly IPEndPoint Remote;

		protected ClientHandlerBase(Socket socket)
		{
			if (socket == null)
			{
				Logger.Fatal("Socket is not initialized");
				return;
			}
			Socket = socket;
			Remote = Socket.RemoteEndPoint as IPEndPoint;
			Processor = new PacketProcessor(this, 4096);
			Processor.PacketProcessed += ProcessorOnPacketProcessed;

		}

		public PacketProcessor Processor { get; }

		public void WaitForData()
		{
			if (Socket != null && Socket.Connected)
				Socket.BeginReceive(Processor.Buffer, 0, Processor.Buffer.Length, SocketFlags.None, ReceiveCallback, null);
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				var byteCount = Socket.EndReceive(ar, out var errorCode);
				if (byteCount <= 0 || errorCode != SocketError.Success)
				{
					Socket.Close();
					Socket.Dispose();

					Logger.Debug(errorCode != SocketError.Success
						? $" Connection with {Remote.Address} was forcibly dropped, socket error {errorCode}."
						: $" Connection from {Remote.Address} was dropped.");
					return;
				}

				Processor.Process(byteCount);
			}
			catch (SocketException exception)
			{
				Logger.Error(exception, $"An exception occured during receiving data.");
			}
			finally
			{
				WaitForData();
			}

		}


		public void Send(ServerPacket packet)
		{
			var rawPacket = packet.ToArray();

			// Debug to console.
			Logger.Fatal($"Sending packet:\n" +
						 $"\t{"Id",-9}: {packet.Opcode}\n" +
						 $"\t{"Length",-9}: {packet.Length}\n" +
						 $"\t{"Hex dump",-9}:\n" +
						 $"{HexUtils.HexDump(packet.PacketRaw)}");
			// Send to client.
			Socket.Send(rawPacket);

			// Clean-up packet resources.
			packet.Dispose();
		}


		private void ProcessorOnPacketProcessed(object sender, PacketProcessedEventArgs e)
		{
			var payload = e.ClientPacket.Payload;
			//Logger.Debug("Received packet");
			Logger.Debug($"Received packet:\n" +
						 $"\t{"Id",-9}: {payload.Opcode} ({(int)payload.Opcode})\n" +
						 $"\t{"Length",-9}: {payload.Length}\n" +
						 $"\t{"Packet type",-9}: {payload.PacketType} \n" +
						 $"\t{"Hex dump",-9}:\n" +
						 $"{HexUtils.HexDump(payload.Payload)}");
		}
	}
}
