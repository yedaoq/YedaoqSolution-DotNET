/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：SourceGridHelper.cs

 * 说明：单例逻辑

 * 作者：叶道全
 
 * 时间：2009年6月9日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 封装单例逻辑的类
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
    /// <typeparam name="T">要使用单例模式的目标类型</typeparam>
    public class Singleton<T> where T:new()
    {
        /// <summary>
        /// 创建对象的代码段的锁，用于防止多个线程重复创建对象；由于锁是静态成员，所有使用此类实现单例的类的对象的创建过程无法同时进行。
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// 目标对象
        /// </summary>
        private T obj;

        /// <summary>
        /// 单例
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