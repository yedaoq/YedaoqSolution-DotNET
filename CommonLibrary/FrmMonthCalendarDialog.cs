/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：MultiPointEquivalenceCaseManager.cs

 * 说明：日期选择窗体（日历）

 * 作者：叶道全
 
 * 时间：2009年10月10日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// 日期选择窗体（日历）
    /// </summary>
    public partial class FrmMonthCalendarDialog : Form
    {
        public FrmMonthCalendarDialog()
        {
            InitializeComponent();
        }

        private DateTime _SelectedDate;

        /// <summary>
        /// 当前选择的日期
        /// </summary>
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                MonthCalendar.SetDate(value);
            }
        }

        #region 单例

        /// <summary>
        /// 封闭单例逻辑的对象
        /// </summary>
        private static Singleton<FrmMonthCalendarDialog> Singleton = new Singleton<FrmMonthCalendarDialog>();

        /// <summary>
        /// 实例
        /// </summary>
        public static FrmMonthCalendarDialog Instance
        {
            get { return Singleton.Instance; }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _SelectedDate = MonthCalendar.SelectionStart;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}