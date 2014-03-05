using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary.CommonImput
{
    public class EnumChooseItem
    {
        /// <summary>
        /// 是否允许多选，此参数将决定枚举项呈现为复选框还是单选框
        /// </summary>
        public bool MultiSelectable
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable Source
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 用户所选择的项
        /// </summary>
        public List<Object> SelectedItems
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 输入组的呈现样式
        /// </summary>
        public GroupLayoutSetting LayoutSetting
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
