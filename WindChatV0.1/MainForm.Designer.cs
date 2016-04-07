namespace WindChat
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("联系人", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("频道", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ImTreeView = new System.Windows.Forms.TreeView();
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.UserPictureBox = new System.Windows.Forms.PictureBox();
            this.Name_Label = new System.Windows.Forms.Label();
            this.AddFriendBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UserPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ImTreeView
            // 
            this.ImTreeView.ImageIndex = 0;
            this.ImTreeView.ImageList = this.IconList;
            this.ImTreeView.Location = new System.Drawing.Point(13, 72);
            this.ImTreeView.Name = "ImTreeView";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Contacts";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "联系人";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "ChatRoom";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "频道";
            this.ImTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.ImTreeView.SelectedImageIndex = 0;
            this.ImTreeView.Size = new System.Drawing.Size(259, 500);
            this.ImTreeView.TabIndex = 0;
            this.ImTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SelectFriendChat);
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "contacts128.png");
            this.IconList.Images.SetKeyName(1, "channel128.png");
            this.IconList.Images.SetKeyName(2, "WomanShoe128.png");
            this.IconList.Images.SetKeyName(3, "manBread.png");
            // 
            // UserPictureBox
            // 
            this.UserPictureBox.Location = new System.Drawing.Point(4, 1);
            this.UserPictureBox.Name = "UserPictureBox";
            this.UserPictureBox.Size = new System.Drawing.Size(64, 64);
            this.UserPictureBox.TabIndex = 3;
            this.UserPictureBox.TabStop = false;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Location = new System.Drawing.Point(81, 27);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(0, 12);
            this.Name_Label.TabIndex = 4;
            // 
            // AddFriendBtn
            // 
            this.AddFriendBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddFriendBtn.BackgroundImage")));
            this.AddFriendBtn.Location = new System.Drawing.Point(12, 583);
            this.AddFriendBtn.Name = "AddFriendBtn";
            this.AddFriendBtn.Size = new System.Drawing.Size(49, 44);
            this.AddFriendBtn.TabIndex = 6;
            this.AddFriendBtn.UseVisualStyleBackColor = true;
            this.AddFriendBtn.Click += new System.EventHandler(this.AddFriendBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 639);
            this.Controls.Add(this.AddFriendBtn);
            this.Controls.Add(this.Name_Label);
            this.Controls.Add(this.UserPictureBox);
            this.Controls.Add(this.ImTreeView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UserPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ImTreeView;
        private System.Windows.Forms.PictureBox UserPictureBox;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Button AddFriendBtn;
        private System.Windows.Forms.ImageList IconList;
    }
}