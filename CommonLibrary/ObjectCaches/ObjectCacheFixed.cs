using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.ObjectCaches
{ 
    /// <summary>
    /// �̶������Ķ��󻺴��
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
        /// ����Ķ�������
        /// </summary>
        protected T[] Cache;

        /// <summary>
        /// ���õĶ�����
        /// </summary>
        int _IdleCount;

        /// <summary>
        /// ���û���������һ����þ�ʱ�Ĳ���
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
        /// ���û���������һ����þ�ʱ�Ĳ���
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
        /// ��������
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
        /// ���õĶ�����
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
        /// ���Ի�ȡһ��������󣬴˺������������޿��û���ʱ�����ؿ�ֵ
        /// </summary>
        /// <returns></returns>
        public T TryGetInstance()
        {
            return (IdleCount > 0) ? Cache[--IdleCount] : null;
        }

        /// <summary>
        /// ��ȡһ��������󣬵������þ�ʱ��������OperateAtExhaust����
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
        /// �ͷŶ����򻺴�黹һ������
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
