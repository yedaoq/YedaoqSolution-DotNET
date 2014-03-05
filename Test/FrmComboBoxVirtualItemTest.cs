using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class FrmComboBoxVirtualItemTest : Form
    {
        public FrmComboBoxVirtualItemTest()
        {
            InitializeComponent();

            string[] source = new string[] {"1","2" };

            comboBox1.DataSource = source;
           
            //comboBox1.Items.Insert(0,"all");  //Error
        }
    }
}