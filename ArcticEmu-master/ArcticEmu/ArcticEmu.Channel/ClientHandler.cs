using ArctucEmu.Server.Net;
using ArctucEmu.Server.Net.PacketTypes;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ArcticEmu.Channel
{
	internal class ClientHandler : ClientHandlerBase
	{

		public ClientHandler(Socket socket) : base(socket)
		{
			Processor.PacketProcessed += OnPacketProcessed;

		}
		private void OnPacketProcessed(object sender, PacketProcessedEventArgs e)
		{

		}
	}
}