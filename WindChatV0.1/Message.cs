using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindChat
{
    public class CMessage
    {
        private string send_name = String.Empty;
        private string send_time = String.Empty;
        private string content = String.Empty;

        public string Send_time
        {
            get
            {
                return send_time;
            }

            set
            {
                send_time = value;
            }
        }

        public string Send_name
        {
            get
            {
                return send_name;
            }

            set
            {
                send_name = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public CMessage(string send_name_, string send_time_, string content_)
        {
            Send_name = send_name_;
            Send_time = send_time_;
            Content = content_;
        }



    }
}
