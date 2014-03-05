using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace CommonLibrary.BusyTip
{
    /// <summary>
    /// ����ί��
    /// </summary>
    public delegate void DelegateInvoke();

    /// <summary>
    /// �������̰߳�ȫ����������ί��
    /// </summary>
    /// <param name="Value">����ֵ</param>
    public delegate void DelegateSetProperty(string PropertyName, object Value);

    /// <summary>
    /// ʹ����ʾ����Ľӿ�
    /// </summary>
    public class BusyTipOperator:IDisposable
    {
        #region Constructor & Destructor

        /// <summary>
        /// ���캯��
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
        /// ��������
        /// </summary>
        ~BusyTipOperator()
        {
            if (!Disposed) Dispose();
        }

        #endregion

        #region Fields

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private Form FrmTip;

        /// <summary>
        /// ������ʾ��ʾ������߳�
        /// </summary>
        private Thread TipThread;

        /// <summary>
        /// ������ʾ��ʾ����������¼�
        /// </summary>
        private AutoResetEvent ShowTipEvent;

        /// <summary>
        /// ���¼��ڴ���������ʱ����֪ͨ
        /// </summary>
        private AutoResetEvent FrmOpCompletedEvent;

        /// <summary>
        /// ָ��SetProperty��ί��
        /// </summary>
        private DelegateSetProperty DelegateSetProperty;

        /// <summary>
        /// ��ǰ��ʾ��״̬
        /// </summary>
        private EnumBusyTipOperatorState _State = EnumBusyTipOperatorState.Free;

        /// <summary>
        /// �Ƿ���ֹ��ʾ�߳�
        /// </summary>
        private bool StopTipThread = false;

        /// <summary>
        /// ���й���Դ�ͷű�־
        /// </summary>
        private bool Disposed = false;

        /// <summary>
        /// ��������ͬ������Ĵ򿪣��رչ���
        /// </summary>
        private object Lock = new object();

        /// <summary>
        /// ͳ�Ƶ���Start��Ƕ�ײ���(����ǰ��Ҫ����Stop�Ĵ���)
        /// </summary>
        private int ShowCount = 0;

        #endregion

        #region Properties

        /// <summary>
        /// ��ǰ��ʾ�����״̬
        /// </summary>
        public EnumBusyTipOperatorState State
        {
            get { return _State; }
        }

        /// <summary>
        /// ��ʾ����
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
        /// �Ƿ�����ȡ��,����������ʱ��Ч,Ĭ��Ϊ��
        /// </summary>
        public bool CancelEnabled
        {
            get { return false; }
            set
            {
                
            }
        }

        /// <summary>
        /// �������Ƿ����,����������ʱ��Ч,Ĭ��Ϊ��
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
        /// ��ʼ��ʾ��ʾ����
        /// </summary>
        public int Start()
        {
            Monitor.Enter(Lock);

            ++ShowCount;

            if (this.State == EnumBusyTipOperatorState.Free)
            {
                _State = EnumBusyTipOperatorState.Show;

                //ͨ��ShowTipEvent�¼���TipThread������Ϣ��ʹ����ʾ��ʾ����
                ShowTipEvent.Set();

                //�ȴ���ʾ������ʾ��ɵ��ź�  &  Ҳ����ͨ�����·�ʽʵ�֣�ʹ��ǰ�̹߳���Ȼ����FrmTip_Load�лָ�
                FrmOpCompletedEvent.WaitOne();
            }

            Monitor.Exit(Lock);

            return 1;
        }

        /// <summary>
        /// ��ʼ��ʾ��ʾ���岢��������ʾָ������ʾ�ı�
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public int Start(string Text)
        {
            this.Text = Text;
            return Start();
        }

        /// <summary>
        /// ��ֹ��ʾ��ʾ����
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
        /// ������ʾ�������ʾ�ı�
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
        /// ������ʾ��ʾ����ػ��߳�
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
        /// �ͷŷ��й���Դ
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
        /// ��ʾ����������
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
