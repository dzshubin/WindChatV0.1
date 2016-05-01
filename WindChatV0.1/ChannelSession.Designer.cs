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
            // ChannelTree
            // 
            this.ChannelTree.ImageIndex = 0;
            this.ChannelTree.ImageList = this.PictureList;
            this.ChannelTree.Location = new System.Drawing.Point(496, 35);
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
            this.ClientSize = new System.Drawing.Size(664, 561);
            this.Controls.Add(this.ChannelTree);
            this.Name = "ChannelSession";
            this.Text = "";
            this.Load += new System.EventHandler(this.ChannelSession_Load);
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