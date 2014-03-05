using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonLibrary
{
    /// <summary>
    /// 事件源类,此类相当于扩展的事件
    /// </summary>
    /// <typeparam name="T">事件参数类型</typeparam>
    public class EventSource<T> where T : EventArgs
    {
        #region Fields

        /// <summary>
        /// 事件对象
        /// </summary>
        private event EventHandler<T> _Event;

        /// <summary>
        /// 是否可用
        /// </summary>
        private bool _Enabled = true;

        /// <summary>
        /// 异步触发事件的线程池调用接口委托
        /// </summary>
        private WaitCallback _AsyncRaiseCallBack;

        #endregion

        #region Properties

        /// <summary>
        /// 事件对象
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
        /// 是否可用:若设置为true,则Raise函数将不会触发事件
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        /// <summary>
        /// 异步触发事件的线程池调用接口委托
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
        /// 触发事件
        /// </summary>
        /// <param name="sender">发送方</param>
        /// <param name="args">参数</param>
        public void Raise(object sender, T args)
        {
            if (Enabled && _Event != null)
            {
                _Event(sender, args);
            }
        }

        /// <summary>
        /// 异步触发事件
        /// </summary>
        /// <param name="sender">发送方</param>
        /// <param name="args">参数</param>
        /// <param name="Async">是否异步触发</param>
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
        /// 异步触发事件的线程池入口函数
        /// </summary>
        private void RaiseEventCallBack(object ThreadContext)
        {
            Pair<object, T> context = ThreadContext as Pair<object, T>;
            _Event(context.First, context.Second);
        }

        /// <summary>
        /// 添加响应
        /// </summary>
        /// <param name="Response">响应</param>
        /// <returns>事件源</returns>
        public static EventSource<T> operator +(EventSource<T> Source, EventHandler<T> Response)
        {
            Source.Event += Response;
            return Source;
        }

        /// <summary>
        /// 添加响应
        /// </summary>
        /// <param name="Response">响应</param>
        /// <returns>事件源</returns>
        public static EventSource<T> operator -(EventSource<T> Source, EventHandler<T> Response)
        {
            Source.Event -= Response;
            return Source;
        }

        /// <summary>
        /// 添加响应
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
