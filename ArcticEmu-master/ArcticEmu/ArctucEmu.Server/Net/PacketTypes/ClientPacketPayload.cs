using ArctucEmu.Server.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArctucEmu.Server.Net.PacketTypes
{
	public class ClientPacketPayload
	{
		public ClientPacketPayload(byte[] payload)
		{
			Position = 0;
			Payload = payload;
			Length = ReadInt();
			Unknown_01 = ReadInt();
			Opcode = (Opcode)ReadInt();

			//very ugly.
			PayloadBody = new byte[Payload.Length - 12];
			Buffer.BlockCopy(Payload, 12, PayloadBody, 0, Payload.Length - 12);
		}

		public int Position { get; private set; }

		/// <summary>
		///     The raw bytes of the payload.
		/// </summary>
		public byte[] Payload { get; private set; }

		public byte[] PayloadBody { get; private set; }

		public int Unknown_01 { get; }

		/// <summary>
		///     The length of the payload.
		/// </summary>
		public int Length { get; }

		/// <summary>
		///     The id of the payload (packet id).
		/// </summary>
		public Opcode Opcode { get; }

		/// <summary>
		///     Type of packet?
		/// </summary>
		public byte PacketType { get; }


		/// <summary>
		///     Reads a byte.
		/// </summary>
		/// <param name="position">Position where the byte is located.</param>
		/// <returns></returns>
		public byte ReadByte()
		{
			var x = Payload[Position];
			Position++;
			return x;
		}

		/// <summary>
		///     Reads a short in little-endian byte order.
		/// </summary>
		/// <param name="position">Position where the short starts.</param>
		/// <returns></returns>
		public short ReadShort()
		{
			var x = NetUtils.ReadShort(Payload, Position);
			Position += 2;
			return x;
		}

		/// <summary>
		///     Reads an integer in little-endian byte order.
		/// </summary>
		/// <param name="position">Position where the integer starts.</param>
		/// <returns></returns>
		public int ReadInt()
		{
			var x = NetUtils.ReadInt(Payload, Position);
			Position += 4;
			return x;
		}
		/// <summary>
		///     Future use.
		///     Future use.
		/// </summary>
		public void Verify()
		{

		}
	}
}
