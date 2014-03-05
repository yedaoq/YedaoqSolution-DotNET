using CommonLibrary;

namespace Test
{
    partial class Form1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flexLabel1 = new CommonLibrary.FlexLabel();
            this.toolStripStatusLabel1 = new CommonLibrary.ToolStripStatusRollingLabel();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(405, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.flexLabel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 525);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(32, 72);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(312, 125);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listBox1
            // 
            this.listBox1.ColumnWidth = 30;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "sdf\t",
            "dgag",
            "eg",
            "erg",
            "e",
            "h",
            "egh",
            "qeg"});
            this.listBox1.Location = new System.Drawing.Point(32, 203);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(312, 112);
            this.listBox1.TabIndex = 3;
            // 
            // flexLabel1
            // 
            this.flexLabel1.HideColor = System.Drawing.SystemColors.Control;
            this.flexLabel1.HighLightColor = System.Drawing.SystemColors.ActiveCaption;
            this.flexLabel1.IsSpread = false;
            this.flexLabel1.LabelStyle = 0;
            this.flexLabel1.Location = new System.Drawing.Point(15, 20);
            this.flexLabel1.Name = "flexLabel1";
            this.flexLabel1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.flexLabel1.Padding = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.flexLabel1.Size = new System.Drawing.Size(320, 30);
            this.flexLabel1.SizeShrink = 32;
            this.flexLabel1.SizeSpread = 525;
            this.flexLabel1.TabIndex = 1;
            this.flexLabel1.Target = this.groupBox1;
            this.flexLabel1.Text = "flexLabel1";
            this.flexLabel1.TickCount = 30;
            this.flexLabel1.TickPause = 40;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel1.RollDistance = -186;
            this.toolStripStatusLabel1.RollSpeed = 3;
            this.toolStripStatusLabel1.RollSytle = CommonLibrary.EnumTextRollStyle.TurnLeft;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(300, 17);
            this.toolStripStatusLabel1.Text = "在你淡定的脸后面,我隐约看见一颗蠢蠢欲动的心~~~~~";
            this.toolStripStatusLabel1.TextWidth = 300;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 573);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private ToolStripStatusRollingLabel toolStripStatusLabel1;
        private FlexLabel flexLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

