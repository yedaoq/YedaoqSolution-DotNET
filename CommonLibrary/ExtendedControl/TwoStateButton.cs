using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CommonLibrary.ExtendedControl
{  
    /// <summary>
    /// 具有两种状态的按钮，按钮状态的切换在ClickAtStateA、ClickAtStateB事件之后，Click事件之前发生。可在ClickA、ClickB事件的处理程序对是否切换状态进行干预。
    /// </summary>
    public partial class TwoStateButton : Button
    {
        /// <summary>
        /// 按钮状态
        /// </summary>
        public enum EnumButtonState
        {
            /// <summary>
            /// 状态A
            /// </summary>
            StateA = 0, 

            /// <summary>
            /// 状态B
            /// </summary>
            StateB = 1
        }

        #region 数据

        /// <summary>
        /// 按钮当前状态
        /// </summary>
        private EnumButtonState _State;

        /// <summary>
        /// 事件参数
        /// </summary>
        private CancelEventArgs EventArgs = new CancelEventArgs();

        /// <summary>
        /// 处于状态A时显示的文本
        /// </summary>
        private string _TextAtStateA = "编辑";

        /// <summary>
        /// 处于状态B时显示的文本
        /// </summary>
        private string _TextAtStateB = "保存";

        #endregion

        #region 属性

        /// <summary>
        /// 按钮状态
        /// </summary>
        public EnumButtonState State
        {
            get { return _State; }
        }

        /// <summary>
        /// 处于状态A时显示的文本
        /// </summary>
        [Browsable(true), Category("自定义"), Description("按钮处于第一种状态时的显示文本")]
        public string TextAtStateA
        {
            get { return _TextAtStateA; }
            set
            {
                if (_TextAtStateA == value) return;
                _TextAtStateA = value;
                if (this.State == EnumButtonState.StateA) this.Text = value;
            }
        }

        /// <summary>
        /// 处于状态B时显示的文本
        /// </summary>
        [Browsable(true), Category("自定义"), Description("按钮处于第二种状态时的显示文本")]
        public string TextAtStateB
        {
            get { return _TextAtStateB; }
            set
            {
                if (_TextAtStateB == value) return;
                _TextAtStateB = value;
                if (State == EnumButtonState.StateB) base.Text = value;
            }
        }

        /// <summary>
        /// 按钮文本
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //base.Text = value;
            }
        }

        #endregion

        #region 事件

        private event EventHandler<CancelEventArgs> _ClickAtStateA;

        private event EventHandler<CancelEventArgs> _ClickAtStateB;

        /// <summary>
        /// 按钮在状态A时的单击事件
        /// </summary>
        public event EventHandler<CancelEventArgs> ClickAtStateA
        {
            add { this._ClickAtStateA += value; }
            remove { this._ClickAtStateA -= value; }
        }

        /// <summary>
        /// 按钮在状态B时的单击事件
        /// </summary>
        public event EventHandler<CancelEventArgs> ClickAtStateB
        {
            add { this._ClickAtStateB += value; }
            remove { this._ClickAtStateB -= value; }
        }

        #endregion

        #region 方法

        public TwoStateButton()
        {
            InitializeComponent();

            base.Text = TextAtStateA;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            EventArgs.Cancel = false;

            if (State == EnumButtonState.StateA)
            {
                if (_ClickAtStateA != null) _ClickAtStateA(this, EventArgs);
            }
            else
            {
                if (_ClickAtStateB != null) _ClickAtStateB(this, EventArgs);
            }

            if (!EventArgs.Cancel) _State = (EnumButtonState)(1 - (int)State);
            UpdateText();
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        public void ResetState()
        {
            ResetState(EnumButtonState.StateA);
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        /// <param name="State">目标状态</param>
        public void ResetState(EnumButtonState State)
        {
            this._State = State;
            UpdateText();
        }

        private void UpdateText()
        {
            base.Text = (this.State == EnumButtonState.StateA) ? TextAtStateA : TextAtStateB;
        }

        #endregion
    }
}
