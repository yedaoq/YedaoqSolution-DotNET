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
    /// 滚动显示的状态栏标签类
    /// </summary>
    public class ToolStripStatusRollingLabel : ToolStripStatusLabel
    {
        public ToolStripStatusRollingLabel()
        {
            StringRect.X = this.Padding.Left;
            StringRect.Y = this.Padding.Top;
            StringRect.Height = Size.Height - Padding.Top - Padding.Bottom;

            Brush = new SolidBrush(this.ForeColor);
            ControlDataRollOffsetGenerater = ControlDataRollOffsetGeneraterFactory.Instance.GetControlDataRollOffsetGenerater(this.RollSytle,0,0,3,0);

            this.UpdateTimer = new Timer();
            this.UpdateTimer.Interval = 500;
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += delegate { this.OnTick(); };
        }

        #region 辅助

        /// <summary>
        /// 定时器
        /// </summary>
        private Timer UpdateTimer;

        /// <summary>
        /// 笔刷
        /// </summary>
        SolidBrush Brush;

        /// <summary>
        /// 用于显示内容的矩形
        /// </summary>
        Rectangle StringRect = Rectangle.Empty;

        /// <summary>
        /// 滚动的偏移量生成器
        /// </summary>
        IControlDataRollOffsetGenerater ControlDataRollOffsetGenerater;

        /// <summary>
        /// 图形
        /// </summary>
        Graphics Graphics;

        #endregion

        #region 数据

        /// <summary>
        /// 显示内容
        /// </summary>
        private string _Text;

        /// <summary>
        /// 滚动距离
        /// </summary>
        private int _RollDistance = 0;

        /// <summary>
        /// 控件内容的滚动样式
        /// </summary>
        private EnumTextRollStyle _RollStyle = EnumTextRollStyle.TurnLeft;

        /// <summary>
        /// 文本宽度
        /// </summary>
        private int _TextWidth;

        #endregion

        #region 属性

        /// <summary>
        /// 文本内容
        /// </summary>
        public override string Text
        {
            get
            {
                if (DesignMode)
                {
                    return _Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (_Text == value) return;
                _Text = value;

                if (!DesignMode)
                {
                    if (Graphics != null) TextWidth = (int)(Graphics.MeasureString(value, this.Font).Width);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 文本宽度
        /// </summary>
        public int TextWidth
        {
            get { return _TextWidth; }
            set
            {
                if (_TextWidth == value) return;
                _TextWidth = value;
                if (ControlDataRollOffsetGenerater != null) ControlDataRollOffsetGenerater.DataWidth = value;
            }
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                Brush = new SolidBrush(base.ForeColor);
                base.ForeColor = value;

                //this.Invalidate();
            }
        }

        /// <summary>
        /// 控件大小
        /// </summary>
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (this.Size == value) return;
                base.Size = value;
                StringRect.Height = Size.Height - Padding.Top - Padding.Bottom;
                if (ControlDataRollOffsetGenerater != null) ControlDataRollOffsetGenerater.WindowWidth = this.Size.Width - Padding.Left - Padding.Right;
            }
        }

        /// <summary>
        /// 内部间距
        /// </summary>
        public override Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                if (value == Padding) return;
                base.Padding = value;
                StringRect.Y = Padding.Top;
                StringRect.Height = Size.Height - Padding.Top - Padding.Bottom;
                if (ControlDataRollOffsetGenerater != null) ControlDataRollOffsetGenerater.WindowWidth = this.Size.Width - Padding.Left - Padding.Right;
            }
        }

        /// <summary>
        /// 滚动速度
        /// </summary>
        public int RedrawTimeSpan
        {
            get { return UpdateTimer.Interval; }
            set
            {
                UpdateTimer.Interval = value;
            }
        }

        /// <summary>
        /// 滚动速度：每次刷新文本显示位置的间距
        /// </summary>
        public int RoolSpeed
        {
            get
            {
                return ControlDataRollOffsetGenerater.Speed;
            }
            set
            {
                ControlDataRollOffsetGenerater.Speed = value;
            }
        }

        /// <summary>
        /// 当前显示位置与初始位置的距离
        /// </summary>
        public int RollDistance
        {
            get { return _RollDistance; }
            set { _RollDistance = value; }
        }

        /// <summary>
        /// 控件内容的滚动样式
        /// </summary>
        public EnumTextRollStyle RollSytle
        {
            get { return _RollStyle; }
            set
            {
                if (_RollStyle == value) return;
                _RollStyle = value;
                ControlDataRollOffsetGenerater = ControlDataRollOffsetGeneraterFactory.Instance.GetControlDataRollOffsetGenerater(value, StringRect.Width, TextWidth, this.RedrawTimeSpan, this.RollDistance);
            }
        }

        #endregion

        private void OnTick()
        {
            //if (ControlDataRollOffsetGenerater == null) return;

            RollDistance = ControlDataRollOffsetGenerater.GetNextOffset();
            StringRect.X = this.Padding.Left + RollDistance;

            StringRect.Width = ControlDataRollOffsetGenerater.WindowWidth - RollDistance;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Graphics == null)
            {
                Graphics = e.Graphics;
                TextWidth = (int)(Graphics.MeasureString(_Text, this.Font).Width);
            }

            e.Graphics.DrawString(_Text, this.Font, Brush, StringRect);

            base.OnPaint(e);
        }
    }

    /// <summary>
    /// 控件文本内容滚动的样式
    /// </summary>
    public enum EnumTextRollStyle
    {
        /// <summary>
        /// 向左滚动
        /// </summary>
        TurnLeft,

        /// <summary>
        /// 向左滚动
        /// </summary>
        TurnRight,

        /// <summary>
        /// 来回滚动
        /// </summary>
        PingPong
    }

    public class ControlDataRollOffsetGeneraterFactory
    {
        #region 单例

        static Singleton<ControlDataRollOffsetGeneraterFactory> Singleton;

        public static ControlDataRollOffsetGeneraterFactory Instance
        {
            get
            {
                if (Singleton == null) Singleton = new Singleton<ControlDataRollOffsetGeneraterFactory>();
                return Singleton.Instance;
            }
        }

        #endregion

        /// <summary>
        /// 获取一个数据滚动的偏移量生成器
        /// </summary>
        /// <param name="RollStyle"></param>
        /// <param name="WindowWidth"></param>
        /// <param name="DataWidth"></param>
        /// <param name="Speed"></param>
        /// <param name="InitOffset"></param>
        /// <returns></returns>
        public IControlDataRollOffsetGenerater GetControlDataRollOffsetGenerater(EnumTextRollStyle RollStyle, int WindowWidth, int DataWidth, int Speed, int InitOffset)
        {
            switch (RollStyle)
            {
                case EnumTextRollStyle.TurnLeft:
                    return new ControlDataRollToLeftOffsetGenerater(WindowWidth, DataWidth, Speed, InitOffset);
            }

            return null;
        }
    }

    /// <summary>
    /// 控件内容滚动偏移量生成器的接口
    /// </summary>
    public interface IControlDataRollOffsetGenerater
    {
        /// <summary>
        /// 显示区域宽度
        /// </summary>
        int WindowWidth { get;set;}

        /// <summary>
        /// 数据宽度
        /// </summary>
        int DataWidth { get;set;}

        /// <summary>
        /// 偏移量
        /// </summary>
        int Offset { get;set;}

        /// <summary>
        /// 移动速度
        /// </summary>
        int Speed { get;set;}

        /// <summary>
        /// 获取下一个偏移量
        /// </summary>
        /// <returns>新偏移量</returns>
        int GetNextOffset();
    }

    /// <summary>
    /// 控件内容滚动偏移量生成器基类
    /// </summary>
    public class ControlDataRollOffsetGeneraterBase : IControlDataRollOffsetGenerater
    {
        public ControlDataRollOffsetGeneraterBase()
        {

        }

        public ControlDataRollOffsetGeneraterBase(int WindowWidth, int DataWidth, int Speed)
        {
            this._WindowWidth = WindowWidth;
            this._DataWidth = DataWidth;
            this._Speed = Speed;
        }

        public ControlDataRollOffsetGeneraterBase(int WindowWidth, int DataWidth, int Speed, int InitOffset)
        {
            this._WindowWidth = WindowWidth;
            this._DataWidth = DataWidth;
            this._Speed = Speed;
            this._Offset = InitOffset;
        }

        #region 数据

        /// <summary>
        /// 显示区域宽度
        /// </summary>
        int _WindowWidth;

        /// <summary>
        /// 数据宽度
        /// </summary>
        int _DataWidth;

        /// <summary>
        /// 偏移量
        /// </summary>
        int _Offset;

        /// <summary>
        /// 移动速度
        /// </summary>
        int _Speed;

        #endregion

        #region 属性

        /// <summary>
        /// 显示区域宽度
        /// </summary>
        public virtual int WindowWidth
        {
            get
            {
                return _WindowWidth;
            }
            set
            {
                _WindowWidth = value;
            }
        }

        /// <summary>
        /// 数据宽度
        /// </summary>
        public virtual int DataWidth
        {
            get
            {
                return _DataWidth;
            }
            set
            {
                _DataWidth = value;
            }
        }

        /// <summary>
        /// 偏移量
        /// </summary>
        public virtual int Offset
        {
            get
            {
                return _Offset;
            }
            set
            {
                _Offset = value;
            }
        }

        /// <summary>
        /// 移动速度
        /// </summary>
        public virtual int Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
            }
        }

        #endregion

        /// <summary>
        /// 获取下一个偏移量
        /// </summary>
        /// <returns>新偏移量</returns>
        public virtual int GetNextOffset()
        {
            return 0;
        }
    }

    /// <summary>
    /// 控件内容向左滚动的偏移量生成器
    /// </summary>
    public class ControlDataRollToLeftOffsetGenerater : ControlDataRollOffsetGeneraterBase
    {
        public ControlDataRollToLeftOffsetGenerater(int WindowWidth, int DataWidth, int Speed)
            : base(WindowWidth, DataWidth, Speed)
        {
            //RollWidth = WindowWidth + DataWidth;
        }

        public ControlDataRollToLeftOffsetGenerater(int WindowWidth, int DataWidth, int Speed, int InitOffset)
            : base(WindowWidth, DataWidth, Speed)
        {
            //RollWidth = WindowWidth + DataWidth;
        }

        #region 数据

        //private int _RollWidth;

        #endregion

        #region 属性

        public override int WindowWidth
        {
            get
            {
                return base.WindowWidth;
            }
            set
            {
                base.WindowWidth = value;
                //RollWidth = DataWidth + WindowWidth;
            }
        }

        public override int DataWidth
        {
            get
            {
                return base.DataWidth;
            }
            set
            {
                base.DataWidth = value;
                //RollWidth = DataWidth + WindowWidth;
            }
        }

        //public int RollWidth
        //{
        //    get
        //    {
        //        return _RollWidth;
        //    }
        //    set
        //    {
        //        if (value < 0) _RollWidth = 0;
        //    }
        //}

        #endregion

        /// <summary>
        /// 获取一个新的偏移量
        /// </summary>
        /// <returns></returns>
        public override int GetNextOffset()
        {
            if (Offset < -DataWidth)
            {
                Offset = WindowWidth;
            }
            else
            {
                Offset -= Speed;
            }

            return Offset;
        }
    }
}
