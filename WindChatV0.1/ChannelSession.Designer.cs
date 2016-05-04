namespace WindChat
{
    partial class ChannelSession
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelSession));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("频道成员");
            this.ChannelTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "");
            this.imageList.Images.SetKeyName(15, "");
            this.imageList.Images.SetKeyName(16, "");
            this.imageList.Images.SetKeyName(17, "");
            this.imageList.Images.SetKeyName(18, "");
            this.imageList.Images.SetKeyName(19, "");
            // 
            // SendBtn
            // 
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click_1);
            // 
            // PictureList
            // 
            this.PictureList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PictureList.ImageStream")));
            this.PictureList.Images.SetKeyName(0, "group.png");
            this.PictureList.Images.SetKeyName(1, "manBread.png");
            this.PictureList.Images.SetKeyName(2, "WomanShoe128.png");
            // 
            // SendedText
            // 
            this.SendedText.Lines = new string[0];
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
            // 
            // SendingText
            // 
            this.SendingText.Lines = new string[0];
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
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Click += new System.EventHandler(this.HistoryBtn_Click);
            // 
            // ChannelTree
            // 
            this.ChannelTree.ImageIndex = 0;
            this.ChannelTree.ImageList = this.PictureList;
            this.ChannelTree.Location = new System.Drawing.Point(490, 35);
            this.ChannelTree.Name = "ChannelTree";
            treeNode1.Name = "Members";
            treeNode1.SelectedImageIndex = -2;
            treeNode1.Text = "频道成员";
            this.ChannelTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ChannelTree.SelectedImageIndex = 0;
            this.ChannelTree.Size = new System.Drawing.Size(163, 519);
            this.ChannelTree.TabIndex = 3;
            this.ChannelTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Select_chat);
            // 
            // ChannelSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 560);
            this.Controls.Add(this.ChannelTree);
            this.Name = "ChannelSession";
            this.Load += new System.EventHandler(this.ChannelSession_Load);
            this.Controls.SetChildIndex(this.OpenFileBtn, 0);
            this.Controls.SetChildIndex(this.HistoryBtn, 0);
            this.Controls.SetChildIndex(this.SendedText, 0);
            this.Controls.SetChildIndex(this.SendingText, 0);
            this.Controls.SetChildIndex(this.SendBtn, 0);
            this.Controls.SetChildIndex(this.ChannelTree, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ChannelTree;
    }
}