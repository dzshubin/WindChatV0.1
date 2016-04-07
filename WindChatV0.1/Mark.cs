using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindChat
{
    public class Mark
    {

        public Mark(Int64 send_id_, Int64 recv_id_)
        {
            m_send_id = send_id_;
            m_recv_id = recv_id_;
        }



        private Int64 m_send_id;

        public Int64 Send_id
        {
            get { return m_send_id; }
            set { m_send_id = value; }
        }
        private Int64 m_recv_id;

        public Int64 Recv_id
        {
            get { return m_recv_id; }
            set { m_recv_id = value; }
        }



        public class EqualityComparer : IEqualityComparer<Mark>
        {
            public int GetHashCode(Mark key)
            {
                return (int)(key.m_send_id ^ key.Recv_id);
            }

            public bool Equals(Mark m1, Mark m2)
            {
                return m1.Recv_id == m2.Recv_id && m1.Send_id == m2.Send_id;
            }
        }

    }
}
