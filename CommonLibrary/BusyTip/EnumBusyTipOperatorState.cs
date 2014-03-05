using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.BusyTip
{
    /// <summary>
    /// 提示对象状态的枚举
    /// </summary>
    public enum EnumBusyTipOperatorState
    {
        /// <summary>
        /// 正在显示提示窗体
        /// </summary>
        Show,

        /// <summary>
        /// 空闲，未显示提示窗体
        /// </summary>
        Free,

        /// <summary>
        /// 已释放，无法再显示提示窗体
        /// </summary>
        Released
    }
}
