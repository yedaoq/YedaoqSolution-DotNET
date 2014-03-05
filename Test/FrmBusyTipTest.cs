using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using System.Threading;
using CommonLibrary.BusyTip;

namespace Test
{
    public partial class FrmBusyTipTest : Form
    {
        public FrmBusyTipTest()
        {
            InitializeComponent();
        }

        BusyTipOperator busytip = new BusyTipOperator(new FrmBusyTip());

        private void button1_Click(object sender, EventArgs e)
        {
for (int i = 0; i < 5; ++i)
{
    busytip.Start();
    busytip.Start(string.Format("Ñ­»·:{0}...",i));
    
    Thread.Sleep(1000);
    busytip.Stop();
    //Thread.Sleep(100);
    busytip.Stop();
}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmFlexLabelTest dialog = new FrmFlexLabelTest();
            dialog.Show();
            //this.Enabled = false;
            //for (int i = 0; i < 100000; ++i)
            //{
            //    Console.WriteLine(i);
            //}
            //this.Enabled = true;
            Thread.Sleep(5000);
            //dialog.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmFlexLabelTest dialog = new FrmFlexLabelTest();

            Thread th = new Thread(new ThreadStart(dialog.Show));

            th.Start();

            Thread.Sleep(5000);

            dialog.Invoke(new ThreadStart(dialog.Close));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmFlexLabelTest dialog = new FrmFlexLabelTest();

            Thread th = new Thread(new ParameterizedThreadStart(this.Run));

            th.Start(dialog);

            Thread.Sleep(5000);

            dialog.Invoke(new ThreadStart(dialog.Close));
        }

        public void Run(object obj)
        {
            Application.Run(obj as Form);
        }
    }
}