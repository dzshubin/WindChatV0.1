using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindChat;



namespace WindChat
{
    class SessionManager
    {
        private static SessionManager m_Instance = null;

        private Dictionary<Mark, WindChat.Session> m_sessions 
            = new Dictionary<Mark, WindChat.Session>(new Mark.EqualityComparer());

        private ChatSession m_ChatSession = new ChatSession();

        private SessionManager() 
        {

        }

        public static SessionManager get_instance()
        {
            if (m_Instance == null)
            {
                m_Instance = new SessionManager();
            }
            return m_Instance;
        }

        private Boolean find(Mark id_)
        {
            Boolean bFind = false;

            foreach (var mark_ in m_sessions.Keys)
            {
                if (mark_ == id_)
                {
                    bFind = true;
                    break;
                }

            }
            return bFind;
        }


        public WindChat.Session get_session(Mark id_)
        {

            Boolean bFind = m_sessions.ContainsKey(id_);

            if (!bFind)
            {
                return null;
            } 
            else
            {
                return m_sessions[id_];
            }

        }

        public Boolean insert(Mark id_, WindChat.Session session_)
        {
            Boolean bFind = m_sessions.ContainsKey(id_);

            if (bFind)
            {
                return false;
            } 
            else
            {
                m_sessions.Add(id_, session_);
                return true;
            }
        }


        public void remove(Mark id_)
        {
            m_sessions.Remove(id_);
        }
    }
}
