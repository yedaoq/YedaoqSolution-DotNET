/*

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：BusImpedanceItem.cs

 * 说明：带一个参数的简单事件参数类

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
    /// 可以传递一个数据的简单事件参数类
    /// </summary>
    public class SimpleEvnetArgs : EventArgs
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        public object Obj;

        public SimpleEvnetArgs(object Obj)
        {
            this.Obj = Obj;
        }
    }

    /// <summary>
    ///  带有一个数据的简单事件的响应原型
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimpleEventHandler(object sender, SimpleEvnetArgs e);
}
