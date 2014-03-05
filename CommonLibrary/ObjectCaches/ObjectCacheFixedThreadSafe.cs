using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonLibrary.ObjectCaches
{
    /// <summary>
    /// �̶������Ķ��󻺴��
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

        /// <summary>
        /// �����ͷ�
        /// </summary>
        protected AutoResetEvent EventObjRelease;

        /// <summary>
        /// ͬ������
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
            T Result = null;

            EventSync.WaitOne();

            if (IdleCount > 0) Result = Cache[--IdleCount];

            EventSync.Set();

            return Result;

        }

        /// <summary>
        /// ��ȡһ��������󣬵������þ�ʱ��������OperateAtExhaust����
        /// </summary>
        /// <returns></returns>
        public T GetInstance()
        {
            T Result = null;

            #region ��ȡ����

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
                //�����þ�ʱ�����߳�,�ȴ������߳��ͷŶ���
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
        /// �ͷŶ����򻺴�黹һ������
        /// </summary>
        public void Release(T obj)
        {
            EventSync.WaitOne();

            if (IdleCount < Cache.Length)
            {
                Cache[IdleCount++] = obj;
            }

            //�����ͷŶ���Ĳ�����ʹһ�����ڵȴ�״̬���̱߳���������ڡ����޵ȴ��̣߳��¼�����״̬���ᱻ���á������ˡ���ֹ��״̬���������壬����ڴ��ٴε���һ��Reset
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
