using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.EditPanel
{
    /// <summary>
    /// 编辑状态的枚举
    /// </summary>
    public enum EnumEditMode
    {
        /// <summary>
        /// 自动确定
        /// </summary>
        Auto,

        /// <summary>
        /// 查看
        /// </summary>
        View,

        /// <summary>
        /// 修改
        /// </summary>
        Modify,

        /// <summary>
        /// 新增
        /// </summary>
        New
    }
}
