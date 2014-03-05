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
    /// 文本框状态
    /// </summary>
    public enum EnumTextBoxState
    {
        /// <summary>
        /// 查看
        /// </summary>
        View,

        /// <summary>
        /// 获得焦点
        /// </summary>
        Focus,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit
    }
}
