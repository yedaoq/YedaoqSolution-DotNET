using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.ObjectCaches
{
    public interface IObjectCache<T> where T:new()
    {
        /// <summary>
        /// ��ȡһ������Ķ���
        /// </summary>
        T Instance
        {
            get;
        }

        /// <summary>
        /// ���Դӻ����л�ȡһ�����󣬴˷���������ֹ�����޿��ö��󣬽���������null
        /// </summary>
        /// <returns></returns>
        T TryGetInstance();

        /// <summary>
        /// �ͷŶ����򻺴�黹һ������
        /// </summary>
        void Release(T obj);
    }
}
