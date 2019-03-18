using ArctucEmu.Server.Net;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ArctucEmu.Server
{
	public class Server<T> : IDisposable where T : ClientHandlerBase
	{
		private Logger _logger;
		private Socket _server;

		public Server(EndPoint endPoint)
		{
			_logger = LogManager.GetCurrentClassLogger();
			_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_server.Bind(endPoint);
			_server.Listen(5);
		}

		public void StartAccepting()
		{
			_logger.Trace("Waiting for incoming connection..");
			_server?.BeginAccept(AcceptCallback, null);
		}

		private void AcceptCallback(IAsyncResult res)
		{
			if (_server == null)
				return;

			var client = _server.EndAccept(res);
			var clientHandler = Activator.CreateInstance(typeof(T), client) as ClientHandlerBase;

			new Thread(() => clientHandler.WaitForData())
			{
				IsBackground = false
			}.Start();
		}

		public void Dispose()
		{
			_server?.Close();
			_server?.Dispose();
		}
	}
}
