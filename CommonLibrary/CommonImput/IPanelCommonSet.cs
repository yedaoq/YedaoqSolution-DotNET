using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.CommonImput
{
    /// <summary>
    /// 通用设置面板的接口
    /// </summary>
    public interface IPanelCommonSet
    {
        /// <summary>
        /// 面板的关键字，用于唯一标识面板
        /// </summary>
        object ID { get;set;}

        /// <summary>
        /// 包含面板的TabPage，此值提供给FrmCommonSet使用。接口的实现者请勿修改此值。
        /// </summary>
        TabPage OwnerPage { get;set;}

        /// <summary>
        /// 面板初始化标记
        /// </summary>
        //bool InitFlag { get;set;}

        /// <summary>
        /// 是否只读
        /// </summary>
        bool ReadOnly { get;set;}

        /// <summary>
        /// 是否可以批量选择(即全选,反选)
        /// </summary>
        bool BatchSelectEnabled { get;set;}

        /// <summary>
        /// 控件相关标签页上显示的文本
        /// </summary>
        string Title { get;set;}

        /// <summary>
        /// 面板初始化
        /// </summary>
        /// <returns></returns>
        //int Init();

        /// <summary>
        /// 显示前工作
        /// </summary>
        /// <returns></returns>
        int Show();

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int Save();

        /// <summary>
        /// 全选
        /// </summary>
        /// <returns></returns>
        int SelectAll();

        /// <summary>
        /// 反选
        /// </summary>
        /// <returns></returns>
        int SelectReverse();
    }
}
