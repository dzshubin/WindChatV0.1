using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindChat
{
    class MsgTypeDef
    {
        public enum Type
        {
            CUSTOM_MSG_FETCH_INFO           = 1000,
            CUSTOM_MSG_CHAT                 = 1001,
            CUSTOM_MSG_FETCH_CONTACTS       = 1002,





            SERVER_MSG_SEND_OFFLINE_MESSAGE = 10000,
        }
    }
}
