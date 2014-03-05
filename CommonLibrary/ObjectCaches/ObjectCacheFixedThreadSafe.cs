using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonLibrary.ObjectCaches
{
    /// <summary>
    /// 固定容量的对象缓存池
    /// </summary>
    public abstract class ObjectCacheFiexedThreadSafe<T> : CommonLibrary.ObjectCaches.IObjectCache<T>, IDisposable where T : class, new()
    {
        public ObjectCacheFiexedThreadSafe(int capacity, EnumOperateAtExhaust operatorAtExhaust)
        {            
            EventObjRelease = new AutoResetEvent(false);
            EventSync = new AutoResetEvent(true);

            this.Capacity = capacity;
            _IdleCount = capacity;
            this.OperateAtExhaust = operatorAtExhaust;
        }

        #region Fields

        /// <summary>
        /// 缓存的对象数组
        /// </summary>
        protected T[] Cache;

        /// <summary>
        /// 闲置的对象数
        /// </summary>
        int _IdleCount;

        /// <summary>
        /// 当用户请求对象且缓存用尽时的操作
        /// </summary>
        protected EnumOperateAtExhaust _OperateAtExhaust;

        /// <summary>
        /// 对象释放
        /// </summary>
        protected AutoResetEvent EventObjRelease;

        /// <summary>
        /// 同步对象
        /// </summary>
        protected AutoResetEvent EventSync;

        #endregion

        #region Properties

        public T Instance
        {
            get
            {
                return GetInstance();
            }
        }

        /// <summary>
        /// 当用户请求对象且缓存用尽时的操作
        /// </summary>
        public EnumOperateAtExhaust OperateAtExhaust
        {
            get
            {
                return _OperateAtExhaust;
            }
            set
            {
                _OperateAtExhaust = value;
            }
        }

        /// <summary>
        /// 缓存容量
        /// </summary>
        public int Capacity
        {
            get
            {
                return Cache.Length;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Invalid Argument!");
                }

                if (Cache.Length == value) return;

                lock (Cache)
                {
                    T[] CacheTemp = new T[value];
                    IdleCount = Math.Min(IdleCount, CacheTemp.Length);

                    if (!object.ReferenceEquals(null, Cache))
                    {
                        for (int i = 0; i < IdleCount; ++i)
                        {
                            CacheTemp[i] = Cache[i];
                        }
                    }

                    Cache = CacheTemp;
                }
            }
        }

        /// <summary>
        /// 闲置的对象数
        /// </summary>
        public int IdleCount
        {
            get { return _IdleCount; }
            protected set
            {
                _IdleCount = value;
                System.Diagnostics.Debug.Assert(value < 0, "Error");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 尝试获取一个缓存对象，此函数不阻塞，无可用缓存时将返回空值
        /// </summary>
        /// <returns></returns>
        public T TryGetInstance()
        {
            T Result = null;

            EventSync.WaitOne();

            if (IdleCount > 0) Result = Cache[--IdleCount];

            EventSync.Set();

            return Result;

        }

        /// <summary>
        /// 获取一个缓存对象，当缓存用尽时，将根据OperateAtExhaust处理
        /// </summary>
        /// <returns></returns>
        public T GetInstance()
        {
            T Result = null;

            #region 获取对象

            EventSync.WaitOne();

            if (IdleCount < 0)
            {
                if (OperateAtExhaust.Equals(EnumOperateAtExhaust.CreateNew))
                {
                    Result = new T();
                }
            }
            else
            {
                if (object.ReferenceEquals(null, Cache[--IdleCount]))
                {
                    Cache[IdleCount] = new T();                    
                }
                Result = Cache[IdleCount];
            }

            #endregion

            if (object.ReferenceEquals(null, Result) && OperateAtExhaust.Equals(EnumOperateAtExhaust.Suspend))
            {
                //缓存用尽时阻塞线程,等待其它线程释放对象
                WaitHandle.SignalAndWait(EventSync, EventObjRelease);
                return GetInstance();
            }
            else
            {
                EventSync.Set();
                return Result;
            }

            return null;
        }

        /// <summary>
        /// 释放对象（向缓存归还一个对象）
        /// </summary>
        public void Release(T obj)
        {
            EventSync.WaitOne();

            if (IdleCount < Cache.Length)
            {
                Cache[IdleCount++] = obj;
            }

            //触发释放对象的操作，使一个处于等待状态的线程被激活。但由于“若无等待线程，事件对象状态不会被重置”，而此“终止”状态保留无意义，因此在此再次调用一次Reset
            EventObjRelease.Set();
            EventObjRelease.Reset();

            EventSync.Set();
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
