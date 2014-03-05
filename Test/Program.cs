using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonLibrary;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new FrmFlexLabelTest());
            //AlgorithmConbinationTest.Test();

            //DebugAssist.ClearFile("C:\\test.txt");

            //Application.Run(new FrmCommonSetTest());
            //Application.Run(new FrmFlowLayoutPanelTest());
            //Application.Run(new FrmComboBoxVirtualItemTest());
            Application.Run(new FrmBusyTipTest());
        }
    }
}