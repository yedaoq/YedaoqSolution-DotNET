/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：FrmBusyTip.cs

 * 说明：用于表示系统处理繁忙状态的提示窗体

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
using System.Threading;

namespace CommonLibrary.BusyTip
{
    /// <summary>
    /// 用于表示系统处理繁忙状态的提示窗体
    /// </summary>
    public partial class FrmBusyTip : Form
    {
        #region 数据

        /// <summary>
        /// 显示的图片
        /// </summary>
        public String _PictureAddress;

        /// <summary>
        /// 是否允许取消
        /// </summary>
        public bool _CancelEnabled = true;

        /// <summary>
        /// 父窗体是否可用
        /// </summary>
        public bool _ParentEnabled = false;

        #endregion

        #region 属性

        /// <summary>
        /// 提示内容
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
        /// 图片地址
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
                    Console.WriteLine("加载图片失败!");
                }
            }
        }

        /// <summary>
        /// 是否允许取消
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
        /// 父窗体是否可用
        /// </summary>
        public bool ParentEnabled
        {
            get { return _ParentEnabled; }
            set { _ParentEnabled = value; }
        }

        #endregion

        #region 构造函数

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

        #region 方法

        /// <summary>
        /// 开始显示
        /// </summary>
        /// <param name="ParentEnabled">父窗体是否可用（未实现）</param>
        public void BeginShow(bool ParentEnabled)
        {
            this.ShowDialog();
        }

        /// <summary>
        /// 终止显示
        /// </summary>
        public void EndShow()
        {
            if (InvokeRequired)
                this.BeginInvoke(new DelegateInvoke(this.Close));
            else
                this.Close();
        }

        #endregion

        #region 事件响应

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