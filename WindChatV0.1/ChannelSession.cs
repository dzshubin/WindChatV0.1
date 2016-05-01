using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindChat
{
    public partial class ChannelSession : WindChat.Session
    {
        public ChannelSession()
            : base()
        {
            InitializeComponent();
        }


        ~ChannelSession()
        {
            InitializeComponent();
        }

        public ChannelSession(Mark mark_)
            : base(mark_)
        {
            InitializeComponent();
        }

        public ChannelSession(Mark mark_, byte[] context_)
            : base(mark_, context_)
        {
            InitializeComponent();
        }





        /*
         *
         *  事件处理
         *
         *
         */

        private void ChannelSession_Load(object sender, EventArgs e)
        {
            // 加载图片
            ChannelTree.ImageList = PictureList;
            TreeNode[] rootNode = ChannelTree.Nodes.Find("Members", true);
            rootNode[0].StateImageIndex = (int)CommDef.IconIndexDef.GROUP;
        }


        public override void show()
        {
            this.Show();
        }



        public bool load_members(IM.Channel channel)
        {
            TreeNode[] root = this.ChannelTree.Nodes.Find("Members", true);
            if (root.Count() == 0)
            {
                MessageBox.Show("fatal error! channelsession.load_members");
                return false;
            }

            else
            {
                root[0].Nodes.Clear();

                foreach (IM.User user in channel.User)
                {
                    TreeNode new_node = new TreeNode();
                    new_node.Text = user.NickName;
                    new_node.Name = channel.Id.ToString();

                    int nIndex = int.Parse(user.Sex) == (int)WindChat.EnumDef.SEX.MAN ?
                        (int)CommDef.IconIndexDef.MAN : (int)CommDef.IconIndexDef.WOMAN;

                    new_node.ImageIndex = nIndex;
                    new_node.SelectedImageIndex = nIndex;
                    new_node.StateImageIndex = nIndex;

                    new_node.Tag = user;
                    root[0].Nodes.Add(new_node);
                }
                return true;
            }
        }

        private void Select_chat(object sender, MouseEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;

            if (selectedNode.Level != 0 && selectedNode.Index != -1)
            {
                string channel_id = selectedNode.Name;
                IM.User user = selectedNode.Tag as IM.User;


                IM.UserUpdateReq update_req = new IM.UserUpdateReq();
                update_req.ChannelId = int.Parse(channel_id);
                update_req.ReqId = long.Parse(MainForm.id);
                update_req.TargetId  = user.Id;

                // 不是点的自己
                if(MainForm.id != user.Id.ToString())
                {
                    MainForm.AsyncSend(MainForm.m_client, update_req, 
                        (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_USER_UPDATE);

                }
                


                Mark mark = new Mark(long.Parse(MainForm.id), user.Id);
                Session session = SessionManager.get_instance().get_session(mark);
                if (session == null)
                {
                    Session chat_session = new ChatSession(mark);
                    chat_session.Text = user.NickName;
                    SessionManager.get_instance().insert(mark, chat_session);

                    // 展示频道窗口
                    chat_session.show();

                }
                else
                {
                    // 任务栏提醒
                    session.ShowInTaskbar = true;
                }

            }
            else
            {
                // 顶级节点
            }
        }

        private void SendBtn_Click_1(object sender, EventArgs e)
        {
            byte[] send_bytes = System.Text.Encoding.UTF8.GetBytes(this.SendingText.Text);
            SendingText.Text = "";//清空发送文本框



            IM.ChannelChatPkt channelPkt = new IM.ChannelChatPkt();
            channelPkt.ChannelId = (int)m_mark.Recv_id;
            channelPkt.SendId = long.Parse(MainForm.id);

            channelPkt.Content = 
                Google.Protobuf.ByteString.CopyFrom(send_bytes, 0, send_bytes.Length);


            MainForm.AsyncSend(MainForm.m_client, channelPkt, (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_CHAT);
        }
    }
}
