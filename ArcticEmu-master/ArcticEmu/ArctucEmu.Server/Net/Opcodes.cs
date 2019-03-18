using System;
using System.Collections.Generic;
using System.Text;

namespace ArctucEmu.Server.Net
{
    public enum Opcode : int
    {
		OnConnectComplete = 0,
		ReqServerList = 13,
		ReqLoginHouseTest = 25,
		KeepAlive = 36,
		ReqCharInfo = 38,
		ReqErrorReportServerAddress = 56,
		ReqClientChecksum = 54,
		ReqClientChallenge = 52,
		ReqLoginWithGlobal = 67,
		ReqHash = OnConnectComplete,
        ReqChannelList = 23, //not the actual number
        ReqConnectChannelServer = 99, //not the actual number
    }
}