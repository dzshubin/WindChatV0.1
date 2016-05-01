using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindChat
{
    public partial class ChatSession : WindChat.Session
    {


        public ChatSession()
            :base()
        {
            InitializeComponent();
        }

        ~ChatSession()
        {

        }

        public ChatSession(Mark mark_)
            :base(mark_)
        {
            InitializeComponent();
        }

        public ChatSession(Mark mark_, byte[] context_)
            :base(mark_, context_)
        {
            InitializeComponent();

        }

        public override void show()
        {
            this.Show();
        }

        private void SendBtn_Click_1(object sender, EventArgs e)
        {
            byte[] send_bytes = System.Text.Encoding.UTF8.GetBytes(this.SendingText.Text);
            this.SendingText.Text = "";//清空发送文本框


            IM.ChatPkt chatPkt = new IM.ChatPkt();

            // 发送者ID、接受者ID、内容
            chatPkt.RecvId = m_mark.Recv_id;
            chatPkt.SendId = m_mark.Send_id;
            chatPkt.Content =
                Google.Protobuf.ByteString.CopyFrom(send_bytes, 0, send_bytes.Length);

            MainForm.AsyncSend(MainForm.m_client, chatPkt, (int)MsgTypeDef.Type.CUSTOM_MSG_CHAT);
        }

        private void SendedText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            return;
        }

        private void ChatSession_Load(object sender, EventArgs e)
        {

        }
    }
}
