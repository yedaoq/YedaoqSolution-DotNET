namespace Test
{
    partial class FrmFlexLabelTest
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
            this.flexLabel3 = new CommonLibrary.FlexLabel();
            this.flexLabel2 = new CommonLibrary.FlexLabel();
            this.flexLabel1 = new CommonLibrary.FlexLabel();
            this.SuspendLayout();
            // 
            // flexLabel3
            // 
            this.flexLabel3.HideColor = System.Drawing.SystemColors.Window;
            this.flexLabel3.HighLightColor = System.Drawing.SystemColors.Control;
            this.flexLabel3.IsSpread = false;
            this.flexLabel3.LabelStyle = 2;
            this.flexLabel3.Location = new System.Drawing.Point(12, 93);
            this.flexLabel3.Name = "flexLabel3";
            this.flexLabel3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.flexLabel3.Size = new System.Drawing.Size(337, 23);
            this.flexLabel3.SizeShrink = 10;
            this.flexLabel3.SizeSpread = 10;
            this.flexLabel3.TabIndex = 2;
            this.flexLabel3.Target = null;
            this.flexLabel3.Text = "flexLabel3";
            this.flexLabel3.TickCount = 10;
            this.flexLabel3.TickPause = 50;
            // 
            // flexLabel2
            // 
            this.flexLabel2.HideColor = System.Drawing.SystemColors.Window;
            this.flexLabel2.HighLightColor = System.Drawing.SystemColors.Control;
            this.flexLabel2.IsSpread = false;
            this.flexLabel2.LabelStyle = 1;
            this.flexLabel2.Location = new System.Drawing.Point(12, 52);
            this.flexLabel2.Name = "flexLabel2";
            this.flexLabel2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.flexLabel2.Size = new System.Drawing.Size(337, 23);
            this.flexLabel2.SizeShrink = 10;
            this.flexLabel2.SizeSpread = 10;
            this.flexLabel2.TabIndex = 1;
            this.flexLabel2.Target = null;
            this.flexLabel2.Text = "flexLabel2";
            this.flexLabel2.TickCount = 10;
            this.flexLabel2.TickPause = 50;
            // 
            // flexLabel1
            // 
            this.flexLabel1.HideColor = System.Drawing.SystemColors.Window;
            this.flexLabel1.HighLightColor = System.Drawing.SystemColors.Control;
            this.flexLabel1.IsSpread = false;
            this.flexLabel1.LabelStyle = 0;
            this.flexLabel1.Location = new System.Drawing.Point(12, 12);
            this.flexLabel1.Name = "flexLabel1";
            this.flexLabel1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.flexLabel1.Size = new System.Drawing.Size(337, 23);
            this.flexLabel1.SizeShrink = 10;
            this.flexLabel1.SizeSpread = 10;
            this.flexLabel1.TabIndex = 0;
            this.flexLabel1.Target = null;
            this.flexLabel1.Text = "flexLabel1";
            this.flexLabel1.TickCount = 10;
            this.flexLabel1.TickPause = 50;
            // 
            // FrmFlexLabelTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(460, 346);
            this.Controls.Add(this.flexLabel3);
            this.Controls.Add(this.flexLabel2);
            this.Controls.Add(this.flexLabel1);
            this.Name = "FrmFlexLabelTest";
            this.Text = "FrmFlexLabelTest";
            this.ResumeLayout(false);

        }

        #endregion

        private CommonLibrary.FlexLabel flexLabel1;
        private CommonLibrary.FlexLabel flexLabel2;
        private CommonLibrary.FlexLabel flexLabel3;
    }
}