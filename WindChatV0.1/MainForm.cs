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


namespace WindChat
{
    public partial class MainForm : Form
    {

        public static string msgSvrIp;
        public static string msgSvrPort;

        public static string id;
        public static string passwd ;

        public static Session g_session = new Session();
        public static TcpClient m_client;



        private string m_strName_ = string.Empty;
        private string m_strNick_name_ = string.Empty;
        private int m_nSex_ = 0;






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



        public  void AsyncReceive(TcpClient client)
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

        public  void ReceiveHeadCallback(IAsyncResult ar)
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
                MessageBox.Show("消息总长度: " + be_data_len.ToString());



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
                MessageBox.Show("name len: " + be_name_len.ToString());
                

                // 类名
                byte[] byte_name = new byte[be_name_len];
                Array.Copy(state.m_BodyBuff, sizeof(Int32)*2, byte_name, 0, be_name_len);

                string type_name = Encoding.UTF8.GetString(byte_name);
                type_name = type_name.Substring(0, be_name_len - 1);
                MessageBox.Show("type name: " + type_name);


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


        private void SelectFriendChat(object sender, MouseEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;

            if (selectedNode.Level != 0 && selectedNode.Index != -1)
            {

                IM.User user = selectedNode.Tag as IM.User;
                Mark m = new Mark(Int64.Parse(id), user.Id);

                MessageBox.Show("发送者id: " + id + "接受者id: " + user.Id.ToString());

                Session session = SessionManager.get_instance().get_session(m);

                if (session == null)
                {
                    session = new Session(m);
                    SessionManager.get_instance().insert(m, session);
                    session.Show();
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
                    MessageBox.Show("chat!!");
                    handle_chat(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_CONTACTS:
                    MessageBox.Show("fetch contacts ack!");
                    handle_fetch_contacts(ms);
                    break;

                case (int)MsgTypeDef.Type.CUSTOM_MSG_FETCH_INFO:
                    MessageBox.Show("Read user info ack!");
                    handle_fetch_info(ms);
                    break;

                case (int)MsgTypeDef.Type.SERVER_MSG_SEND_OFFLINE_MESSAGE:
                    MessageBox.Show("fetch offline message!");
                    handle_fetch_offline_message(ms);
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


            Int64 send_id = 0;
            Int64 recv_id = 0;
            string content = string.Empty;
            string send_time = string.Empty;

            if (send_id_field != null)
            {
                send_id = Int64.Parse(send_id_field.Accessor.GetValue(ms).ToString());
            }

            if (recv_id_field != null)
            {
                recv_id = Int64.Parse(recv_id_field.Accessor.GetValue(ms).ToString());
            }

            if (content_field != null)
            {
                content = content_field.Accessor.GetValue(ms).ToString();
            }

            if(send_time_field != null)
            {
                send_time = send_time_field.Accessor.GetValue(ms).ToString();
            }

            if (send_id != 0 && recv_id != 0)
            {


                // 窗口是否存在
                Mark m = new Mark(recv_id, send_id);
                Session session = SessionManager.get_instance().get_session(m);


                if (session == null)
                {
                    MessageBox.Show("聊天窗口不存在！新建一个！");

                    session = new Session(m, content);
                    SessionManager.get_instance().insert(m, session);

                    ProcessDelegate d = new ProcessDelegate(session.display);
                    this.BeginInvoke(d, send_time);
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
                        //MessageBox.Show("id: " + user.Id);
                        //MessageBox.Show("name: " + user.Name);
                        //MessageBox.Show("name: " + user.NickName);
                       // MessageBox.Show("sex: " + user.Sex);

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
            var sex_field       = ms_descriptor.FindFieldByName("sex");
            var name_field      = ms_descriptor.FindFieldByName("name");
            var nick_name_field = ms_descriptor.FindFieldByName("nick_name");
            


            string name         = string.Empty;
            string nick_name    = string.Empty;
            string sex          = string.Empty;

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
                        string content = message.Content;
                        string send_time = message.SendTime;


                        // 窗口是否存在
                        Mark m = new Mark(int.Parse(id), send_id);
                        Session session = SessionManager.get_instance().get_session(m);


                        if (session == null)
                        {
                            session = new Session(m, content);
                            SessionManager.get_instance().insert(m, session);

                            ProcessDelegate d = new ProcessDelegate(session.display);
                            this.BeginInvoke(d, send_time);
                        }
                        else
                        {
                            // 任务栏提醒
                            session.ShowInTaskbar = true;

                            Dele_AddMessage add_ = new Dele_AddMessage(session.add_message);
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


        public delegate void Dele_AddMessage(string content, string  send_time);

        private void add_message(string content, string send_time)
        {

        }


    }
}
 