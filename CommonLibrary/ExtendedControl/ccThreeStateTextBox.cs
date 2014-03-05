using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.ExtendedControl
{
    public partial class ccThreeStateTextBox : TextBox
    {
        public ccThreeStateTextBox()
        {
            InitializeComponent();
        }

        

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
    }

    /// <summary>
    /// �ı���״̬
    /// </summary>
    public enum EnumTextBoxState
    {
        /// <summary>
        /// �鿴
        /// </summary>
        View,

        /// <summary>
        /// ��ý���
        /// </summary>
        Focus,

        /// <summary>
        /// �༭
        /// </summary>
        Edit
    }
}
