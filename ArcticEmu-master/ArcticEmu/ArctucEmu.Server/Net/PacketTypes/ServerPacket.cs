using ArctucEmu.Server.Net.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArctucEmu.Server.Net.PacketTypes
{
	public class ServerPacket : IDisposable
	{
		private readonly MemoryStream _memory;

		private readonly BinaryWriter _writer;

		public ServerPacket(Opcode opcode)
		{
			Opcode = opcode;

			_memory = new MemoryStream();
			_writer = new BinaryWriter(_memory);

			// Packet header of 12 bytes.
			WriteInt(0);      // Length                   [0-1]
			WriteInt(200001); // unk_01
			WriteInt((int)opcode); // id? [2]
		}

		public Opcode Opcode { get; }

		public int Length { get; private set; }

		public byte[] PacketRaw { get; private set; }

		public ServerPacket WriteEmpty(int amount, byte value = 0x00)
		{
			for (var i = 0; i < amount; i++)
			{
				WriteByte(value);
			}

			return this;
		}
		public ServerPacket WriteByte(byte value)
		{
			_writer.Write(value);

			return this;
		}

		public ServerPacket WriteByte(sbyte value)
		{
			_writer.Write(value);

			return this;
		}

		public ServerPacket WriteByte(byte[] value)
		{
			_writer.Write(value);

			return this;
		}

		public ServerPacket WriteShort(short value)
		{
			_writer.Write(NetUtils.WriteShort(value));

			return this;
		}

		public ServerPacket WriteInt(int value)
		{
			_writer.Write(NetUtils.WriteInt(value));

			return this;
		}
		public ServerPacket WriteInt(uint value)
		{
			_writer.Write(NetUtils.WriteInt(value));

			return this;
		}
		public ServerPacket WriteStringUnicode(string value)
		{
			_writer.Write(Encoding.Unicode.GetBytes(value));

			return this;
		}
		public ServerPacket WriteBuffer(byte[] newBuffer)
		{
			_writer.Write(newBuffer);
			return this;
		}

		public ServerPacket WriteDouble(double value)
		{
			_writer.Write(value);
			return this;
		}
		public ServerPacket WriteInt2(int value)
		{
			_writer.Write(value);
			return this;

		}
		public byte[] ToArray()
		{
			PacketRaw = _memory.ToArray();

			//Puts the length on the right spot..
			Buffer.BlockCopy(NetUtils.WriteInt(PacketRaw.Length), 0, PacketRaw, 0, 4); 

			Length = PacketRaw.Length;
			
			return PacketRaw;
		}

		public void Dispose()
		{
			_memory?.Dispose();
			_writer?.Dispose();
		}
	}
}
