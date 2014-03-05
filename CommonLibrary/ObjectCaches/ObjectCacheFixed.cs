using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.ObjectCaches
{ 
    /// <summary>
    /// 固定容量的对象缓存池
    /// </summary>
    public abstract class ObjectCacheFiexed<T> : CommonLibrary.ObjectCaches.IObjectCache<T>, IDisposable where T : class, new()
    {
        public ObjectCacheFiexed(int capacity, EnumOperateAtExhaust operatorAtExhaust)
        {        
            this.Capacity = capacity;
            _IdleCount = 0;
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
                if (_OperateAtExhaust.Equals(EnumOperateAtExhaust.Suspend))
                {
                    throw new Exception("Invalid Operate In Single Thread Environment!");
                }

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
            return (IdleCount > 0) ? Cache[--IdleCount] : null;
        }

        /// <summary>
        /// 获取一个缓存对象，当缓存用尽时，将根据OperateAtExhaust处理
        /// </summary>
        /// <returns></returns>
        public T GetInstance()
        {
            T Result = null;

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

            return Result;
        }

        /// <summary>
        /// 释放对象（向缓存归还一个对象）
        /// </summary>
        public void Release(T obj)
        {
            if (IdleCount < Cache.Length)
            {
                Cache[IdleCount++] = obj;
            }
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
