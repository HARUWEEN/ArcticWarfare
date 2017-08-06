using System;

public class MasterServerClient
{
    public MasterServerClient();
    //{
    class MasterServerClient                  //
        }
    //}

enum EClientToMaster
    {
        CTM_Query,
        CTM_GetMOTD,
        CTM_QueryUpgrade,
        CTM_GetModMOTD,
        CTM_GetOwnageList
    };

    enum EQueryType
    {
        QT_Equals,
        QT_NotEquals,
        QT_LessThan,
        QT_LessThanEquals,
        QT_GreaterThan,
        QT_GreaterThanEquals,
        QT_Disabled
    };

    enum EResponseInfo
    {
        RI_AuthenticationFailed,
        RI_ConnectionFailed,
        RI_ConnectionTimeout,
        RI_Success,
        RI_MustUpgrade,
        RI_DevClient,
        RI_BadClient,
        RI_BannedClient,
        RI_UTANBan
    };

    enum EMOTDResponse
    {
        MR_MOTD,
        MR_MandatoryUpgrade,
        MR_OptionalUpgrade,
        MR_NewServer,
        MR_IniSetting,
        MR_Command
    };
//