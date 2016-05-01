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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("联系人", 0, 0);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("频道", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ImTreeView = new System.Windows.Forms.TreeView();
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.UserPictureBox = new System.Windows.Forms.PictureBox();
            this.Name_Label = new System.Windows.Forms.Label();
            this.AddFriendBtn = new System.Windows.Forms.Button();
            this.n_selection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.n_join = new System.Windows.Forms.ToolStripMenuItem();
            this.n_exit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.UserPictureBox)).BeginInit();
            this.n_selection.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImTreeView
            // 
            this.ImTreeView.ImageIndex = 0;
            this.ImTreeView.ImageList = this.IconList;
            this.ImTreeView.Location = new System.Drawing.Point(16, 104);
            this.ImTreeView.Name = "ImTreeView";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "Contacts";
            treeNode3.SelectedImageIndex = 0;
            treeNode3.Text = "联系人";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Channel";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "频道";
            this.ImTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.ImTreeView.SelectedImageIndex = 0;
            this.ImTreeView.Size = new System.Drawing.Size(256, 507);
            this.ImTreeView.TabIndex = 0;
            this.ImTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ImTreeView_AfterSelect);
            this.ImTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SelectChat);
            this.ImTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImTreeView_MouseUp);
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "contacts128.png");
            this.IconList.Images.SetKeyName(1, "channel128.png");
            this.IconList.Images.SetKeyName(2, "WomanShoe128.png");
            this.IconList.Images.SetKeyName(3, "manBread.png");
            this.IconList.Images.SetKeyName(4, "Game.png");
            this.IconList.Images.SetKeyName(5, "Music.png");
            this.IconList.Images.SetKeyName(6, "psy.png");
            this.IconList.Images.SetKeyName(7, "Social.png");
            this.IconList.Images.SetKeyName(8, "Brain.png");
            // 
            // UserPictureBox
            // 
            this.UserPictureBox.Location = new System.Drawing.Point(16, 34);
            this.UserPictureBox.Name = "UserPictureBox";
            this.UserPictureBox.Size = new System.Drawing.Size(64, 64);
            this.UserPictureBox.TabIndex = 3;
            this.UserPictureBox.TabStop = false;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Location = new System.Drawing.Point(93, 57);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(0, 12);
            this.Name_Label.TabIndex = 4;
            // 
            // AddFriendBtn
            // 
            this.AddFriendBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddFriendBtn.BackgroundImage")));
            this.AddFriendBtn.Location = new System.Drawing.Point(7, 646);
            this.AddFriendBtn.Name = "AddFriendBtn";
            this.AddFriendBtn.Size = new System.Drawing.Size(49, 44);
            this.AddFriendBtn.TabIndex = 6;
            this.AddFriendBtn.UseVisualStyleBackColor = true;
            this.AddFriendBtn.Click += new System.EventHandler(this.AddFriendBtn_Click);
            // 
            // n_selection
            // 
            this.n_selection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.n_join,
            this.n_exit});
            this.n_selection.Name = "n_selection";
            this.n_selection.Size = new System.Drawing.Size(101, 48);
            this.n_selection.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.n_selection_ItemClicked);
            // 
            // n_join
            // 
            this.n_join.Name = "n_join";
            this.n_join.Size = new System.Drawing.Size(100, 22);
            this.n_join.Text = "加入";
            // 
            // n_exit
            // 
            this.n_exit.Name = "n_exit";
            this.n_exit.Size = new System.Drawing.Size(100, 22);
            this.n_exit.Text = "退出";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 697);
            this.Controls.Add(this.AddFriendBtn);
            this.Controls.Add(this.Name_Label);
            this.Controls.Add(this.UserPictureBox);
            this.Controls.Add(this.ImTreeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UserPictureBox)).EndInit();
            this.n_selection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ImTreeView;
        private System.Windows.Forms.PictureBox UserPictureBox;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Button AddFriendBtn;
        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.ContextMenuStrip n_selection;
        private System.Windows.Forms.ToolStripMenuItem n_join;
        private System.Windows.Forms.ToolStripMenuItem n_exit;
    }
}