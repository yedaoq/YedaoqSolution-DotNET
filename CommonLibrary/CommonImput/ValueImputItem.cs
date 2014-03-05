using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.CommonImput
{
    public class ValueImputItem<T>
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value
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
        /// 输入项名称
        /// </summary>
        public string Name
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
        /// 文本宽度
        /// </summary>
        public int TextWidth
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
        /// 包含可选值的数据源；若指定此值，输入框将显示为ComboBox样式
        /// </summary>
        public IEnumerable<T> Source
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
