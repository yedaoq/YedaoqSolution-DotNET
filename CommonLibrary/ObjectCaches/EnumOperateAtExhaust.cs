using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 指示用户请求对象且缓存用尽时应如何操作
    /// </summary>
    public enum EnumOperateAtExhaust
    {
        /// <summary>
        /// 挂起线程直到有可用对象归还缓存池
        /// </summary>
        Suspend = 0,

        /// <summary>
        /// 创建一个新对象给用户
        /// </summary>
        CreateNew = 1,

        /// <summary>
        /// 向用户返回一个空对象
        /// </summary>
        ReturnNull
    }
}
