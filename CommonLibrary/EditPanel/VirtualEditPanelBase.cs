using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.EditPanel
{
    /// <summary>
    /// 虚拟编辑面板基类
    /// </summary>
    public class VirtualEditPanelBase : IEditPanel
    {
        #region Fields

        /// <summary>
        /// 提示保存的时显示的消息
        /// </summary>
        private string _TipSave = "数据有变更，是否保存？";

        /// <summary>
        /// 数据更改标记
        /// </summary>
        private bool _DataChanged = false;

        /// <summary>
        /// 编辑是否已经完成
        /// </summary>
        private bool _EditCompleted = false;

        /// <summary>
        /// 编辑模式
        /// </summary>
        private EnumEditMode _EditMode = EnumEditMode.View;

        #endregion

        #region Properties

        /// <summary>
        /// 提示保存的时显示的消息
        /// </summary>
        protected virtual string TipSave
        {
            get { return "数据有变更，是否保存？"; }
        }

        /// <summary>
        /// 数据是否更改
        /// </summary>
        public virtual bool DataChanged
        {
            get { return _DataChanged; }
            set { _DataChanged = value; }
        }

        /// <summary>
        /// 编辑模式
        /// </summary>
        public virtual EnumEditMode EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="obj">目标数据</param>
        /// <param name="editMode">编辑模式</param>
        /// <returns></returns>
        public virtual int LoadData(object obj, EnumEditMode editMode)
        {
            //检查当前编辑数据是否已保存
            if (this.CheckToSave() != 1) return -1;

            return 1;
        }

        /// <summary>
        /// 检查并保存数据
        /// </summary>
        /// <returns>1:保存成功或用户选择放弃保存;0:用户取消操作;-1:保存失败</returns>
        public int CheckToSave()
        {
            if (EditMode == EnumEditMode.View) return 1;

            //先完成编辑
            CompleteEdit();

            //检查编辑模式和数据变化标记
            if (!DataChanged) return 1;

            //提示保存
            switch (MessageBox.Show(TipSave, "提示", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    return (this.SaveData() == 1) ? 1 : -1; //保存成功时返回1,不成功返回-1;

                case DialogResult.No:
                    CancelEdit();
                    DataChanged = false;
                    return 1;

                case DialogResult.Cancel:
                    return 0;
            }
            return 1;
        }

        /// <summary>
        /// 保存数据,此过程将调用CompleteEdit
        /// </summary>
        /// <returns>1:成功;-1:失败</returns>
        public int Save()
        {
            if (EditMode == EnumEditMode.View) return 1;

            //先完成编辑
            CompleteEdit();

            //检查编辑模式和数据变化标记
            if (!DataChanged) return 1;

            //保存
            if (SaveData() != 1) return -1;
            DataChanged = false;
            return 1;
        }

        /// <summary>
        /// 完成编辑
        /// </summary>
        /// <returns>1:成功;-1:错误</returns>
        public virtual int CompleteEdit()
        {
            return 1;
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        /// <returns></returns>
        public virtual int CancelEdit()
        {
            DataChanged = false;
            return 1;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public virtual int SaveData()
        {
            return 1;
        }

        #endregion
    }
}
