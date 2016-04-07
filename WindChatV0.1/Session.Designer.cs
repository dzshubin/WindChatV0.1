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
            this.SendingText = new System.Windows.Forms.RichTextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.SendedText = new System.Windows.Forms.RichTextBox();
            this.Font = new System.Windows.Forms.Label();
            this.FlashNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.FlashTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // SendingText
            // 
            this.SendingText.Location = new System.Drawing.Point(1, 376);
            this.SendingText.Name = "SendingText";
            this.SendingText.Size = new System.Drawing.Size(491, 146);
            this.SendingText.TabIndex = 0;
            this.SendingText.Text = "";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(364, 528);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(114, 23);
            this.SendBtn.TabIndex = 1;
            this.SendBtn.Text = "发送";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // SendedText
            // 
            this.SendedText.Location = new System.Drawing.Point(1, 5);
            this.SendedText.Name = "SendedText";
            this.SendedText.Size = new System.Drawing.Size(491, 344);
            this.SendedText.TabIndex = 2;
            this.SendedText.Text = "";
            // 
            // Font
            // 
            this.Font.AutoSize = true;
            this.Font.Location = new System.Drawing.Point(1, 358);
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(29, 12);
            this.Font.TabIndex = 3;
            this.Font.Text = "字体";
            // 
            // FlashNotify
            // 
            this.FlashNotify.Text = "notifyIcon1";
            this.FlashNotify.Visible = true;
            this.FlashNotify.DoubleClick += new System.EventHandler(this.FlashNotify_DoubleClick);
            // 
            // FlashTimer
            // 
            this.FlashTimer.Interval = 400;
            this.FlashTimer.Tick += new System.EventHandler(this.FlashTimer_Tick);
            // 
            // Session
            // 
            this.AcceptButton = this.SendBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 563);
            this.Controls.Add(this.Font);
            this.Controls.Add(this.SendedText);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.SendingText);
            this.Name = "Session";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Session_FormClosing);
            this.Load += new System.EventHandler(this.Session_Load);
            this.Resize += new System.EventHandler(this.Session_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox SendingText;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.RichTextBox SendedText;
        private System.Windows.Forms.Label Font;
        private System.Windows.Forms.NotifyIcon FlashNotify;
        private System.Windows.Forms.Timer FlashTimer;

    }
}