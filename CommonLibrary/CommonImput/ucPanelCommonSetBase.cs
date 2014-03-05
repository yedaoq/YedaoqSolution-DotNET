using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.CommonImput
{
    /// <summary>
    /// 通用设置面板的基类
    /// </summary>
    public class ucPanelCommonSetBase : UserControl, IPanelCommonSet
    {
        public ucPanelCommonSetBase()
        {
            if (DesignMode) return;

            this.BackColor = System.Drawing.SystemColors.Window;
        }

        #region 数据

        /// <summary>
        /// 是否只读
        /// </summary>
        protected bool _ReadOnly;

        /// <summary>
        /// 是否可以批量选择(即全选,反选)
        /// </summary>
        protected bool _BatchSelectEnabled;

        /// <summary>
        /// 面板ID，用于唯一标识面板
        /// </summary>
        protected object _ID;

        /// <summary>
        /// 包含面板的TabPage，此值提供给FrmCommonSet使用。接口的实现者请勿修改此值。
        /// </summary>
        protected TabPage _OwnerPage;

        /// <summary>
        /// 控件相关标签页上显示的文本
        /// </summary>
        string _Title;

        #endregion

        #region 属性

        /// <summary>
        /// 用于唯一标识面板
        /// </summary>
        public virtual object ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 包含面板的TabPage，此值提供给FrmCommonSet使用。接口的实现者请勿修改此值。
        /// </summary>
        public TabPage OwnerPage
        {
            get { return _OwnerPage; }
            set { _OwnerPage = value; }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return _ReadOnly; }
            set { _ReadOnly = value; }

        }

        /// <summary>
        /// 是否可以批量选择(即全选,反选)
        /// </summary>
        public virtual bool BatchSelectEnabled
        {
            get { return _BatchSelectEnabled; }
            set { _BatchSelectEnabled = value; }
        }

        /// <summary>
        /// 控件相关标签页上显示的文本
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 面板初始化
        /// </summary>
        /// <returns></returns>
        public virtual int Init()
        {
            return 1;
        }

        /// <summary>
        /// 显示前工作
        /// </summary>
        /// <returns></returns>
        public virtual int Show()
        {
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            return 1;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <returns></returns>
        public virtual int SelectAll()
        {
            return 1;
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <returns></returns>
        public virtual int SelectReverse()
        {
            return 1;
        }

        #endregion
    }
}
