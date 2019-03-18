using ArctucEmu.Server;
using ArticEmu.Utils;
using NLog;
using System;
using System.Net;

namespace ArcticEmu.Channel
{
	class Program
	{
		//Authserver is running on port 56000.


		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private static bool _isRunning = true;

		static void Main(string[] args)
		{
			Console.Title = "ArcticEmu | AuthServer.";
			LogManager.Configuration = LoggerUtils.GetConfig();
			_logger.Warn("Starting up ArcticEmu.Auth..");

			using (var server = new Server<ClientHandler>(new IPEndPoint(IPAddress.Any, 55999)))
			{
				server.StartAccepting();

				while (_isRunning)
				{
					switch (Console.ReadLine())
					{
						case "quit":
							_isRunning = false;
							server.Dispose();
							break;
					}
				}

			}
		}
	}
}
