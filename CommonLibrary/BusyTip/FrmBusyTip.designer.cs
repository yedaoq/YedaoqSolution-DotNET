namespace CommonLibrary.BusyTip
{
    partial class FrmBusyTip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBusyTip));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labTip = new System.Windows.Forms.Label();
            this.LabCancel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labTip
            // 
            this.labTip.AutoEllipsis = true;
            this.labTip.AutoSize = true;
            this.labTip.Location = new System.Drawing.Point(90, 52);
            this.labTip.MaximumSize = new System.Drawing.Size(490, 12);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(119, 12);
            this.labTip.TabIndex = 1;
            this.labTip.Text = "正在计算整定参数...";
            // 
            // LabCancel
            // 
            this.LabCancel.AutoSize = true;
            this.LabCancel.Location = new System.Drawing.Point(215, 52);
            this.LabCancel.Name = "LabCancel";
            this.LabCancel.Size = new System.Drawing.Size(29, 12);
            this.LabCancel.TabIndex = 3;
            this.LabCancel.TabStop = true;
            this.LabCancel.Text = "取消";
            this.LabCancel.Visible = false;
            this.LabCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labCancel_LinkClicked);
            // 
            // FrmBusyTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(626, 88);
            this.Controls.Add(this.LabCancel);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBusyTip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label labTip;
        public System.Windows.Forms.LinkLabel LabCancel;
    }
}

