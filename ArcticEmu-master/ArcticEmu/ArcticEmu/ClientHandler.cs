using ArctucEmu.Server.Net;
using ArctucEmu.Server.Net.PacketTypes;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ArcticEmu.Auth
{
	internal class ClientHandler : ClientHandlerBase
	{

		public ClientHandler(Socket socket) : base(socket)
		{
			Processor.PacketProcessed += OnPacketProcessed;

		}


		private void OnPacketProcessed(object sender, PacketProcessedEventArgs e)
		{
			var packet = e.ClientPacket.Payload;

			switch (packet.Opcode)
			{
				case Opcode.KeepAlive:

					break;
				case Opcode.ReqHash:
					{
						// 0 = HashField 0
						// 4 = HashField 1
						// 8 = HashField 2
						// 12 = HashField 3
						int hashField0 = packet.ReadInt();
						int hashField1 = packet.ReadInt();
						int hashField2 = packet.ReadInt();
						int hashField3 = packet.ReadInt();


						var rspPacket = new ServerPacket(Opcode.ReqHash + 1);
						rspPacket.WriteInt(0);
						rspPacket.WriteStringUnicode("Yo /0");
						Send(rspPacket);
					}

					break;
				case Opcode.ReqErrorReportServerAddress:
					{
						var rspPacket = new ServerPacket(Opcode.ReqErrorReportServerAddress + 1);
						rspPacket.WriteStringUnicode("127.0.0.1");
						rspPacket.WriteByte(0);
						rspPacket.WriteInt(55999);
						Send(rspPacket);
					}
					break;
				case Opcode.ReqClientChallenge:
					{
						var rspPacket = new ServerPacket(Opcode.ReqClientChallenge + 1);
						rspPacket.WriteStringUnicode("New key 0/");
						rspPacket.WriteInt(10381);
						Send(rspPacket);
					}
					break;
				case Opcode.ReqClientChecksum:
					{
						var rspPacket = new ServerPacket(Opcode.ReqClientChecksum + 1);
						//byte[] buffer1 = new byte[66];
						//Buffer.BlockCopy(packet.PayloadBody,0, buffer1, 0, 66);
						//byte[] version = BitConverter.GetBytes(400000.0);
						//Array.Reverse(version);
						rspPacket.WriteInt(0);
						//rspPacket.WriteBuffer(buffer1);
						//rspPacket.WriteBuffer(version);

						Send(rspPacket);

					}
					break;
				case Opcode.ReqLoginWithGlobal:
					{
						var rspPacket = new ServerPacket(Opcode.ReqLoginWithGlobal + 1);
						rspPacket.WriteInt(0);
						Send(rspPacket);
					}
					break;
				case Opcode.ReqLoginHouseTest:
					{
						var rspPacket = new ServerPacket(Opcode.ReqLoginHouseTest + 1); //general protection fault
						var d = new DateTime();
						rspPacket.WriteByte(0);
						rspPacket.WriteStringUnicode("d");
						rspPacket.WriteByte(0);
						rspPacket.WriteInt((int)d.Ticks);
						rspPacket.WriteInt(0);
						rspPacket.WriteInt(0);
						rspPacket.WriteInt(0);
						rspPacket.WriteInt(0);
						rspPacket.WriteInt(0);
						rspPacket.WriteByte(1);
						rspPacket.WriteStringUnicode("Y");
						rspPacket.WriteByte(0);
						rspPacket.WriteStringUnicode(d.ToString());
						rspPacket.WriteByte(1);
						rspPacket.WriteDouble(0);
						Send(rspPacket);
					}
					break;
				//case Opcode.ReqServerList:
					//{
						//var rspPacket = new ServerPacket(Opcode.ReqServerList + 1);
						//rspPacket.WriteEmpty(140, 0x01);
						//rspPacket.WriteByte(7);
						//rspPacket.WriteStringUnicode("delxze"); // servername;
						//string myString = "delxze";
						//rspPacket.WriteByte((byte)myString.Length);
						//rspPacket.WriteStringUnicode(myString);
					//	Send(rspPacket);
					//	break;
				//	}



						//rspPacket.WriteByte(0); 
						//	rspPacket.WriteStringUnicode("dx"); //server short name
						//	rspPacket.WriteByte(0);
						//	rspPacket.WriteStringUnicode("127.0.0.1"); // server ip
						//	rspPacket.WriteByte(0);
						//	rspPacket.WriteInt(55999); // serverport
						//	rspPacket.WriteStringUnicode("ChannelName"); // channelname
						//	rspPacket.WriteByte(0);
						//	rspPacket.WriteStringUnicode("CN"); // channelnickname
						///	rspPacket.WriteByte(0);
						//	rspPacket.WriteInt(1); // channelnum
						//		rspPacket.WriteInt(10); // max user
						//	rspPacket.WriteInt(0); // curr user
						//		rspPacket.WriteStringUnicode("Group name"); // group name
						//		rspPacket.WriteByte(0);
						//		rspPacket.WriteInt(0); // group id
						//		rspPacket.WriteInt(0); // group disp order
						//	rspPacket.WriteInt(0);// dataport
						//		rspPacket.WriteByte(0); // count
						//		rspPacket.WriteByte(0); // key
						//		rspPacket.WriteByte(0); // value
						//		rspPacket.WriteByte(0); // count
						//		rspPacket.WriteByte(0); // Key_int
						//		rspPacket.WriteInt(0); //Value int
						//		rspPacket.WriteByte(0); // count float;
						//		rspPacket.WriteByte(0); // key float;
						//		rspPacket.WriteInt(0); // value float;
						//		rspPacket.WriteByte(0); //clan channel;
						//		rspPacket.WriteByte(0); // is dedi
						//	Send(rspPacket);
						//		break;
						//}
						//}


						case Opcode.ReqCharInfo:
						{
							var rspPacket = new ServerPacket(Opcode.ReqCharInfo + 1);
							rspPacket.WriteInt(0);
							rspPacket.WriteStringUnicode("delxze");
							rspPacket.WriteByte(0);
							rspPacket.WriteInt(10);
							rspPacket.WriteInt(100);
							rspPacket.WriteStringUnicode("DX");
							rspPacket.WriteByte(0);
							Send(rspPacket);
						}
						break;
		case Opcode.ReqLogOut: 
			{
				var rspPacket = new ServerPacket(Opcode.ReqLogOut + 1);
				rspPacket.WriteByte(1);
				rspPacket.WriteByte(1);
				Send(rspPacket);
			}
			break;




			}
		}
	}
}
