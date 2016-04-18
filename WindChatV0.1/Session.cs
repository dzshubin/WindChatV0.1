using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace WindChat
{
    public partial class Session : Form
    {

        private Boolean m_bFlashFlag = false;
        private List<string> m_ChatHistory = new List<string>();
        private Mark m_mark; //唯一标识一个窗体


        public Session()
        {
            InitializeComponent();
        }

        ~Session()
        {

        }

        public Session(Mark mark_)
        {
            InitializeComponent();
            m_mark = mark_;
        }

        public Session(Mark mark_, string context_)
        {
            InitializeComponent();
            m_ChatHistory.Add(context_);
            m_mark = mark_;
        }


        private void SendBtn_Click(object sender, EventArgs e)
        {
            IM.ChatPkt chatPkt = new IM.ChatPkt();
            if (SendingText.Text.Trim() == "")
            {
                MessageBox.Show("发送内容不能为空");
                return;
            }


            DateTime now = DateTime.Now;
            SendedText.Text += now.ToLongTimeString() + "\n" + "  " + SendingText.Text + "\n";


            chatPkt.Content = SendingText.Text;
            SendingText.Text = "";

            // 发送者ID、接受者ID、内容
            chatPkt.RecvId = m_mark.Recv_id;
            chatPkt.SendId = m_mark.Send_id;
            
            MainForm.AsyncSend(MainForm.m_client, chatPkt, 1001);
        }


        public void flash()
        {
            this.Hide();
            this.FlashTimer.Enabled = true;
            this.FlashNotify.Visible = true;
        }

        private void FlashTimer_Tick(object sender, EventArgs e)
        {
            if (m_bFlashFlag)
            {
                FlashNotify.Icon = new Icon("12.ico");
                m_bFlashFlag = !m_bFlashFlag;
            }
            else
            {
                FlashNotify.Icon = new Icon("13.ico");
                m_bFlashFlag = !m_bFlashFlag;
            }
        }

        public void display(string time)
        {
            this.Show();
            // 加载内容
            if (m_ChatHistory.Count != 0)
            {
                DateTime now = DateTime.Now;
                SendedText.Text += time + "\n" + "  ";
                foreach (string s in m_ChatHistory)
                {
                    SendedText.Text += s;
                }

                SendedText.Text += "\n";
            }
            else
            {

            }
        }


        public void FlashNotify_DoubleClick(object sender, EventArgs e)
        {
            FlashNotify.Visible = false;
            FlashTimer.Enabled = false;

            this.Show();

            // 加载内容
            if (m_ChatHistory.Count != 0)
            {
                DateTime now = DateTime.Now;
                SendedText.Text += now.ToLongTimeString() + "\n" + "  ";
                foreach (string s in m_ChatHistory)
                {
                    SendedText.Text += s;
                }

                SendedText.Text += "\n";
            }
            else
            {

            }


        }

        public void add_message(string context_, string time)
        {
            m_ChatHistory.Add(context_);

            DateTime now = DateTime.Now;
            this.SendedText.Text += time + "\n" + "  ";
            this.SendedText.Text += context_;
            this.SendedText.Text += "\n";
        }


        private void Session_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_mark != null)
            {
                SessionManager.get_instance().remove(m_mark);
            }
            
        }


        private void Session_Resize(object sender, EventArgs e)
        {
      

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;
            }
        }

        private void Session_Load(object sender, EventArgs e)
        {
        }

    }
}
