using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.ExtendedControl
{
    /// <summary>
    /// 具有〈编辑，显示〉两种呈现样式的TextBox
    /// </summary>
    public partial class ccTSTextBox : TextBox
    {
        #region 类型

        /// <summary>
        /// 标识文本框控件的状态的枚举
        /// </summary>
        public enum TextBoxStateEnum { Edit, Show }

        public class StateChangeEventArgs:EventArgs
        {
            public TextBoxStateEnum TargetState;
            public bool Cancel = false;

            public StateChangeEventArgs(TextBoxStateEnum State)
            {
                this.TargetState = State;
            }
        }

        public delegate void BeforeStateChangedHander(object sender, StateChangeEventArgs e);

        #endregion 

        #region 数据

        /// <summary>
        /// 在状态切换之前触发的事件
        /// </summary>
        private event BeforeStateChangedHander _BeforeStateChanged;

        /// <summary>
        /// 控件当前的状态
        /// </summary>
        private TextBoxStateEnum _State = TextBoxStateEnum.Show;

        /// <summary>
        /// 此控件用于获取文本的合适的宽度
        /// </summary>
        protected Label WidthCreaterLabel = new Label();

        /// <summary>
        /// 状态切换的事件参数
        /// </summary>
        protected StateChangeEventArgs args = new StateChangeEventArgs(TextBoxStateEnum.Show);

        /// <summary>
        /// 编辑状态时的背景色
        /// </summary>
        private Color _EditBckColor = Color.White;

        /// <summary>
        /// 展示状态下的背景色
        /// </summary>
        private Color _ShowBckColor = (Color)new System.Drawing.ColorConverter().ConvertFrom("Control");

        /// <summary>
        /// 编辑时的边框样式
        /// </summary>
        private BorderStyle _BorderStyleForEdit = BorderStyle.Fixed3D;

        /// <summary>
        /// 显示时的边框样式
        /// </summary>
        private BorderStyle _BorderStyleForShow = BorderStyle.None;

        /// <summary>
        /// 自动根据文本调整控件宽度
        /// </summary>
        private bool _AutoWidthForText = false;

        /// <summary>
        /// 按下Enter键时是否自动切换为显示状态
        /// </summary>
        private bool _ChangeToShowWhenKeyEnter = true;

        /// <summary>
        /// 控件失去焦点时是否自动切换为显示状态
        /// </summary>
        private bool _ChangeToShowWhenLoseFucus = true;

        /// <summary>
        /// 控件获得焦点时是否自动切换为编辑状态
        /// </summary>
        private bool _ChangeToEditWhenGetFocus = true;

        /// <summary>
        /// 单击控件时是否自动切换为编辑状态
        /// </summary>
        public bool _ChangeToEditWhenClick;

        /// <summary>
        /// 此标记用于记录是否正在进行状态切换
        /// </summary>
        private bool StateChanging = false;

        #endregion

        #region 属性

        /// <summary>
        /// 文本框状态
        /// </summary>
        public TextBoxStateEnum State
        {
            get { return _State; }
            set 
            {
                if (_State == value|| StateChanging) return;

                StateChanging = true;

                do
                {
                    StateChangeEventArgs Args = new StateChangeEventArgs(value);

                    if (_BeforeStateChanged != null) _BeforeStateChanged(this, Args);

                    if (Args.Cancel) break; ;

                    _State = value;

                    this.LostFocus -= this.OnLoseFocus;

                    if (_State == TextBoxStateEnum.Edit)
                    {
                        this.ReadOnly = false;
                        this.BackColor = this.EditBckColor;
                        this.BorderStyle = BorderStyleForEdit;
                    }
                    else
                    {
                        this.ReadOnly = true;
                        this.BackColor = this.ShowBckColor;
                        this.BorderStyle = BorderStyleForShow;
                        this.Select(0, 0);
                    }

                    this.LostFocus += this.OnLoseFocus;
                } while (false);

                StateChanging = false;
            }
        }

        /// <summary>
        /// 状态改变事件
        /// </summary>
        public event BeforeStateChangedHander BeforeStateChanged
        {
            add
            {
                _BeforeStateChanged += value;
            }
            remove
            {
                _BeforeStateChanged -= value;
            }
        }

        /// <summary>
        /// 编辑状态下的背景色
        /// </summary>
        public Color EditBckColor
        {
            get { return _EditBckColor; }
            set 
            { 
                if(_EditBckColor == value) return;
                _EditBckColor = value;
                if (this.State == TextBoxStateEnum.Edit) this.BackColor = value;
            }
        }

        /// <summary>
        /// 展示状态下的背景色
        /// </summary>
        public Color ShowBckColor
        {
            get { return _ShowBckColor; }
            set
            {
                if (_ShowBckColor == value) return;
                _ShowBckColor = value;
                if (this.State == TextBoxStateEnum.Show) this.BackColor = value;
            }
        }

        /// <summary>
        /// 编辑时的边框样式
        /// </summary>
        public BorderStyle BorderStyleForEdit
        {
            get { return _BorderStyleForEdit; }
            set { _BorderStyleForEdit = value; }
        }

        /// <summary>
        /// 显示时的边框样式
        /// </summary>
        public BorderStyle BorderStyleForShow
        {
            get { return _BorderStyleForShow; }
            set { _BorderStyleForShow = value; }
        }

        /// <summary>
        /// 自动根据文本变化调整控件宽度
        /// </summary>
        public bool AutoWidthForText
        {
            get { return _AutoWidthForText; }
            set
            {
                _AutoWidthForText = value;
            }
        }

        /// <summary>
        /// 按下Enter键时是否自动切换为显示状态
        /// </summary>
        public bool ChangeToShowWhenKeyEnter
        {
            get { return _ChangeToShowWhenKeyEnter; }
            set
            {
                _ChangeToShowWhenKeyEnter = value;
            }
        }

        /// <summary>
        /// 单击控件时是否自动切换为编辑状态
        /// </summary>
        public bool ChangeToEditWhenClick
        {
            get { return _ChangeToEditWhenClick; }
            set { _ChangeToEditWhenClick = value; }
        }

        /// <summary>
        /// 控件失去焦点时是否自动切换为显示状态
        /// </summary>
        public bool ChangeToShowWhenLoseFucus
        {
            get { return _ChangeToShowWhenLoseFucus; }
            set
            {
                _ChangeToShowWhenLoseFucus = value;
            }
        }

        /// <summary>
        /// 当控件获得焦点时是否切换是编辑状态
        /// </summary>
        public bool ChangeToEditWhenGetFocus
        {
            get { return _ChangeToEditWhenGetFocus; }
            set { _ChangeToEditWhenGetFocus = value; }
        }

        /// <summary>
        /// 与其内容相匹配的宽度
        /// </summary>
        public int SuitWidth
        {
            get
            {
                this.WidthCreaterLabel.Text = this.Text;
                return System.Math.Max(this.WidthCreaterLabel.PreferredWidth,this.MinimumSize.Width);
            }
        }

        #endregion

        #region 方法

        public ccTSTextBox()
        {
            InitializeComponent();

            this.BackColor = ShowBckColor;
            this.BorderStyle = BorderStyle.None;
            this.ReadOnly = true;
            this.Select(0, 0);

            this.TextChanged += OnTextChanged;
            this.FontChanged += OnFontChanged;
            this.LostFocus += this.OnLoseFocus;
            this.Click += OnClick;
            this.KeyDown+= OnKeyDown;
        }        
            
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (AutoWidthForText) this.Width = SuitWidth;
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            WidthCreaterLabel.Font = this.Font;
        }

        private void OnLoseFocus(object sender, EventArgs e)
        {
            if(ChangeToShowWhenLoseFucus) this.State = TextBoxStateEnum.Show;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_ChangeToShowWhenKeyEnter) this.State = TextBoxStateEnum.Show;
            }
        }

        private void OnGetFocus(object sender, EventArgs e)
        {
            if (_ChangeToEditWhenGetFocus) this.State = TextBoxStateEnum.Edit;
        }

        void OnClick(object sender, EventArgs e)
        {
            if (_ChangeToEditWhenClick) this.State = TextBoxStateEnum.Edit;
        }        

        #endregion
    }
}
