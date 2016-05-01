using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;


namespace WindChat
{
    public partial class LoginForm : Skin_Mac
    {
        private TcpClient tcpClient;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            ////// 发送登陆请求
            ////// 显示登陆结果
            IM.Account account = new IM.Account();
            account.Id = Convert.ToInt64(textBox1.Text);
            account.Passwd = textBox2.Text;


            byte[] stream = Google.Protobuf.MessageExtensions.ToByteArray(account);
            //MessageBox.Show("stream len: " + stream.Length.ToString());

            // 消息类型
            int type = 1000;
            int be_type = IPAddress.HostToNetworkOrder(type);
            byte[] by_type = BitConverter.GetBytes(be_type);


            // 类名
            String s = IM.Account.Descriptor.FullName;
            string s_long = s + "\0";

            byte[] by_s = Encoding.UTF8.GetBytes(s_long);


            // NameLen
            int s_len = s.Length + 1;
            int be_s_len = IPAddress.HostToNetworkOrder(s_len);
            byte[] by_s_len = BitConverter.GetBytes(be_s_len);


            // 数据总长度
            int data_len = sizeof(Int32) * 2 + by_s.Length + stream.Length;
            int be_data_len = IPAddress.HostToNetworkOrder(data_len);
            byte[] by_data_len = BitConverter.GetBytes(be_data_len);

           // MessageBox.Show("data_len: " + data_len);


            NetworkStream ns = tcpClient.GetStream();

            //MessageBox.Show("by_len: " + len.ToString());
            ns.Write(by_data_len, 0, by_data_len.Length);
            ns.Write(by_type, 0, by_type.Length);
            ns.Write(by_s_len, 0, by_s_len.Length);
            ns.Write(by_s, 0, by_s.Length);
            ns.Write(stream, 0, stream.Length);



            // read
            NetworkStream read_ns = tcpClient.GetStream();
            // read data len
            byte[] read_len = new byte[4];
            read_ns.Read(read_len, 0, read_len.Length);
            int len = BitConverter.ToInt32(read_len, 0);
            int be_len = IPAddress.NetworkToHostOrder(len);
            //MessageBox.Show("收到的数据总长度： " + be_len.ToString());



            byte[] pkt_data = new byte[be_len];
            read_ns.Read(pkt_data, 0, pkt_data.Length);


            // msg type
            byte[] byte_type = new byte[sizeof(Int32)];
            Array.Copy(pkt_data, 0, byte_type, 0, byte_type.Length);
            int read_type = BitConverter.ToInt32(byte_type, 0);
            int be_read_type = IPAddress.NetworkToHostOrder(read_type);
            //MessageBox.Show("收到的消息类型： " + be_read_type.ToString());

             

            // 类名长度
            byte[] byte_name_len = new byte[sizeof(Int32)];
            Array.Copy(pkt_data, byte_type.Length, byte_name_len, 0, byte_name_len.Length);
            int name_len = BitConverter.ToInt32(byte_name_len, 0);
            int be_name_len = IPAddress.NetworkToHostOrder(name_len);
            //MessageBox.Show("类名长度： " + be_name_len.ToString());



            byte[] read_name = new byte[be_name_len];
            Array.Copy(pkt_data, by_type.Length + byte_name_len.Length, read_name, 0, read_name.Length);

            string type_name = Encoding.UTF8.GetString(read_name);
            type_name = type_name.Substring(0, be_name_len - 1);
            MessageBox.Show("type name: " + type_name);


            // read data
            byte[] read_data = new byte[be_len - byte_type.Length - byte_name_len.Length- read_name.Length];
            Array.Copy(pkt_data, by_type.Length + byte_name_len.Length + read_name.Length, read_data, 0, read_data.Length);


            object o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(type_name, false);
            Google.Protobuf.IMessage ms = o as Google.Protobuf.IMessage;
            ms = ms.Descriptor.Parser.ParseFrom(read_data);



            var descriptor = ms.Descriptor;

            int result = 0;
            String ip = string.Empty;
            String port = string.Empty;

            
            
            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                int num = field.FieldNumber;
                string name = field.Name;
                string value = field.Accessor.GetValue(ms).ToString();


                switch (name)
                {
                    case "result":
                        result = int.Parse(value);
                        break;
                    case "ip":
                        ip = value;
                        break;
                    case "port":
                        port = value;
                        break;
                }

            }

            if (result == 0)
            {
                MessageBox.Show("登陆成功！");
                MainForm.msgSvrIp = "192.168.1.77";
                MainForm.msgSvrPort = port;

                MainForm.id = textBox1.Text.Trim();
                MainForm.passwd = textBox2.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
            else if (result == 2)
            {
                MessageBox.Show("没有可用的服务器！");
                this.DialogResult = DialogResult.Cancel;
            }
            else if(result == 3)
            {

                MessageBox.Show("已经登陆了！");
                this.DialogResult = DialogResult.Cancel;
            }

            else
            {
                MessageBox.Show("登陆失败！");
                this.DialogResult = DialogResult.Cancel;
            }

           ;    //返回一个登录成功的对话框状态
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //// 连接服务器
            try
            {

                tcpClient = new TcpClient("192.168.1.77", 9900);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
