/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��FrmBusyTip.cs

 * ˵�������ڱ�ʾϵͳ����æ״̬����ʾ����

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
using System.Threading;

namespace CommonLibrary.BusyTip
{
    /// <summary>
    /// ���ڱ�ʾϵͳ����æ״̬����ʾ����
    /// </summary>
    public partial class FrmBusyTip : Form
    {
        #region ����

        /// <summary>
        /// ��ʾ��ͼƬ
        /// </summary>
        public String _PictureAddress;

        /// <summary>
        /// �Ƿ�����ȡ��
        /// </summary>
        public bool _CancelEnabled = true;

        /// <summary>
        /// �������Ƿ����
        /// </summary>
        public bool _ParentEnabled = false;

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                labTip.Text = value;
                LabCancel.Left = labTip.Location.X + labTip.Width + 6;
            }
        }

        /// <summary>
        /// ͼƬ��ַ
        /// </summary>
        public string PictureAddress
        {
            get { return _PictureAddress; }
            set
            {
                if (value == null && value == _PictureAddress) return;

                _PictureAddress = value;
                try
                {
                    pictureBox1.Image = Image.FromFile(value);
                }
                catch
                {
                    Console.WriteLine("����ͼƬʧ��!");
                }
            }
        }

        /// <summary>
        /// �Ƿ�����ȡ��
        /// </summary>
        public bool CancelEnabled
        {
            get { return _CancelEnabled; }
            set
            {
                _CancelEnabled = value;
                labTip.Visible = false;
            }
        }

        /// <summary>
        /// �������Ƿ����
        /// </summary>
        public bool ParentEnabled
        {
            get { return _ParentEnabled; }
            set { _ParentEnabled = value; }
        }

        #endregion

        #region ���캯��

        public FrmBusyTip()
        {
            Console.WriteLine("Begin Load");

            InitializeComponent();
        }

        public FrmBusyTip(String PictureAddress):this()
        {            
            this.PictureAddress = PictureAddress;
            this.pictureBox1.Image = Image.FromFile(PictureAddress);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��ʾ
        /// </summary>
        /// <param name="ParentEnabled">�������Ƿ���ã�δʵ�֣�</param>
        public void BeginShow(bool ParentEnabled)
        {
            this.ShowDialog();
        }

        /// <summary>
        /// ��ֹ��ʾ
        /// </summary>
        public void EndShow()
        {
            if (InvokeRequired)
                this.BeginInvoke(new DelegateInvoke(this.Close));
            else
                this.Close();
        }

        #endregion

        #region �¼���Ӧ

        private void labCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            //this.EndShow();
        }

        #endregion

        private void FrmBusyTip_Load(object sender, EventArgs e)
        {
           Console.WriteLine("Load Completed");
            //this.Visible = true;
            //this.Activate();
            //Console.WriteLine("The Owner if BusTip is : {0}",(Owner == null)?"null":Owner.Text);
            //Console.WriteLine("The Visibled of BusTip at Load is : {0}", Visible.ToString());
        }

        private void FrmBusyTip_VisibleChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("The Visibled of BusTip is Changed");
        }

        private void FrmBusyTip_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Close Completed");

            //Console.WriteLine("The Visibled of BusTip at Closed is : {0}", Visible.ToString());
        }

        private void FrmBusyTip_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Begin Close");

            //Console.WriteLine("The Visibled of BusTip at Closing is : {0}", Visible.ToString());
        }

        private void FrmBusyTip_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("Shown");
        }

    }
}