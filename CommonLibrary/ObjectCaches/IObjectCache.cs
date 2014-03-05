using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.ObjectCaches
{
    public interface IObjectCache<T> where T:new()
    {
        /// <summary>
        /// 获取一个缓存的对象
        /// </summary>
        T Instance
        {
            get;
        }

        /// <summary>
        /// 尝试从缓存中获取一个对象，此方法不会阻止，若无可用对象，将立即返回null
        /// </summary>
        /// <returns></returns>
        T TryGetInstance();

        /// <summary>
        /// 释放对象（向缓存归还一个对象）
        /// </summary>
        void Release(T obj);
    }
}
