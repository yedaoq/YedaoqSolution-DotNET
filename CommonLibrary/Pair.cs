/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：Pair.cs

 * 说明：用于存储一对关联数据的类

 * 作者：叶道全
 
 * 时间：2009年10月10日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 存储一对相关联的数据的类
    /// </summary>
    /// <typeparam name="FirstT">第一个值的类型</typeparam>
    /// <typeparam name="SecondT">第二个值的类型</typeparam>
    public class Pair<FirstT, SecondT>
    {
        /// <summary>
        /// 第一个值
        /// </summary>
        private FirstT _First;

        /// <summary>
        /// 第二个值
        /// </summary>
        private SecondT _Second;

        /// <summary>
        /// 第一个值
        /// </summary>
        public FirstT First
        {
            get { return _First; }
            set { _First = value; }
        }

        /// <summary>
        /// 第二个值
        /// </summary>
        public SecondT Second
        {
            get { return _Second; }
            set { _Second = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        public Pair(FirstT First, SecondT Second)
        {
            this.First = First;
            this.Second = Second;
        }

        /// <summary>
        /// 重载的相等比较逻辑
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;
            if (obj is Pair<FirstT, SecondT>)
            {
                Pair<FirstT, SecondT> Other = obj as Pair<FirstT, SecondT>;
                if (this.First.Equals(Other.First) && this.Second.Equals(Other.Second)) return true;
            }
            else if (obj is FirstT)
            {
                if (this.First.Equals((FirstT)obj)) return true;
            }
            else if (obj is SecondT)
            {
                if (this.Second.Equals((SecondT)obj)) return true;
            }
            return this.First.Equals(obj);
        }

        /// <summary>
        /// 获取哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return First.GetHashCode() + Second.GetHashCode();
        }

        public static bool operator ==(Pair<FirstT, SecondT> A, object B)
        {
            return (object.ReferenceEquals(A, null) && object.ReferenceEquals(B, null)) || A.Equals(B);
        }

        public static bool operator !=(Pair<FirstT, SecondT> A, object B)
        {
            return !(A == B);
        }
    }
}
