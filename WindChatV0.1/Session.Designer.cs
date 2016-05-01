namespace WindChat
{
    partial class Session
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Session));
            this.SendBtn = new System.Windows.Forms.Button();
            this.PictureList = new System.Windows.Forms.ImageList(this.components);
            this.SendedText = new CCWin.SkinControl.SkinTextBox();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.SendingText = new CCWin.SkinControl.SkinTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(364, 529);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(115, 23);
            this.SendBtn.TabIndex = 2;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // PictureList
            // 
            this.PictureList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PictureList.ImageStream")));
            this.PictureList.TransparentColor = System.Drawing.Color.Transparent;
            this.PictureList.Images.SetKeyName(0, "group.png");
            this.PictureList.Images.SetKeyName(1, "manBread.png");
            this.PictureList.Images.SetKeyName(2, "WomanShoe128.png");
            // 
            // SendedText
            // 
            this.SendedText.BackColor = System.Drawing.Color.Transparent;
            this.SendedText.DownBack = null;
            this.SendedText.Icon = null;
            this.SendedText.IconIsButton = false;
            this.SendedText.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.SendedText.IsPasswordChat = '\0';
            this.SendedText.IsSystemPasswordChar = false;
            this.SendedText.Lines = new string[0];
            this.SendedText.Location = new System.Drawing.Point(8, 36);
            this.SendedText.Margin = new System.Windows.Forms.Padding(0);
            this.SendedText.MaxLength = 32767;
            this.SendedText.MinimumSize = new System.Drawing.Size(28, 28);
            this.SendedText.MouseBack = null;
            this.SendedText.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.SendedText.Multiline = true;
            this.SendedText.Name = "SendedText";
            this.SendedText.NormlBack = null;
            this.SendedText.Padding = new System.Windows.Forms.Padding(5);
            this.SendedText.ReadOnly = false;
            this.SendedText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SendedText.Size = new System.Drawing.Size(471, 312);
            // 
            // 
            // 
            this.SendedText.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SendedText.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendedText.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.SendedText.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.SendedText.SkinTxt.Multiline = true;
            this.SendedText.SkinTxt.Name = "BaseText";
            this.SendedText.SkinTxt.Size = new System.Drawing.Size(461, 302);
            this.SendedText.SkinTxt.TabIndex = 0;
            this.SendedText.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.SendedText.SkinTxt.WaterText = "";
            this.SendedText.TabIndex = 6;
            this.SendedText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SendedText.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.SendedText.WaterText = "";
            this.SendedText.WordWrap = true;
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(8, 356);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenFileBtn.TabIndex = 7;
            this.OpenFileBtn.Text = "文件";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // SendingText
            // 
            this.SendingText.AllowDrop = true;
            this.SendingText.BackColor = System.Drawing.Color.Transparent;
            this.SendingText.DownBack = null;
            this.SendingText.Icon = null;
            this.SendingText.IconIsButton = false;
            this.SendingText.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.SendingText.IsPasswordChat = '\0';
            this.SendingText.IsSystemPasswordChar = false;
            this.SendingText.Lines = new string[0];
            this.SendingText.Location = new System.Drawing.Point(8, 385);
            this.SendingText.Margin = new System.Windows.Forms.Padding(0);
            this.SendingText.MaxLength = 32767;
            this.SendingText.MinimumSize = new System.Drawing.Size(28, 28);
            this.SendingText.MouseBack = null;
            this.SendingText.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.SendingText.Multiline = true;
            this.SendingText.Name = "SendingText";
            this.SendingText.NormlBack = null;
            this.SendingText.Padding = new System.Windows.Forms.Padding(5);
            this.SendingText.ReadOnly = false;
            this.SendingText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SendingText.Size = new System.Drawing.Size(471, 141);
            // 
            // 
            // 
            this.SendingText.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SendingText.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendingText.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.SendingText.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.SendingText.SkinTxt.Multiline = true;
            this.SendingText.SkinTxt.Name = "BaseText";
            this.SendingText.SkinTxt.Size = new System.Drawing.Size(461, 131);
            this.SendingText.SkinTxt.TabIndex = 0;
            this.SendingText.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.SendingText.SkinTxt.WaterText = "";
            this.SendingText.TabIndex = 8;
            this.SendingText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SendingText.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.SendingText.WaterText = "";
            this.SendingText.WordWrap = true;
            this.SendingText.DragDrop += new System.Windows.Forms.DragEventHandler(this.SendingText_DragDrop_1);
            this.SendingText.DragEnter += new System.Windows.Forms.DragEventHandler(this.SendingText_DragEnter_1);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // Session
            // 
            this.AcceptButton = this.SendBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 555);
            this.Controls.Add(this.SendingText);
            this.Controls.Add(this.OpenFileBtn);
            this.Controls.Add(this.SendedText);
            this.Controls.Add(this.SendBtn);
            this.Name = "Session";
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Session_FormClosing);
            this.Load += new System.EventHandler(this.Session_Load);
            this.Resize += new System.EventHandler(this.Session_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button SendBtn;
        protected System.Windows.Forms.ImageList PictureList;
        protected CCWin.SkinControl.SkinTextBox SendedText;
        protected System.Windows.Forms.Button OpenFileBtn;
        protected CCWin.SkinControl.SkinTextBox SendingText;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}