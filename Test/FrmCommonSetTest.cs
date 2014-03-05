using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.CommonImput;

namespace Test
{
    public partial class FrmCommonSetTest : Form
    {
        public FrmCommonSetTest()
        {
            InitializeComponent();
        }

        FrmCommonSet Dialog = new FrmCommonSet();

        private void button1_Click(object sender, EventArgs e)
        {
            Dialog.ShowDialog("设置一", new Pair<object, Type>(typeof(ucControlTest1).Name, typeof(ucControlTest1)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dialog.ShowDialog("设置二", new Pair<object, Type>(typeof(ucControlTest2).Name, typeof(ucControlTest2)));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dialog.ShowDialog("设置三", new Pair<object, Type>(typeof(ucControlTest2).Name, typeof(ucControlTest2)), new Pair<object, Type>(typeof(ucControlTest1).Name, typeof(ucControlTest1)));
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dialog.ShowDialog("设置二", new string[] { typeof(ucControlTest2).Name });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dialog.ShowDialog("设置二", new ucControlTest1());
        }
    }
}