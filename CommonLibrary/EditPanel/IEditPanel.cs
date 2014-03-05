using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.EditPanel
{
    /// <summary>
    /// 编辑面板的接口
    /// </summary>
    public interface IEditPanel
    {
        /// <summary>
        /// 数据是否有更改
        /// </summary>
        bool DataChanged { get;set;}

        /// <summary>
        /// 编辑模式
        /// </summary>
        EnumEditMode EditMode
        {
            get;
            set;
        }

        /// <summary>
        /// 加载数据接口
        /// </summary>
        /// <param name="obj">目标数据</param>
        /// <param name="editMode">编辑模式</param>
        int LoadData(object obj, EnumEditMode editMode);

        /// <summary>
        /// 检查更改，必要时提示保存
        /// </summary>
        int CheckToSave();

        /// <summary>
        /// 完成未完的编辑操作，进行数据校验，使数据处理确定状态；
        /// 若可保存，则返回1，否则其它其它值
        /// </summary>
        int CompleteEdit();

        /// <summary>
        /// 取消所做的编辑
        /// </summary>
        int CancelEdit();

        /// <summary>
        /// 保存数据接口
        /// </summary>
        int Save();
    }
}
