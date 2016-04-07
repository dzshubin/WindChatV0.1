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
        private Dictionary<Mark, WindChat.Session> m_Sessions = new Dictionary<Mark, WindChat.Session>(new Mark.EqualityComparer());

        private Session m_session = new Session();

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

            foreach (var mark_ in m_Sessions.Keys)
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

            Boolean bFind = m_Sessions.ContainsKey(id_);

            if (!bFind)
            {
                return null;
            } 
            else
            {
                return m_Sessions[id_];
            }

        }

        public void add(Mark id_, string context_)
        {
            m_session = new Session(id_, context_);
            insert(id_, m_session);
        }


        public void add(Mark id_)
        {
            m_session = new Session(id_);
            insert(id_, m_session);


        }

        public Boolean insert(Mark id_, WindChat.Session Session_)
        {
            Boolean bFind = m_Sessions.ContainsKey(id_);

            if (bFind)
            {
                return false;
            } 
            else
            {
                m_Sessions.Add(id_, Session_);
                //m_Sessions.add()[id_] = Session_;
                return true;
            }
        }


        public void remove(Mark id_)
        {
            m_Sessions.Remove(id_);
        }
    }
}
