using System;
using System.Collections.Generic;
using System.Text;

namespace ArctucEmu.Server.Net
{
    public enum Opcode : int
    {
		OnConnectComplete = 0,
		//ReqLogin = 2, //?
        ReqCreateChar = 11,
		ReqServerList = 13,
		ReqChannelList = 15,
      //ReqConnectChannelServer = 15 //also 15 wtf?
		ReqEnterChannel = 17,
		ReqChannelUserList = 21,
		ReqLoginHouseTest = 25,
		ReqEnterRoom = 27,
		KeepAlive = 36,
		ReqCharInfo = 38,
		ReqErrorReportServerAddress = 56,
		ReqClientChecksum = 54,
		ReqClientChallenge = 52,
		ReqConnectGame = 66,
		ReqLoginWithGlobal = 67,
		ReqDisconnectGame = 68,
		ReqLogOut = 63,
		ReqHash = OnConnectComplete,
    }
}
