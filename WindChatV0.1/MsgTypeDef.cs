using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindChat
{
    public class MsgTypeDef
    {
        public enum Type
        {
            CUSTOM_MSG_FETCH_INFO                   = 1000,
            CUSTOM_MSG_CHAT                         = 1001,
            CUSTOM_MSG_FETCH_CONTACTS               = 1002,
            CUSTOM_MSG_JOIN_CHANNEL                 = 1003,
            CUSTOM_MSG_EXIT_CHANNEL                 = 1004,
            CUSTOM_MSG_CHANNEL_USER_UPDATE          = 1005,
            CUSTOM_MSG_CHANNEL_CHAT                 = 1006,
            CUSTOM_MSG_FILE_TRANSLATION             = 1007,         // 发送文件


            SERVER_MSG_SEND_OFFLINE_MESSAGE         = 10000,
            SERVER_MSG_CHANNEL_BASE                 = 10001,
            SERVER_MSG_CHANNEL_MEMBER               = 10002,
            SERVER_MSG_CHANNEL_MEMBER_JOIN          = 10003,
            SERVER_MSG_CHANNEL_MEMBER_EXIT          = 10004,
            SERVER_MSG_CHANNEL_OFFLINE_MESSAGE      = 10005,

        }
    }
}
