using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace CommonLibrary.BusyTip
{
    /// <summary>
    /// 调用委托
    /// </summary>
    public delegate void DelegateInvoke();

    /// <summary>
    /// 不考虑线程安全的设置属性委托
    /// </summary>
    /// <param name="Value">属性值</param>
    public delegate void DelegateSetProperty(string PropertyName, object Value);

    /// <summary>
    /// 使用提示窗体的接口
    /// </summary>
    public class BusyTipOperator:IDisposable
    {
        #region Constructor & Destructor

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusyTipOperator(Form tipDialog)
        {
            FrmTip = tipDialog;
            FrmTip.ShowInTaskbar = false;
            FrmTip.Load += new EventHandler(FrmTip_FormLoad);

            ShowTipEvent = new AutoResetEvent(false);
            FrmOpCompletedEvent = new AutoResetEvent(false);

            DelegateSetProperty = new DelegateSetProperty(this.SetProperty);

            TipThread = new Thread(new ThreadStart(this.TipThreadRun));
            TipThread.Start();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~BusyTipOperator()
        {
            if (!Disposed) Dispose();
        }

        #endregion

        #region Fields

        /// <summary>
        /// 提示窗体
        /// </summary>
        private Form FrmTip;

        /// <summary>
        /// 用于显示提示窗体的线程
        /// </summary>
        private Thread TipThread;

        /// <summary>
        /// 触发显示提示窗体操作的事件
        /// </summary>
        private AutoResetEvent ShowTipEvent;

        /// <summary>
        /// 此事件在窗体操作完成时发出通知
        /// </summary>
        private AutoResetEvent FrmOpCompletedEvent;

        /// <summary>
        /// 指向SetProperty的委托
        /// </summary>
        private DelegateSetProperty DelegateSetProperty;

        /// <summary>
        /// 当前提示的状态
        /// </summary>
        private EnumBusyTipOperatorState _State = EnumBusyTipOperatorState.Free;

        /// <summary>
        /// 是否终止显示线程
        /// </summary>
        private bool StopTipThread = false;

        /// <summary>
        /// 非托管资源释放标志
        /// </summary>
        private bool Disposed = false;

        /// <summary>
        /// 锁，用于同步窗体的打开／关闭过程
        /// </summary>
        private object Lock = new object();

        /// <summary>
        /// 统计调用Start的嵌套层数(即当前需要调用Stop的次数)
        /// </summary>
        private int ShowCount = 0;

        #endregion

        #region Properties

        /// <summary>
        /// 当前提示窗体的状态
        /// </summary>
        public EnumBusyTipOperatorState State
        {
            get { return _State; }
        }

        /// <summary>
        /// 提示内容
        /// </summary>
        public string Text
        {
            get
            {
                return FrmTip.Text;
            }
            set
            {
                SetProperty("Text", value);
            }
        }

        /// <summary>
        /// 是否允许取消,此设置项暂时无效,默认为否
        /// </summary>
        public bool CancelEnabled
        {
            get { return false; }
            set
            {
                
            }
        }

        /// <summary>
        /// 父窗体是否可用,此设置项暂时无效,默认为否
        /// </summary>
        public bool ParentEnabled
        {
            get { return false ; }
            set
            {
               
            }
        }

        #endregion

        #region Operator Interface

        /// <summary>
        /// 开始显示提示窗体
        /// </summary>
        public int Start()
        {
            Monitor.Enter(Lock);

            ++ShowCount;

            if (this.State == EnumBusyTipOperatorState.Free)
            {
                _State = EnumBusyTipOperatorState.Show;

                //通过ShowTipEvent事件向TipThread发送信息，使其显示提示窗体
                ShowTipEvent.Set();

                //等待提示窗体显示完成的信号  &  也可以通过以下方式实现：使当前线程挂起，然后在FrmTip_Load中恢复
                FrmOpCompletedEvent.WaitOne();
            }

            Monitor.Exit(Lock);

            return 1;
        }

        /// <summary>
        /// 开始显示提示窗体并在其中显示指定的提示文本
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public int Start(string Text)
        {
            this.Text = Text;
            return Start();
        }

        /// <summary>
        /// 终止显示提示窗体
        /// </summary>
        public int Stop()
        {
            Monitor.Enter(Lock);

            do
            {
                if (_State == EnumBusyTipOperatorState.Released) break;

                if (ShowCount <= 0) break;
                if (--ShowCount > 0) break;

                _State = EnumBusyTipOperatorState.Free;

                FrmTip.Invoke(new DelegateInvoke(FrmTip.Close));

                FrmOpCompletedEvent.WaitOne();

            } while (false);

            Monitor.Exit(Lock);

            return 1;
        }

        /// <summary>
        /// 设置提示窗体的提示文本
        /// </summary>
        /// <param name="Text"></param>
        public void SetProperty(string PropertyName,object Value)
        {
            if (FrmTip.InvokeRequired)
            {
                FrmTip.Invoke(DelegateSetProperty, PropertyName, Value);
            }
            else
            {
                FrmTip.GetType().InvokeMember(PropertyName, System.Reflection.BindingFlags.SetProperty, null, FrmTip, new object[] { Value });
            }
            }

        #endregion

        #region Innerl Methods

        /// <summary>
        /// 用来显示提示框的守护线程
        /// </summary>
        private void TipThreadRun()
        {
            ShowTipEvent.WaitOne();
            while (!StopTipThread)
            {
                FrmTip.ShowDialog();
                FrmOpCompletedEvent.Set();
                ShowTipEvent.WaitOne();
            }
        }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        public void Dispose()
        {
            Disposed = true;

            if (this.State == EnumBusyTipOperatorState.Show) Stop();
            _State = EnumBusyTipOperatorState.Released;
            StopTipThread = true;
            ShowTipEvent.Set();
            TipThread.Join();           
        }

        /// <summary>
        /// 提示窗体加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmTip_FormLoad(object sender, EventArgs e)
        {
            FrmOpCompletedEvent.Set();
        }

        #endregion
    }
}
