using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticEmu.Utils
{
    public static class LoggerUtils
    {
		public static LoggingConfiguration GetConfig()
		{
			var config = new LoggingConfiguration();

			config.AddTarget(new ColoredConsoleTarget
			{
				Name = "ConsoleOutput",
				Layout = Layout.FromString("${shortdate} ${pad:padding=5:inner=${level:uppercase=true}} ${message} ${exception:format=tostring}"),
				ErrorStream = false,
				UseDefaultRowHighlightingRules = true
			});

			config.AddRule(LogLevel.Trace, LogLevel.Off, "ConsoleOutput");
			return config;
		}
    }
}
