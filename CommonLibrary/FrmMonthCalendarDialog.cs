/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��MultiPointEquivalenceCaseManager.cs

 * ˵��������ѡ���壨������

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��10��10��
 
 * �޸ļ�¼ �� 
 
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
    /// ����ѡ���壨������
    /// </summary>
    public partial class FrmMonthCalendarDialog : Form
    {
        public FrmMonthCalendarDialog()
        {
            InitializeComponent();
        }

        private DateTime _SelectedDate;

        /// <summary>
        /// ��ǰѡ�������
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

        #region ����

        /// <summary>
        /// ��յ����߼��Ķ���
        /// </summary>
        private static Singleton<FrmMonthCalendarDialog> Singleton = new Singleton<FrmMonthCalendarDialog>();

        /// <summary>
        /// ʵ��
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