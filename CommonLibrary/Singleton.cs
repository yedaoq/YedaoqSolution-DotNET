/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��SourceGridHelper.cs

 * ˵���������߼�

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��6��9��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// ��װ�����߼�����
    /// <example>
    /// public class MyClass
    ///{
    ///    Singleton<![CDATA[<MyClass>]]> Singleton = new Singleton<![CDATA[<MyClass>]]>();
    ///    public MyClass Instance
    ///    {
    ///        get { return Singleton.Instance; }
    ///    }
    ///}</example>
    /// </summary>
    /// <typeparam name="T">Ҫʹ�õ���ģʽ��Ŀ������</typeparam>
    public class Singleton<T> where T:new()
    {
        /// <summary>
        /// ��������Ĵ���ε��������ڷ�ֹ����߳��ظ����������������Ǿ�̬��Ա������ʹ�ô���ʵ�ֵ�������Ķ���Ĵ��������޷�ͬʱ���С�
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Ŀ�����
        /// </summary>
        private T obj;

        /// <summary>
        /// ����
        /// </summary>
        public T Instance
        {
            get
            {
                //if (obj == null)
                //{
                //    lock (Lock)
                //    {
                //        if (obj == null)
                //        {
                //            obj = new T();
                //        }
                //    }
                //}
                return obj;
            }
        }

        public Singleton()
        {
            lock (Lock)
            {
                if (obj == null)
                {
                    obj = new T();
                }
            }
        }
    }
}