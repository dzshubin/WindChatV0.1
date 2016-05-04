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
            CUSTOM_MSG_FETCH_INFO                   = 1000,         // 获取玩家信息
            CUSTOM_MSG_CHAT                         = 1001,         // 聊天
            CUSTOM_MSG_FETCH_CONTACTS               = 1002,         // 获取联系人信息
            CUSTOM_MSG_JOIN_CHANNEL                 = 1003,         // 加入频道
            CUSTOM_MSG_EXIT_CHANNEL                 = 1004,         // 离开频道
            CUSTOM_MSG_CHANNEL_USER_UPDATE          = 1005,         // 更新用户信息频道
            CUSTOM_MSG_CHANNEL_CHAT                 = 1006,         // 频道聊天
            CUSTOM_MSG_FILE_TRANSLATION             = 1007,         // 发送文件
            CUSTOM_MSG_FETCH_HISTORY                = 1008,         // 请求与某个用户的历史消息
            CUSTOM_MSG_FETCH_CHANNEL_HISTORY        = 1009,         // 请求频道历史消息


            SERVER_MSG_SEND_OFFLINE_MESSAGE         = 10000,
            SERVER_MSG_CHANNEL_BASE                 = 10001,
            SERVER_MSG_CHANNEL_MEMBER               = 10002,
            SERVER_MSG_CHANNEL_MEMBER_JOIN          = 10003,
            SERVER_MSG_CHANNEL_MEMBER_EXIT          = 10004,
            SERVER_MSG_CHANNEL_OFFLINE_MESSAGE      = 10005,

        }
    }
}
