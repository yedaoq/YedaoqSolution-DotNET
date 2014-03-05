using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class FrmFlowLayoutPanelTest : Form
    {
        public FrmFlowLayoutPanelTest()
        {
            InitializeComponent();

            Label label = new Label();
            label.Text = "My Imput : ";

            TextBox tBox = new TextBox();

            flpMain.Controls.Add(label);
            flpMain.Controls.Add(new TextBox());
        }
    }
}