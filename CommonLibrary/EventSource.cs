using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonLibrary
{
    /// <summary>
    /// �¼�Դ��,�����൱����չ���¼�
    /// </summary>
    /// <typeparam name="T">�¼���������</typeparam>
    public class EventSource<T> where T : EventArgs
    {
        #region Fields

        /// <summary>
        /// �¼�����
        /// </summary>
        private event EventHandler<T> _Event;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool _Enabled = true;

        /// <summary>
        /// �첽�����¼����̳߳ص��ýӿ�ί��
        /// </summary>
        private WaitCallback _AsyncRaiseCallBack;

        #endregion

        #region Properties

        /// <summary>
        /// �¼�����
        /// </summary>
        public event EventHandler<T> Event
        {
            add
            {
                this._Event += value;
            }
            remove
            {
                this._Event -= value;
            }
        }

        /// <summary>
        /// �Ƿ����:������Ϊtrue,��Raise���������ᴥ���¼�
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        /// <summary>
        /// �첽�����¼����̳߳ص��ýӿ�ί��
        /// </summary>
        private WaitCallback AsyncRaiseCallBack
        {
            get
            {
                if (_AsyncRaiseCallBack == null)
                {
                    _AsyncRaiseCallBack = new WaitCallback(RaiseEventCallBack);
                }
                return _AsyncRaiseCallBack;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender">���ͷ�</param>
        /// <param name="args">����</param>
        public void Raise(object sender, T args)
        {
            if (Enabled && _Event != null)
            {
                _Event(sender, args);
            }
        }

        /// <summary>
        /// �첽�����¼�
        /// </summary>
        /// <param name="sender">���ͷ�</param>
        /// <param name="args">����</param>
        /// <param name="Async">�Ƿ��첽����</param>
        public void Raise(object sender, T args, bool Async)
        {
            if (!Enabled || _Event == null) return;

            if (Async)
            {
                Pair<object, T> context = new Pair<object, T>(sender, args);
                ThreadPool.QueueUserWorkItem(AsyncRaiseCallBack, context);
            }
            else
            {
                _Event(sender, args);
            }
        }

        /// <summary>
        /// �첽�����¼����̳߳���ں���
        /// </summary>
        private void RaiseEventCallBack(object ThreadContext)
        {
            Pair<object, T> context = ThreadContext as Pair<object, T>;
            _Event(context.First, context.Second);
        }

        /// <summary>
        /// �����Ӧ
        /// </summary>
        /// <param name="Response">��Ӧ</param>
        /// <returns>�¼�Դ</returns>
        public static EventSource<T> operator +(EventSource<T> Source, EventHandler<T> Response)
        {
            Source.Event += Response;
            return Source;
        }

        /// <summary>
        /// �����Ӧ
        /// </summary>
        /// <param name="Response">��Ӧ</param>
        /// <returns>�¼�Դ</returns>
        public static EventSource<T> operator -(EventSource<T> Source, EventHandler<T> Response)
        {
            Source.Event -= Response;
            return Source;
        }

        /// <summary>
        /// �����Ӧ
        /// </summary>
        /// <param name="Response"></param>
        public void Add(EventHandler<T> Response)
        {
            this._Event += Response;
        }

        public void Remove(EventHandler<T> Response)
        {
            this._Event -= Response;
        }

        #endregion
    }
}
