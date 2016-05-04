using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.IO;
using CustomUIControls;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WindChat
{
    public partial class Session : CCWin.Skin_Mac
    {
        protected Boolean m_bFlashFlag = false;
        protected List<CMessage> m_ChatHistory = new List<CMessage>();
        protected Mark m_mark; //唯一标识一个窗体


        protected ImageList imageList;
        protected ImageListPopup ilp;      // 表情选择框


        public Session(): base()
        {
            InitializeComponent();

            load();
        }

        ~Session()
        {
            
        }

        public Session(Mark mark_)
        {
            InitializeComponent();

            load();
            m_mark = mark_;
        }

        public Session(Mark mark_, CMessage message)
        {
            InitializeComponent();

            load();
            m_ChatHistory.Add(message);
            m_mark = mark_;
        }




        public void add_history(CMessage message)
        {

            this.HistoryTxt.Text += message.Send_name + " " + message.Send_time;
            this.HistoryTxt.Text += "\r\n";
            this.HistoryTxt.Text += "  " + message.Content;
            this.HistoryTxt.Text += "\r\n";
        }


        public void display()
        {
            this.show();
            // 加载内容
            foreach (CMessage message in m_ChatHistory)
            {
                AddMessage(message);
            }
        }


        private void OnDisplay(string time)
        {
            // 加载内容
            foreach(CMessage message in m_ChatHistory)
            {
                AddMessage(message);
            }
        }



        public void add_message(CMessage message)
        {

            m_ChatHistory.Add(message);
            AddMessage(message);
        }

        private void AddMessage(CMessage message)
        {
            
            SendedText.Text += message.Send_name + " " + message.Send_time;
            SendedText.Text += "\r\n";
            SendedText.Text += "  " + message.Content;
            SendedText.Text += "\r\n";
 
        }

        public delegate   void Del_ShowMessage(string message);
        public delegate void Del_AddMessage(string time, string message);


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


        private void load()
        {
            imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);
            imageList.ColorDepth = ColorDepth.Depth32Bit;       //32位的带alpha通道的可以直接透明
            imageList.Images.AddStrip(WindChatV0._1.Properties.Resources.face2);  //加载资源表情图片          

            ilp = new ImageListPopup();
            ilp.Init(imageList, 8, 8, 10, 2);   //水平、垂直线间距，表情显示的列和行
            ilp.ItemClick += new ImageListPopupEventHandler(OnItemClicked);

        }

        /************************************************************************/
        /* 选择了表情                                                  */
        /************************************************************************/
        public void OnItemClicked(object sender, ImageListPopupEventArgs e)
        {
            Image img = imageList.Images[e.SelectedItem];



            Clipboard.SetDataObject(img);
            SendingText.ReadOnly = false;
        }



        public virtual void show() { }
        public virtual void history_show()
        {
            if (HistoryTxt.Visible == false)
            {
                this.Width += 115;
            }
            else
            {
                this.Width -= 115;
            }
        }


        private void SendFile(byte[] file_data)
        {

        }






        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }


        public static void WirteFile(string file_name, byte[] file_byte)
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().Location);


            string path = dir + @"\File\" + file_name;
            FileStream fileStream = new FileStream(path, FileMode.Create);

            try
            {
                fileStream.Write(file_byte, 0, file_byte.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
            finally
            {
                fileStream.Close();
            }


            // 打开并选中
            string args = string.Format("/Select, {0}", path);
            System.Diagnostics.Process.Start(new ProcessStartInfo("Explorer.exe", args));


        }

        private void FaceBtn_Click(object sender, EventArgs e)
        {
            //Point pt = PointToScreen(new Point(FaceBtn.Left, FaceBtn.Top));

            //if (ilp.InvokeRequired)
            //{

            //    Dele_show _show = new Dele_show(ilp.Show);
            //    this.BeginInvoke(_show, pt.X, pt.Y - 80);
            //}
            //else
            //{
            //    ilp.Show(pt.X, pt.Y - 80);
            //}
            
        }

        public delegate void Dele_show(int x, int y);
        public delegate void Dele_Addmsg(CMessage message);

        private void SendBtn_Click(object sender, EventArgs e)
        {

            SendedText.Text += MainForm.m_strNick_name_ + " " + DateTime.Now.ToString();
            SendedText.Text += "\r\n";
            SendedText.Text += "  " + SendingText.Text;
            SendedText.Text += "\r\n";


            SendingText.Focus();
        }



        private void SendingText_DragEnter_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SendingText_DragDrop_1(object sender, DragEventArgs e)
        {
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];

            if (files.Count() == 0)
            {
                return;
            }

            // 提示玩家是否发送
            DialogResult dr = MessageBox.Show("是否发送文件？", "通知", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            else
            {
                SendFilePb(files[0]);
            }
        }


        private void SendFilePb(string file_path)
        {
            /// 获得文件数据
            byte[] file_data = ReadFile(file_path);
            if (file_data.Length == 0)
            {
                return;
            }

            byte[] file_bytes = Compression.compressBytes(file_data);

            IM.FileTrans file_trans = new IM.FileTrans();
            file_trans.ReqId = m_mark.Send_id;
            file_trans.TargetId = m_mark.Recv_id;
            file_trans.Name = file_path.Substring(file_path.LastIndexOf('\\') + 1);
            file_trans.Data = Google.Protobuf.ByteString.CopyFrom(file_bytes, 0, file_bytes.Length);

            // 发送文件
            MainForm.AsyncSend(MainForm.m_client, file_trans,
                (int)MsgTypeDef.Type.CUSTOM_MSG_FILE_TRANSLATION);


            string tips = "成功发送文件: " + file_trans.Name;

            CMessage message = new CMessage("", DateTime.Now.ToString(), tips);
            Dele_Addmsg _add = new Dele_Addmsg(this.add_message);
            this.BeginInvoke(_add, message);


        }
        private void OpenFileBtn_Click(object sender, EventArgs e)
        {



            //InitialDirectory 默认打开文件夹的位置  
            openFileDialog.InitialDirectory = "d:\\";
            //Filter 允许打开文件的格式  显示在Dialg中的Files of Type  
            openFileDialog.Filter = "Text files (*.txt)| *.txt | Pdf file (*.pdf)|*.pdf| All files (*.*)|*.*";
            //显示在Dialg中的Files of Type的选择  
            openFileDialog.FilterIndex = 1;

            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "选择文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //文件路径 和文件名字  
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                // 获取文件后缀  
                string fileExtension = Path.GetExtension(filePath);


                DialogResult dr = MessageBox.Show("是否发送文件: " + fileName + " ？", 
                    "通知", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                SendFilePb(filePath);

            }
        }

        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            if(HistoryTxt.Visible == false)
            {
                this.Width += 115;
            }
            else
            {
                this.Width -= 115;
            }

        }
    }
}
