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
    /// 带有伸缩功能的一个标签
    /// </summary>
    public partial class FlexLabel : Control
    {
        #region 数据

        #region 控件绘制相关

        /// <summary>
        /// 是否高亮
        /// </summary>
        private bool _IsHighLight = false;

        /// <summary>
        /// 是否处于伸展状态
        /// </summary>
        private bool _IsSpread = false;

        /// <summary>
        /// 状态变化：此变量用于防止使用者在Flexing事件响应中修改IsSpread属性
        /// </summary>
        private bool StateChanging = false;

        /// <summary>
        /// 高亮区域的边界路径
        /// </summary>
        System.Drawing.Drawing2D.GraphicsPath HighLightRegionPath;

        /// <summary>
        /// 高亮颜色
        /// </summary>
        Color _HighLightColor;

        /// <summary>
        /// 隐藏时的颜色
        /// </summary>
        Color _HideColor;

        /// <summary>
        /// 用于填充高亮区域的刷子
        /// </summary>
        Brush HighLightBrush;

        /// <summary>
        /// 用于清空高亮区域的刷子
        /// </summary>
        Brush HideBrush;

        /// <summary>
        /// 文本画刷
        /// </summary> 
        Brush TextBrush;

        /// <summary>
        /// 描述信息
        /// </summary>
        string _Description;

        private int _LabelStyle = 0;

        #endregion

        #region 关联控件数据与伸缩效果

        /// <summary>
        /// 目标
        /// </summary>
        Control _Target;

        /// <summary>
        /// 收缩时的尺寸
        /// </summary>
        int _SizeShrink = 10;

        /// <summary>
        /// 伸展时的尺寸
        /// </summary>
        int _SizeSpread = 10;

        /// <summary>
        /// 阻尼效果的间隔角度
        /// </summary>
        double RadianTick = 5 * (Math.PI / 180);

        /// <summary>
        /// 指示伸缩过程分为多个部分完成
        /// </summary>
        int _TickCount = 10;

        /// <summary>
        /// 指定两个部分伸缩动作间的时间间隔，以毫秒为单位
        /// </summary>
        int _TickPause = 50;

        #endregion

        CancelEventArgs FlexingArgs;

        #endregion

        #region 属性

        /// <summary>
        /// 是否处于伸展状态
        /// </summary>
        public bool IsSpread
        {
            get { return _IsSpread; }
            set
            {
                if (_IsSpread == value || StateChanging) return;

                StateChanging = true;

                FlexingArgs.Cancel = false;
                if (_Flexing != null) _Flexing(this, FlexingArgs);

                if (FlexingArgs.Cancel)
                {
                    StateChanging = false;
                    return;
                }

                _IsSpread = value;

                FlexTarget();

                this.Refresh();

                StateChanging = false;

                if (_Flexed != null) _Flexed(this, System.EventArgs.Empty);
            }
        }

        #region 控件绘制相关

        /// <summary>
        /// 是否高亮
        /// </summary>
        private bool IsHighLight
        {
            get { return _IsHighLight; }
            set
            {
                if (_IsHighLight == value) return;
                _IsHighLight = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// 高亮颜色
        /// </summary>
        public Color HighLightColor
        {
            get { return _HighLightColor; }
            set
            {
                if (_HighLightColor == value) return;

                _HighLightColor = value;
                HighLightBrush = new SolidBrush(_HighLightColor);
            }
        }

        /// <summary>
        /// 隐藏颜色
        /// </summary>
        public Color HideColor
        {
            get { return _HideColor; }
            set
            {
                if (_HideColor == value) return;

                _HideColor = value;
                HideBrush = new SolidBrush(_HideColor);
            }
        }

        /// <summary>
        /// 显示的提示信息
        /// </summary>
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// 标签样式
        /// </summary>
        public int LabelStyle
        {
            get { return _LabelStyle; }
            set { _LabelStyle = value; }
        }

        #endregion

        #region 关联控件与伸缩效果

        /// <summary>
        /// 关联的控件
        /// </summary>
        public Control Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        /// <summary>
        /// 朝向
        /// </summary>
        public Orientation Orientation
        {
            get { return Orientation.Vertical; }
            set { }
        }

        /// <summary>
        /// 收缩时的尺寸
        /// </summary>
        public int SizeShrink
        {
            get { return _SizeShrink; }
            set
            {
                if (value < 0) return;
                _SizeShrink = value;
            }
        }

        /// <summary>
        /// 伸展时的尺寸
        /// </summary>
        public int SizeSpread
        {
            get { return _SizeSpread; }
            set
            {
                if (value < _SizeShrink) return;
                _SizeSpread = value;
            }
        }

        /// <summary>
        /// 此值用于指定将伸缩过程分为多少次完成。此值越大，则伸缩过程的平滑效果越好，但代价也越大。推荐在8至20之间。
        /// </summary>
        public int TickCount
        {
            get { return _TickCount; }
            set
            {
                if (_TickCount < 1)
                {
                    _TickCount = 1;
                }
                else if (_TickCount > 30)
                {
                    TickCount = 30;
                }
                else
                {
                    _TickCount = value;
                }

                RadianTick = Math.PI / (_TickCount * 2);
            }
        }

        /// <summary>
        /// 指定两个部分伸缩动作间的时间间隔，以毫秒为单位
        /// </summary>
        public int TickPause
        {
            get { return _TickPause; }
            set
            {
                if (_TickPause < 0)
                {
                    _TickPause = 0;
                }
                else
                {
                    _TickPause = value;
                }
            }
        }

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 伸缩事件
        /// </summary>
        private event CancelEventHandler _Flexing;

        /// <summary>
        /// 伸缩事件
        /// </summary>
        public event CancelEventHandler Flexing
        {
            add
            {
                _Flexing += value;
            }
            remove
            {
                _Flexing -= value;
            }
        }

        /// <summary>
        /// 伸缩发生后事件
        /// </summary>
        private event EventHandler _Flexed;

        /// <summary>
        /// 伸缩发生后事件
        /// </summary>
        public event EventHandler Flexed
        {
            add
            {
                _Flexed += value;
            }
            remove
            {
                _Flexed -= value;
            }
        }

        #endregion

        #region 方法

        public FlexLabel()
        {
            InitializeComponent();

            #region 画笔

            //Graphics = this.CreateGraphics();

            HighLightRegionPath = GetLRRoundRegionPath(this.ClientRectangle);

            HighLightColor = Color.FromKnownColor(KnownColor.Control);

            HideColor = Color.FromKnownColor(KnownColor.Window);

            TextBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));

            #endregion

            this.Padding = new Padding(3, 3, 20, 3);
            FlexingArgs = new CancelEventArgs(false);
        }

        /// <summary>
        /// 重置伸缩状态
        /// </summary>
        /// <param name="IsSpread"></param>
        public void ResetFlex(bool IsSpread)
        {
            _IsSpread = IsSpread;
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);

            float YPos = Math.Max(0, (this.Height - 10)) / 2;

            if (HighLightRegionPath == null) return;

            pe.Graphics.FillRectangle(HideBrush, this.ClientRectangle);
            if (IsHighLight) pe.Graphics.FillPath(HighLightBrush, HighLightRegionPath);

            pe.Graphics.DrawString(this.Text, this.Font, this.TextBrush, this.Padding.Left, YPos);

            string FlexTag = (IsSpread) ? "︽" : "︾";

            pe.Graphics.DrawString(FlexTag, this.Font, this.TextBrush, (float)(this.Width - this.Padding.Right), YPos);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Console.WriteLine("SizeChanged");

            HighLightRegionPath = GetLRRoundRegionPath(this.ClientRectangle);
            //Graphics = this.CreateGraphics();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            IsHighLight = true;

            //Graphics.FillPath(HighLightBrush, HighLightRegionPath);
            //Graphics.DrawString(Description, this.Font, this.TextBrush, 8f, 3f);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            IsHighLight = false;

            //Graphics.FillPath(HideBrush, HighLightRegionPath);
            //Graphics.DrawString(Description, this.Font, this.TextBrush, 8f, 3f);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsSpread = !IsSpread;
        }

        /// <summary>
        /// 获取一个左右两端为弧形的区域
        /// </summary>
        /// <param name="Rect">包含区域的矩形</param>
        /// <returns></returns>
        public System.Drawing.Drawing2D.GraphicsPath GetLRRoundRegionPath(Rectangle Rect)
        {
            System.Drawing.Drawing2D.GraphicsPath Path = null;
            Rectangle ArcRect;

            try
            {
                Path = new System.Drawing.Drawing2D.GraphicsPath();

                switch (LabelStyle)
                {
                    case 0:
                        #region 两端圆滑

                        ArcRect = new Rectangle(Rect.Location, new Size(Rect.Height, Rect.Height));

                        //左边
                        Path.AddArc(ArcRect, 90, 180);

                        //右边
                        ArcRect.X = Rect.X + Rect.Width - Rect.Height;
                        Path.AddArc(ArcRect, 270, 180);

                        break;
                        #endregion

                    case 1:
                        #region 四角圆滑

                        ArcRect = new Rectangle(Rect.Location, new Size(8, 8));

                        //左上角
                        Path.AddArc(ArcRect, 180, 90);


                        ArcRect.X = Rect.Width - 9;
                        //右上角
                        Path.AddArc(ArcRect, 270, 90);

                        ArcRect.Y = Rect.Height - 9;
                        //右下角
                        Path.AddArc(ArcRect, 0, 90);

                        ArcRect.X = 0;
                        //左下角
                        Path.AddArc(ArcRect, 90, 90);

                        break;

                        #endregion

                    case 2:
                        #region 四角平滑

                        Path.AddLine(0, 2, 2, 0);
                        Path.AddLine(Rect.Width - 2, 0, Rect.Width, 2);
                        Path.AddLine(Rect.Width, Rect.Height - 2, Rect.Width - 2, Rect.Height);
                        Path.AddLine(2, Rect.Height, 0, Rect.Height - 2);

                        break;
                        #endregion

                    default:
                        break;
                }

                //闭合
                Path.CloseFigure();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Path;
        }

        /// <summary>
        /// 伸缩目标控件
        /// </summary>
        public void FlexTarget()
        {
            if (Target == null || SizeShrink == SizeSpread) return;

            int Distance = SizeSpread - SizeShrink;
            double Radian = 0;
            double RadianTick = this.IsSpread ? this.RadianTick : -this.RadianTick;
            int SizeInit = this.IsSpread ? this.SizeShrink : this.SizeSpread;

            for (int i = 0; i < _TickCount; i++)
            {
                Radian += RadianTick;

                Target.Height = SizeInit + (int)(Distance * Math.Sin(Radian));

                Target.Update();

                System.Threading.Thread.Sleep(_TickPause);
            }
        }

        #endregion
    }
}
