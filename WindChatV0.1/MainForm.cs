using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindChat;
using System.Net;
using System.Threading;
using Google.Protobuf.Collections;
using CCWin;

namespace WindChat
{
    public partial class MainForm : Skin_Mac
    {

        public static string msgSvrIp;
        public static string msgSvrPort;

        public static string id;
        public static string passwd;

        public static ChatSession g_ChatSession = new ChatSession();
        public static TcpClient m_client;



        private string m_strName_ = string.Empty;
        private string m_strNick_name_ = string.Empty;
        private int m_nSex_ = 0;


        // 缓存频道信息
        public static Dictionary<int, IM.Channel> m_channels = new Dictionary<int, IM.Channel>();


        /************************************************************************
         *                                                                  
         *  网络相关处理
         * 
         */

        public class StateObject
        {
            // Client socket.
            public TcpClient workSocket = null;
            public static int m_HeadBuffLen = sizeof(Int32);
            public byte[] m_HeadBuff = new byte[m_HeadBuffLen];

            public byte[] m_BodyBuff;
            public int m_BodyLen;

            public byte[] m_SendByte;
        }



        public void AsyncReceive(TcpClient client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // 异步读取数据
                // 前面4个字节是头信息,表示数据长度
                client.GetStream().BeginRead(state.m_HeadBuff, 0, StateObject.m_HeadBuffLen,
                    new AsyncCallback(ReceiveHeadCallback), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReceiveHeadCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                TcpClient client = state.workSocket;

                client.GetStream().EndRead(ar);


                // get body len
                int data_len = BitConverter.ToInt32(state.m_HeadBuff, 0);
                int be_data_len = IPAddress.NetworkToHostOrder(data_len);
                //MessageBox.Show("消息总长度: " + be_data_len.ToString());



                state.m_BodyLen = be_data_len;

                state.m_BodyBuff = new byte[be_data_len];
                client.GetStream().BeginRead(state.m_BodyBuff, 0, be_data_len,
                    new AsyncCallback(ReceiveBodyCallback), state);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public void ReceiveBodyCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                TcpClient client = state.workSocket;

                client.GetStream().EndRead(ar);


                // get msg type
                // 前面4个字节
                byte[] byte_type = new byte[sizeof(Int32)];
                Array.Copy(state.m_BodyBuff, 0, byte_type, 0, sizeof(Int32));

                int type = BitConverter.ToInt32(byte_type, 0);
                int be_type = IPAddress.NetworkToHostOrder(type);



                // 类名长度
                byte[] byte_name_len = new byte[sizeof(Int32)];
                Array.Copy(state.m_BodyBuff, sizeof(Int32), byte_name_len, 0, sizeof(Int32));

                int name_len = BitConverter.ToInt32(byte_name_len, 0);
                int be_name_len = IPAddress.NetworkToHostOrder(name_len);
                //MessageBox.Show("name len: " + be_name_len.ToString());


                // 类名
                byte[] byte_name = new byte[be_name_len];
                Array.Copy(state.m_BodyBuff, sizeof(Int32) * 2, byte_name, 0, be_name_len);

                string type_name = Encoding.UTF8.GetString(byte_name);
                type_name = type_name.Substring(0, be_name_len - 1);
                //MessageBox.Show("type name: " + type_name);


                // get body
                int body_context_len = state.m_BodyLen - byte_type.Length - byte_name_len.Length - be_name_len;
                byte[] body_context = new byte[body_context_len];
                Array.Copy(state.m_BodyBuff, byte_type.Length + byte_name_len.Length + be_name_len, body_context, 0, body_context_len);


                object o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(type_name, false);
                Google.Protobuf.IMessage ms = o as Google.Protobuf.IMessage;
                ms = ms.Descriptor.Parser.ParseFrom(body_context);


                Process_Custom_Msg(be_type, ms);

                Array.Clear(state.m_HeadBuff, 0, StateObject.m_HeadBuffLen);
                client.GetStream().BeginRead(state.m_HeadBuff, 0, StateObject.m_HeadBuffLen,
                    new AsyncCallback(ReceiveHeadCallback), state);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        public static void SendCallBack(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;

            state.workSocket.GetStream().EndWrite(ar);
        }




        public static void AsyncSend(TcpClient client, Google.Protobuf.IMessage data, int type_)
        {

            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;


                byte[] stream = Google.Protobuf.MessageExtensions.ToByteArray(data);


                // 消息类型
                int be_type = IPAddress.HostToNetworkOrder(type_);
                byte[] byte_type = BitConverter.GetBytes(be_type);


                // 类名
                String type_name = data.Descriptor.FullName;
                string type_name_long = type_name + "\0";

                byte[] byte_name = Encoding.UTF8.GetBytes(type_name_long);
                MessageBox.Show("type name: " + type_name_long);

                // NameLen
                int name_len = type_name_long.Length;
                int be_name_len = IPAddress.HostToNetworkOrder(name_len);
                byte[] byte_name_len = BitConverter.GetBytes(be_name_len);


                // 数据总长度
                int data_len = byte_type.Length + byte_name_len.Length + type_name_long.Length + stream.Length;
                int be_data_len = IPAddress.HostToNetworkOrder(data_len);
                byte[] byte_data_len = BitConverter.GetBytes(be_data_len);





                state.m_SendByte = new byte[data_len + sizeof(Int32)];
                Array.Copy(byte_data_len, 0, state.m_SendByte, 0, sizeof(Int32));
                Array.Copy(byte_type, 0, state.m_SendByte, sizeof(Int32), sizeof(Int32));
                Array.Copy(byte_name_len, 0, state.m_SendByte, sizeof(Int32) * 2, sizeof(Int32));
                Array.Copy(byte_name, 0, state.m_SendByte, sizeof(Int32) * 3, byte_name.Length);
                Array.Copy(stream, 0, state.m_SendByte, sizeof(Int32) * 3 + byte_name.Length, stream.Length);


                // 异步读取数据
                // 前面4个字节是头信息,表示数据长度
                client.GetStream().BeginWrite(state.m_SendByte, 0, state.m_SendByte.Length,
                    new AsyncCallback(SendCallBack), state);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }





        /************************************************************************
         *                                                                  
         * 控件事件处理
         * 
         */

        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                m_client = new TcpClient(msgSvrIp, int.Parse(msgSvrPort));


                OnLogin();
                OnLoad();

                // 异步读取数据
                // 前面4个字节是头信息,表示数据长度
                AsyncReceive(m_client);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private bool IsChannelNode(TreeNode selected)
        {
            TreeNode[] ChannelNodes = this.ImTreeView.Nodes.Find("Channel", true);
            if (ChannelNodes.Length != 0)
            {
                return ChannelNodes[0].Nodes.Contains(selected);
            }
            else
            {
                // error
                return false;
            }
        }


        private void SelectChat(object sender, MouseEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;

            if (selectedNode.Level != 0 && selectedNode.Index != -1)
            {

                // 频道
                if (IsChannelNode(selectedNode))
                {

                    IM.ChannelBase base_info = selectedNode.Tag as IM.ChannelBase;
                    Mark mark = new Mark(int.Parse(id), base_info.ChannelId);


                    // 玩家不在频道内部
                    if (!base_info.IsInside)
                    {
                        // 提示玩家是否加入
                        DialogResult dr = MessageBox.Show("是否加入频道？", "通知", MessageBoxButtons.YesNoCancel,
                                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        if (dr != DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            // 加入频道
                            IM.OperateChannel join_req = new IM.OperateChannel();
                            IM.OperateReqBase req_base =  new IM.OperateReqBase();

                            req_base.UserId = Int64.Parse(id);
                            req_base.ChannelId = base_info.ChannelId;

                            join_req.ReqBase = req_base;
                            AsyncSend(m_client, join_req, (int)MsgTypeDef.Type.CUSTOM_MSG_JOIN_CHANNEL);
                        }
                    }
                    else
                    {
                        MessageBox.Show("发送者id: " + id + "【频道id】: " + base_info.ChannelId.ToString());
                        Session session = SessionManager.get_instance().get_session(mark);

                        if (session == null)
                        {
                            IM.Channel channel = m_channels[base_info.ChannelId]; ;

                            ChannelSession channel_session = new ChannelSession(mark);
                            channel_session.Text = channel.Name;

                            SessionManager.get_instance().insert(mark, channel_session);


                            // 加载成员信息
                            Dele_AddChannelMember _load_memeber = new Dele_AddChannelMember(channel_session.load_members);
                            this.BeginInvoke(_load_memeber, channel);


                            // 展示频道窗口
                            channel_session.show();

                        }
                        else
                        {
                            // 任务栏提醒
                            session.ShowInTaskbar = true;
                        }

                    }



                }
                else
                {
                    IM.User user = selectedNode.Tag as IM.User;
                    Mark m = new Mark(Int64.Parse(id), user.Id);

                    MessageBox.Show("发送者id: " + id + "接受者id: " + user.Id.ToString());

                    Session ChatSession = SessionManager.get_instance().get_session(m);

                    if (ChatSession == null)
                    {
                        ChatSession = new ChatSession(m);
                        ChatSession.Text = user.NickName;

                        SessionManager.get_instance().insert(m, ChatSession);
                        ChatSession.show();
                    }
                    else
                    {
                        // 任务栏提醒
                        ChatSession.ShowInTaskbar = true;
                    }
                }


            }
            else
            {
                // 顶级节点
            }
        }



        /************************************************************************
         *                                                                  
         * 自定义事件处理
         * 
         */

        private void Process_Custom_Msg(int msg_type_, Google.Protobuf.IMessage ms)
        {
            switch (msg_type_)
            {
                case (int)MsgTypeDef.Type.CUSTOM_MSG_CHAT:
                    handle_chat(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_CHAT:
                    handle_channel_chat(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_CONTACTS:
                    handle_fetch_contacts(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_INFO:
                    handle_fetch_info(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_SEND_OFFLINE_MESSAGE:
                    handle_fetch_offline_message(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_CHANNEL_OFFLINE_MESSAGE:
                    handle_fetch_channel_offline_message(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_CHANNEL_MEMBER:
                    handle_pull_channel_member(ms);
                    break;
                case (int)MsgTypeDef.Type.SERVER_MSG_CHANNEL_BASE:
                    handle_fetch_fetch_channel(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_JOIN_CHANNEL:
                    handle_join_channel_ack(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_EXIT_CHANNEL:
                    handle_exit_channel_ack(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_USER_UPDATE:
                    handle_update_channel_user(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_CHANNEL_MEMBER_JOIN:
                    handle_channel_member_join(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_CHANNEL_MEMBER_EXIT:
                    handle_channel_member_exit(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_FILE_TRANSLATION:
                    handle_file_recv(ms);
                    break;

                default:
                    MessageBox.Show("invalid type !");
                    break;
            }
        }




        /************************************************************************
         *                                                                  
         *  handle function
         * 
         */
        public delegate void ProcessDelegate(string time);

        public void handle_chat(Google.Protobuf.IMessage ms)
        {

            var send_id_field = ms.Descriptor.FindFieldByName("send_id");
            var recv_id_field = ms.Descriptor.FindFieldByName("recv_id");
            var content_field = ms.Descriptor.FindFieldByName("content");
            var send_time_field = ms.Descriptor.FindFieldByName("send_time");


            Int64 send_id = Int64.Parse(send_id_field.Accessor.GetValue(ms).ToString());
            Int64 recv_id = Int64.Parse(recv_id_field.Accessor.GetValue(ms).ToString());
            string send_time = send_time_field.Accessor.GetValue(ms).ToString();

            Google.Protobuf.ByteString send_bytes 
                = content_field.Accessor.GetValue(ms) as Google.Protobuf.ByteString;

            byte[] content = new byte[send_bytes.Length];
            send_bytes.CopyTo(content, 0);

            if (send_id != 0 && recv_id != 0)
            {

                // 窗口是否存在
                Mark m = new Mark(recv_id, send_id);
                Session session = SessionManager.get_instance().get_session(m);


                if (session == null)
                {
                    MessageBox.Show("聊天窗口不存在！新建一个！");

                    ChatSession chat_session = new ChatSession(m, content);
                    SessionManager.get_instance().insert(m, chat_session);


                    IM.UserUpdateReq req = new IM.UserUpdateReq();
                    req.ReqId = long.Parse(MainForm.id);
                    req.TargetId = send_id;

                    MainForm.AsyncSend(m_client, req,
                        (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_USER_UPDATE);


                    Dele_Display _display = new Dele_Display(chat_session.display);
                    this.BeginInvoke(_display, send_time);

                }
                else
                {
                    MessageBox.Show("聊天窗口已存在！");
                    // 任务栏提醒
                    session.ShowInTaskbar = true;

                    Dele_AddMessage add_ = new Dele_AddMessage(session.add_message);
                    this.BeginInvoke(add_, content, send_time);

                }
            }
            else
            {
                MessageBox.Show("错误！！");
            }
        }



        private void handle_channel_chat(Google.Protobuf.IMessage ms)
        {
            var send_id_field   = ms.Descriptor.FindFieldByName("send_id");
            var recv_id_field   = ms.Descriptor.FindFieldByName("recv_id");
            var content_field   = ms.Descriptor.FindFieldByName("content");
            var send_time_field = ms.Descriptor.FindFieldByName("send_time");
            var channel_id_field = ms.Descriptor.FindFieldByName("channel_id");


            if (send_id_field == null || recv_id_field == null 
                || content_field == null || send_time_field == null 
                || channel_id_field == null)
            {
                MessageBox.Show("handle_channel_chat fatal error!");
                return;
            }



            Int64 send_id = Int64.Parse(send_id_field.Accessor.GetValue(ms).ToString());
            Int64 recv_id = Int64.Parse(recv_id_field.Accessor.GetValue(ms).ToString());
            string send_time = send_time_field.Accessor.GetValue(ms).ToString();
            int channel_id = int.Parse(channel_id_field.Accessor.GetValue(ms).ToString());


            Google.Protobuf.ByteString send_bytes
                = content_field.Accessor.GetValue(ms) as Google.Protobuf.ByteString;

            byte[] content = new byte[send_bytes.Length];
            send_bytes.CopyTo(content, 0);


            // 窗口是否存在
            Mark m = new Mark(recv_id, channel_id);
            Session session = SessionManager.get_instance().get_session(m);


            if (session == null)
            {
                ChannelSession ch_session = new ChannelSession(m, content);
                SessionManager.get_instance().insert(m, ch_session);

                IM.Channel channel = m_channels[channel_id];
                ch_session.Text = channel.Name;

                // 加载缓存数据
                Dele_AddChannelMember _add_memeber = new Dele_AddChannelMember(ch_session.load_members);
                this.BeginInvoke(_add_memeber, m_channels[channel_id]);

                Dele_Display _dispaly = new Dele_Display(ch_session.display);
                this.BeginInvoke(_dispaly, send_time);
            }
            else
            {
                MessageBox.Show("聊天窗口已存在！");
                // 任务栏提醒
                session.ShowInTaskbar = true;

                Dele_AddMessage add_ = new Dele_AddMessage(session.add_message);
                this.BeginInvoke(add_, content, send_time);

            }

        }


        private void handle_fetch_contacts(Google.Protobuf.IMessage ms)
        {

            var contacts_field = ms.Descriptor.FindFieldByName("contacts");

            if (contacts_field.IsRepeated)
            {
                RepeatedField<IM.User> contacts = (RepeatedField<IM.User>)(contacts_field.Accessor.GetValue(ms));

                if (contacts.Count != 0)
                {
                    foreach (IM.User user in contacts)
                    {
                        add_tree_node(this.ImTreeView, user);
                    }
                }
                else
                {
                    // 没有联系人
                }
            }
            else
            {
                MessageBox.Show("error!");
            }

        }


        private void handle_fetch_info(Google.Protobuf.IMessage ms)
        {

            // get descriptor
            var ms_descriptor = ms.Descriptor;

            // find field
            var sex_field = ms_descriptor.FindFieldByName("sex");
            var name_field = ms_descriptor.FindFieldByName("name");
            var nick_name_field = ms_descriptor.FindFieldByName("nick_name");



            string name = string.Empty;
            string nick_name = string.Empty;
            string sex = string.Empty;

            if (name_field != null)
            {
                name = name_field.Accessor.GetValue(ms).ToString();
            }

            if (nick_name_field != null)
            {
                nick_name = nick_name_field.Accessor.GetValue(ms).ToString();
            }

            if (sex_field != null)
            {
                sex = sex_field.Accessor.GetValue(ms).ToString();
            }



            m_strName_ = name;
            m_strNick_name_ = nick_name;
            m_nSex_ = int.Parse(sex);


            // set name
            set_label_text(this.Name_Label, nick_name);
            set_user_head(this.UserPictureBox);
        }


        void handle_fetch_channel_offline_message(Google.Protobuf.IMessage ms)
        {
            var offline_message_field = ms.Descriptor.FindFieldByName("channel_messages");

            if (!offline_message_field.IsRepeated)
            {
                MessageBox.Show("channel offline fatal error!");
                return;
            }

            RepeatedField<IM.ChannelChatPkt> offline_messages =
                (RepeatedField<IM.ChannelChatPkt>)(offline_message_field.Accessor.GetValue(ms));



            foreach(IM.ChannelChatPkt message in offline_messages)
            {
                long send_id        = message.SendId;
                int channel_id      = message.ChannelId;
                string send_time    = message.SendTime;

                byte[] content = new byte[message.Content.Length];
                message.Content.CopyTo(content, 0);

                // 窗口是否存在
                Mark m = new Mark(long.Parse(MainForm.id), channel_id);
                Session chSession = SessionManager.get_instance().get_session(m);


                if (chSession == null)
                {
                    MessageBox.Show("新建一个！发送者id: " + id + "频道id: " + channel_id);


                    ChannelSession session = new ChannelSession(m, content);
                    SessionManager.get_instance().insert(m, session);

                    IM.Channel channel = m_channels[channel_id];
                    session.Text = channel.Name;


                    // 加载缓存数据
                    Dele_AddChannelMember _add_memeber = new Dele_AddChannelMember(session.load_members);
                    this.BeginInvoke(_add_memeber, m_channels[channel_id]);

                    Dele_Display _display = new Dele_Display(session.display);
                    this.BeginInvoke(_display, send_time);

                }
                else
                {
                    // 任务栏提醒
                    chSession.ShowInTaskbar = true;

                    Dele_AddMessage add_ = new Dele_AddMessage(chSession.add_message);
                    this.BeginInvoke(add_, content, send_time);

                }

            }
        }

        void handle_fetch_offline_message(Google.Protobuf.IMessage ms)
        {

            
            var offline_message_field = ms.Descriptor.FindFieldByName("chat_message");

            if (offline_message_field.IsRepeated)
            {
                RepeatedField<IM.ChatPkt> offline_messages =
                    (RepeatedField<IM.ChatPkt>)(offline_message_field.Accessor.GetValue(ms));

                if (offline_messages.Count != 0)
                {
                    foreach (IM.ChatPkt message in offline_messages)
                    {
                        long send_id = message.SendId;
                        string send_time = message.SendTime;


                        byte[] content = new byte[message.Content.Length]; 
                        message.Content.CopyTo(content, 0);

                        // 窗口是否存在
                        Mark m = new Mark(int.Parse(id), send_id);
                        Session ChatSession = SessionManager.get_instance().get_session(m);


                        if (ChatSession == null)
                        {
                            MessageBox.Show("新建一个！发送者id: " + id + "接受者id: " + send_id);
                            ChatSession ch_session = new ChatSession(m, content);
                            SessionManager.get_instance().insert(m, ch_session);


                            IM.UserUpdateReq req = new IM.UserUpdateReq();
                            req.ReqId = long.Parse(MainForm.id);
                            req.TargetId = send_id;

                            MainForm.AsyncSend(m_client, req, 
                                (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_USER_UPDATE);


                            ProcessDelegate d = new ProcessDelegate(ch_session.display);
                            this.BeginInvoke(d, send_time);
                        }
                        else
                        {
                            // 任务栏提醒
                            ChatSession.ShowInTaskbar = true;

                            Dele_AddMessage add_ = new Dele_AddMessage(ChatSession.add_message);
                            this.BeginInvoke(add_, content, send_time);

                        }
                    }
                }
                else
                {
                    // 没有离线消息
                }
            }
            else
            {
                MessageBox.Show("error!");
            }
        }



        void handle_fetch_fetch_channel(Google.Protobuf.IMessage ms)
        {
            var channel_base_info_field = ms.Descriptor.FindFieldByName("channel_base");

            if (channel_base_info_field.IsRepeated == false)
            {
                MessageBox.Show("fatal error!");
            }
            else
            {
                RepeatedField<IM.ChannelBase> channel_base =
                    (RepeatedField<IM.ChannelBase>)(channel_base_info_field.Accessor.GetValue(ms));


                if (channel_base.Count == 0)
                {
                    MessageBox.Show("fatal error！");
                    return;
                }

                foreach (IM.ChannelBase base_info in channel_base)
                {
                    IM.Channel channel = new IM.Channel();

                    if (m_channels.ContainsKey(base_info.ChannelId))
                    {
                        channel = m_channels[base_info.ChannelId];
                    }
                    else
                    {
                        channel.Id = base_info.ChannelId;
                        m_channels.Add(base_info.ChannelId, channel);
                    }
                    channel.Name = base_info.ChannelName;

                    add_channel_node(this.ImTreeView, base_info);
                }
            }

        }


        void handle_pull_channel_member(Google.Protobuf.IMessage ms)
        {
            var channel_member_field = ms.Descriptor.FindFieldByName("channel_member");

            if (channel_member_field.IsRepeated == false)
            {
                MessageBox.Show("fatal error!");
            }
            else
            {
                RepeatedField<IM.ChannelMember> channel_members =
                    (RepeatedField<IM.ChannelMember>)(channel_member_field.Accessor.GetValue(ms));


                if (channel_members.Count == 0)
                {
                    MessageBox.Show("fatal error！");
                    return;
                }

                foreach (IM.ChannelMember member in channel_members)
                {
                    // 
                    Int32 id = member.ChannelId;


                    IM.Channel channel = m_channels[id];

                    foreach (IM.User user in member.Users)
                    {
                        channel.User.Add(user);
                    }
                }
            }
        }


        void handle_exit_channel_ack(Google.Protobuf.IMessage ms)
        {

            var req_base_field = ms.Descriptor.FindFieldByName("req_base");
            var result_field = ms.Descriptor.FindFieldByName("result");

            if (req_base_field == null || result_field == null)
            {
                MessageBox.Show("join ack error!");
                return;
            }

            int result = int.Parse(result_field.Accessor.GetValue(ms).ToString());
            IM.OperateReqBase req_base = req_base_field.Accessor.GetValue(ms) as IM.OperateReqBase;

            int channel_id = req_base.ChannelId;
            long user_id = req_base.UserId;

            if (result == 0)
            {
                MessageBox.Show("Join fail!");
                return;
            }

            MessageBox.Show("退出频道成功!");


            TreeNode[] channel_node = this.ImTreeView.Nodes.Find("Channel", true);
            if (channel_node.Count() == 0)
            {
                MessageBox.Show("exit ack! fatal error!");
                return;
            }
        


            TreeNode[] node = channel_node[0].Nodes.Find(channel_id.ToString(), true);
            TreeNode key_node = node[0];

            // 更新内存
            IM.ChannelBase base_info = key_node.Tag as IM.ChannelBase;
            base_info.IsInside = false;


            // 删除成员
            foreach(IM.User user in m_channels[channel_id].User)
            {
                if (user.Id == user_id)
                {
                    m_channels[channel_id].User.Remove(user);
                }
            }



            Mark mark = new Mark(Int64.Parse(id), channel_id);
            MessageBox.Show("发送者id: " + id + "【频道id】: " + base_info.ChannelId.ToString());
            Session session = SessionManager.get_instance().get_session(mark);

            if (session == null)
            {
            }
            else
            {
                // 删除
                SessionManager.get_instance().remove(mark);

                Dele_Show _close = new Dele_Show(session.Close);
                this.BeginInvoke(_close);
            }
        }


        void handle_join_channel_ack(Google.Protobuf.IMessage ms)
        {
            var req_base_field = ms.Descriptor.FindFieldByName("req_base");
            var result_field = ms.Descriptor.FindFieldByName("result");

            if (req_base_field == null  || result_field == null)
            {
                MessageBox.Show("join ack error!");
                return;
            }

            int result = int.Parse(result_field.Accessor.GetValue(ms).ToString());
            IM.OperateReqBase req_base = req_base_field.Accessor.GetValue(ms) as IM.OperateReqBase;

            int channel_id = req_base.ChannelId;


            if (result == 0)
            {
                MessageBox.Show("Join fail!");
                return;
            }

            MessageBox.Show("加入频道成功!");



            // 找到节点 设置已经加入
            TreeNode[] channel_node = this.ImTreeView.Nodes.Find("Channel", true);
            if (channel_node.Count() == 0)
            {
                MessageBox.Show("join ack! fatal error!");
                return;
            }


            TreeNode[] node = channel_node[0].Nodes.Find(channel_id.ToString(), true);
            TreeNode key_node = node[0];

            // 更新内存
            IM.ChannelBase base_info = key_node.Tag as IM.ChannelBase;
            base_info.IsInside = true;


            // 新成员
            IM.Channel channel = m_channels[channel_id];
            IM.User new_user = new IM.User();
            new_user.Id = long.Parse(id);
            new_user.Name = m_strName_;
            new_user.NickName = m_strNick_name_;
            new_user.Sex = m_nSex_.ToString();

            channel.User.Add(new_user);



            // 展示频道窗口
            Mark mark = new Mark(Int64.Parse(id), channel_id);
            MessageBox.Show("发送者id: " + id + "【频道id】: " + base_info.ChannelId.ToString());
            Session session = SessionManager.get_instance().get_session(mark);

            
            if (session == null)
            {


                ChannelSession channel_s = new ChannelSession(mark);
                channel_s.Text = channel.Name;

                SessionManager.get_instance().insert(mark, channel_s);

                if (m_channels.ContainsKey(channel_id))
                {
                    // 加载缓存数据
                    Dele_AddChannelMember _add_memeber = new Dele_AddChannelMember(channel_s.load_members);
                    this.BeginInvoke(_add_memeber, m_channels[channel_id]);
                }
                else
                {
                    MessageBox.Show("Not found channel id!");
                }


                // 显示
                Dele_Show _show = new Dele_Show(channel_s.show);
                this.BeginInvoke(_show);

            }
            else
            {
                // 任务栏提醒
                // error
                session.ShowInTaskbar = true;
            }

        }

        void handle_channel_member_join(Google.Protobuf.IMessage ms)
        {


            var channel_id_field = ms.Descriptor.FindFieldByName("channel_id");
            var new_member_field = ms.Descriptor.FindFieldByName("users");

            if (new_member_field.IsRepeated == false || channel_id_field == null)
            {
                MessageBox.Show("fatal error!");
            }
            else
            {

                int channel_id = int.Parse(channel_id_field.Accessor.GetValue(ms).ToString());
                RepeatedField<IM.User> new_members =
                    (RepeatedField<IM.User>)(new_member_field.Accessor.GetValue(ms));


                if (new_members.Count == 0)
                {
                    MessageBox.Show("fatal error！");
                    return;
                }



                foreach (IM.User new_user in new_members)
                {
                    IM.Channel channel = m_channels[channel_id];
                    channel.User.Add(new_user);

                    // 如果有session, 更新session
                    Mark mark = new Mark(long.Parse(id), channel_id);
                    ChannelSession session = (ChannelSession)SessionManager.get_instance().get_session(mark);

                    if (session != null)
                    {
                        Dele_AddChannelMember _reload_memeber = new Dele_AddChannelMember(session.load_members);
                        this.BeginInvoke(_reload_memeber, channel);
                    }
                    else
                    {
                        // 没有就算了
                    }
                }
            }
        }



        void handle_file_recv(Google.Protobuf.IMessage ms)
        {
            var req_id_field    = ms.Descriptor.FindFieldByName("req_id");
            var target_id_field = ms.Descriptor.FindFieldByName("target_id");
            var name_field      = ms.Descriptor.FindFieldByName("name");
            var data_field      = ms.Descriptor.FindFieldByName("data");


            if (req_id_field == null || req_id_field == null
                || name_field == null || data_field == null)
            {
                MessageBox.Show("handle_file_recv error!");
                return;
            }


            Int64 target_id = Int64.Parse(target_id_field.Accessor.GetValue(ms).ToString());
            if (target_id != Int64.Parse(MainForm.id))
            {
                MessageBox.Show("file recv fatal error");
                return;
            }

            Int64 send_id = Int64.Parse(req_id_field.Accessor.GetValue(ms).ToString());

            string file_name = name_field.Accessor.GetValue(ms).ToString();
            Google.Protobuf.ByteString file_data = data_field.Accessor.GetValue(ms)
                as Google.Protobuf.ByteString;



            // 展示窗口
            Mark mark = new Mark(Int64.Parse(id), send_id);
            Session session = SessionManager.get_instance().get_session(mark);


            if (session == null)
            {

                session = new ChatSession(mark);
                SessionManager.get_instance().insert(mark, session);

                IM.UserUpdateReq req = new IM.UserUpdateReq();
                req.ReqId = long.Parse(MainForm.id);
                req.TargetId = send_id;

                MainForm.AsyncSend(m_client, req,
                    (int)MsgTypeDef.Type.CUSTOM_MSG_CHANNEL_USER_UPDATE);


                // 显示
                Dele_Show _show = new Dele_Show(session.show);
                this.BeginInvoke(_show);

            }
            else
            {
                // 任务栏提醒
                // error
                session.ShowInTaskbar = true;
            }


            // 提示玩家是否接受
            DialogResult dr = MessageBox.Show("对方给您发送了文件:" + file_name + " 是否接受？", 
                "通知", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr != DialogResult.Yes)
            {
                return;
            }


            byte[] file_byte = new byte[file_data.Length];
            file_data.CopyTo(file_byte, 0);


            byte[] de_file_type = Compression.deCompressBytes(file_byte);
            Session.WirteFile(file_name, de_file_type);


            string message = "成功接受文件: " + file_name;
            Dele_AddMessage _add = new Dele_AddMessage(session.add_message);
            this.BeginInvoke(_add, Encoding.UTF8.GetBytes(message), DateTime.Now.ToString());

        }

        void handle_channel_member_exit(Google.Protobuf.IMessage ms)
        {
            var channel_id_field = ms.Descriptor.FindFieldByName("channel_id");
            var user_id_field = ms.Descriptor.FindFieldByName("user_id");

            if (channel_id_field == null || user_id_field == null)
            {
                MessageBox.Show("channel member join Pdu error!");
                return;
            }

            try
            {
                int channel_id = int.Parse(channel_id_field.Accessor.GetValue(ms).ToString());
                long user_id = long.Parse(user_id_field.Accessor.GetValue(ms).ToString());

                // 更新内存
                IM.Channel channel = m_channels[channel_id];
                foreach (IM.User user in channel.User)
                {
                    if (user.Id == user_id)
                    {
                        channel.User.Remove(user);
                        break;
                    }

                }

                // 如果有session, 更新session
                Mark mark = new Mark(long.Parse(id), channel_id);
                ChannelSession session = (ChannelSession)SessionManager.get_instance().get_session(mark);

                if (session != null)
                {
                    Dele_AddChannelMember _reload_memeber = new Dele_AddChannelMember(session.load_members);
                    this.BeginInvoke(_reload_memeber, m_channels[channel_id]);
                }
                else
                {
                    // 没有就算了
                }

            }
            catch
            {
                MessageBox.Show("handle_channel_member_change: fatal error!");
            }

        }


        void handle_update_channel_user(Google.Protobuf.IMessage ms)
        {
            var channel_id_field = ms.Descriptor.FindFieldByName("channel_id");
            var user_field = ms.Descriptor.FindFieldByName("user");

            IM.User user = user_field.Accessor.GetValue(ms) as IM.User;


            if (channel_id_field == null || user_field == null )
            {

                MessageBox.Show("fatal error!");
                return;
            }

            
            Int32 channel_id = int.Parse(channel_id_field.Accessor.GetValue(ms).ToString());

            // 更新缓存数据
            update_user_info(channel_id, user);

            Dele_Update_Session _update = new Dele_Update_Session(update_chat_session);
            this.BeginInvoke(_update, long.Parse(id), user);

        }



        /************************************************************************
         *                                                                  
         *   private tool function
         * 
         */

        void update_user_info(int channel_id, IM.User user)
        {
            if (channel_id == 0)
            {
                return;
            }

            try
            {
                IM.Channel channel = m_channels[channel_id];
                foreach(IM.User user_ in channel.User)
                {
                    if (user_.Id == user.Id)
                    {
                        user_.Name = user.Name;
                        user_.NickName = user.NickName;
                        user_.Sex = user.Sex;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }


            }
            catch
            {
                MessageBox.Show("Not find channel_id: " + channel_id);
            }
        }

        void update_chat_session(long send_id, IM.User user)
        {
            Mark mark = new Mark(send_id, user.Id);
            Session s = SessionManager.get_instance().get_session(mark);
            
            if (s != null)
            {
                s.Text = user.NickName;
            }
        }




        /************************************************************************
         *                                                                  
         *   
         * 
         */

        private void OnLogin()
        {
            IM.Account loginAccount = new IM.Account();
            loginAccount.Id = int.Parse(id);
            loginAccount.Passwd = passwd;

            AsyncSend(m_client, loginAccount, (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_INFO);
            AsyncSend(m_client, loginAccount, (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_CONTACTS);

        }

        // 控件增加一些提示
        private void OnLoad()
        {
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay   = 5000;
            toolTip1.InitialDelay   = 1000;
            toolTip1.ReshowDelay    = 500;
            toolTip1.ShowAlways     = true;

            toolTip1.SetToolTip(this.AddFriendBtn, "快来认识些新朋友吧！");

            
        }



        /************************************************************************
         *                                                                  
         *  委托定义
         * 
         */
        public delegate void Dele_Update_Session(long id_, IM.User user_);



        public delegate void Dele_Show();

        public delegate void Dele_Display(string time);


        public delegate void Dele_SetLabelText(Label l_, string s_);

        void set_label_text(Label set_label_, string text_)
        {
            Dele_SetLabelText _set = new Dele_SetLabelText((Label l_, string s_) =>
                {
                    l_.Text = s_;
                });

            this.Invoke(_set, set_label_, text_);
        }


        public delegate void Dele_AddTreeNode(TreeView tree, IM.User user);

        void add_node(TreeView tree, IM.User user)
        {
            TreeNode[] contactsNode = tree.Nodes.Find("Contacts", true);
            if (contactsNode.Length != 0)
            {
                TreeNode NewNode = new TreeNode();
                NewNode.Text = user.NickName;
                NewNode.Tag = user;

                // 2- woman 3-man
                int nIndex = int.Parse(user.Sex) == (int)WindChat.EnumDef.SEX.MAN ? 
                    (int)EnumDef.SEX_IMAGE_INDEX.MAN : (int)EnumDef.SEX_IMAGE_INDEX.WOMAN;

                NewNode.ImageIndex = nIndex;
                NewNode.SelectedImageIndex = nIndex;

                contactsNode[0].Nodes.Add(NewNode);
            }
            else
            {
                // error
                MessageBox.Show("找不到联系人这个节点！");
            }
        }


        void add_tree_node(TreeView tree, IM.User user)
        {
            Dele_AddTreeNode _add = new Dele_AddTreeNode(add_node);
            this.Invoke(_add, tree, user);
        }


        public delegate void Dele_AddChannelNode(TreeView tree, IM.ChannelBase user);

        void add_channel(TreeView tree, IM.ChannelBase base_info)
        {
            TreeNode[] ChannelNode = ImTreeView.Nodes.Find("Channel", true);
            if (ChannelNode.Length != 0)
            {
                TreeNode NewNode = new TreeNode();
                NewNode.Text = base_info.ChannelName;
                NewNode.Name = base_info.ChannelId.ToString();
                NewNode.Tag = base_info;
                NewNode.ContextMenuStrip = n_selection;


                int index = base_info.ChannelId % 100000 + 3;
                NewNode.ImageIndex = index;
                NewNode.SelectedImageIndex = index;


                ChannelNode[0].Nodes.Add(NewNode);
            }
            else
            {
                MessageBox.Show("Cant find channel root node!");
            }
        }


        void add_channel_node(TreeView tree, IM.ChannelBase base_info)
        {
            Dele_AddChannelNode _add = new Dele_AddChannelNode(add_channel);
            this.Invoke(_add, tree, base_info);
        }


        public delegate bool Dele_AddChannelMember(IM.Channel channel);





        public delegate void Dele_SetUserHead(PictureBox head);

        private void set_user_head(PictureBox head)
        {
            Dele_SetUserHead _set = new Dele_SetUserHead(set_head);
            this.Invoke(_set, head);
        }

        private void set_head(PictureBox head)
        {
            // 女性
            if (m_nSex_ != 0)
            {
                this.UserPictureBox.ImageLocation = "Icon/Woman64.png";
            } 
            else
            {
                this.UserPictureBox.ImageLocation = "Icon/LoginedUser64.png";
            }
        }


        private void AddFriendBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("add friend!");
        }


        public delegate void Dele_AddMessage(byte[] content, string  send_time);

        private void add_message(string content, string send_time)
        {

        }

        private void n_selection_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            TreeNode selectNode = ImTreeView.SelectedNode;
            IM.ChannelBase base_info = selectNode.Tag as IM.ChannelBase;

            if (e.ClickedItem.Name == "n_join")
            {
                if (base_info.IsInside == false)
                {
                    // 加入频道
                    IM.OperateChannel join_req = new IM.OperateChannel();
                    IM.OperateReqBase req_base = new IM.OperateReqBase();

                    req_base.UserId = Int64.Parse(id);
                    req_base.ChannelId = base_info.ChannelId;

                    join_req.ReqBase = req_base;
                    AsyncSend(m_client, join_req, (int)MsgTypeDef.Type.CUSTOM_MSG_JOIN_CHANNEL);
                }
                else

                {
                    MessageBox.Show("您已经加入该频道！");
                }
            }
            else
            {
                IM.OperateChannel exit_req = new IM.OperateChannel();
                IM.OperateReqBase req_base = new IM.OperateReqBase();

                req_base.UserId = Int64.Parse(id);
                req_base.ChannelId = base_info.ChannelId;

                exit_req.ReqBase = req_base;
                AsyncSend(m_client, exit_req, (int)MsgTypeDef.Type.CUSTOM_MSG_EXIT_CHANNEL);

            }
        }

        private void ImTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                ImTreeView.SelectedNode = ImTreeView.GetNodeAt(e.X, e.Y);
            }
        }

        private void ImTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
 