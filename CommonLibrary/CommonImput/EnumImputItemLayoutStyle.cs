using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.CommonImput
{
    public enum EnumImputItemLayoutStyle
    {
        /// <summary>
        /// 从左至右布局，项宽度自动确定
        /// </summary>
        ItemWidthAuto,

        /// <summary>
        /// 从左至右布局，项宽度固定
        /// </summary>
        ItemWidthFixed,

        /// <summary>
        /// 每行一项
        /// </summary>
        OneRowPerItem,
    }
}
