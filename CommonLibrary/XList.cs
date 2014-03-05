/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：SourceGridHelper.cs

 * 说明：扩展的List<T>类

 * 作者：叶道全
 
 * 时间：2009年6月9日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// <![CDATA[扩展的List<T>类]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XList<T>:List<T>
    {
        /// <summary>
        /// 查找指定对象
        /// </summary>
        /// <param name="Obj">要查找的对象</param>
        /// <returns>查找到的元素</returns>
        public object FindObject(object Obj)
        {
            if (Obj == null) return null;

            Enumerator Enumerator = GetEnumerator();

            while (Enumerator.MoveNext())
            {
                if (Enumerator.Current != null && Enumerator.Current.Equals(Obj)) return Enumerator.Current;
            }

            return null;
        }
    }
}
