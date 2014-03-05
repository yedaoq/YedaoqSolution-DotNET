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
    /// �����������ܵ�һ����ǩ
    /// </summary>
    public partial class FlexLabel : Control
    {
        #region ����

        #region �ؼ��������

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool _IsHighLight = false;

        /// <summary>
        /// �Ƿ�����չ״̬
        /// </summary>
        private bool _IsSpread = false;

        /// <summary>
        /// ״̬�仯���˱������ڷ�ֹʹ������Flexing�¼���Ӧ���޸�IsSpread����
        /// </summary>
        private bool StateChanging = false;

        /// <summary>
        /// ��������ı߽�·��
        /// </summary>
        System.Drawing.Drawing2D.GraphicsPath HighLightRegionPath;

        /// <summary>
        /// ������ɫ
        /// </summary>
        Color _HighLightColor;

        /// <summary>
        /// ����ʱ����ɫ
        /// </summary>
        Color _HideColor;

        /// <summary>
        /// ���������������ˢ��
        /// </summary>
        Brush HighLightBrush;

        /// <summary>
        /// ������ո��������ˢ��
        /// </summary>
        Brush HideBrush;

        /// <summary>
        /// �ı���ˢ
        /// </summary> 
        Brush TextBrush;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string _Description;

        private int _LabelStyle = 0;

        #endregion

        #region �����ؼ�����������Ч��

        /// <summary>
        /// Ŀ��
        /// </summary>
        Control _Target;

        /// <summary>
        /// ����ʱ�ĳߴ�
        /// </summary>
        int _SizeShrink = 10;

        /// <summary>
        /// ��չʱ�ĳߴ�
        /// </summary>
        int _SizeSpread = 10;

        /// <summary>
        /// ����Ч���ļ���Ƕ�
        /// </summary>
        double RadianTick = 5 * (Math.PI / 180);

        /// <summary>
        /// ָʾ�������̷�Ϊ����������
        /// </summary>
        int _TickCount = 10;

        /// <summary>
        /// ָ���������������������ʱ�������Ժ���Ϊ��λ
        /// </summary>
        int _TickPause = 50;

        #endregion

        CancelEventArgs FlexingArgs;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�����չ״̬
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

        #region �ؼ��������

        /// <summary>
        /// �Ƿ����
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
        /// ������ɫ
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
        /// ������ɫ
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
        /// ��ʾ����ʾ��Ϣ
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
        /// ��ǩ��ʽ
        /// </summary>
        public int LabelStyle
        {
            get { return _LabelStyle; }
            set { _LabelStyle = value; }
        }

        #endregion

        #region �����ؼ�������Ч��

        /// <summary>
        /// �����Ŀؼ�
        /// </summary>
        public Control Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Orientation Orientation
        {
            get { return Orientation.Vertical; }
            set { }
        }

        /// <summary>
        /// ����ʱ�ĳߴ�
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
        /// ��չʱ�ĳߴ�
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
        /// ��ֵ����ָ�����������̷�Ϊ���ٴ���ɡ���ֵԽ�����������̵�ƽ��Ч��Խ�ã�������ҲԽ���Ƽ���8��20֮�䡣
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
        /// ָ���������������������ʱ�������Ժ���Ϊ��λ
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

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        private event CancelEventHandler _Flexing;

        /// <summary>
        /// �����¼�
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
        /// �����������¼�
        /// </summary>
        private event EventHandler _Flexed;

        /// <summary>
        /// �����������¼�
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

        #region ����

        public FlexLabel()
        {
            InitializeComponent();

            #region ����

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
        /// ��������״̬
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

            string FlexTag = (IsSpread) ? "��" : "��";

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
        /// ��ȡһ����������Ϊ���ε�����
        /// </summary>
        /// <param name="Rect">��������ľ���</param>
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
                        #region ����Բ��

                        ArcRect = new Rectangle(Rect.Location, new Size(Rect.Height, Rect.Height));

                        //���
                        Path.AddArc(ArcRect, 90, 180);

                        //�ұ�
                        ArcRect.X = Rect.X + Rect.Width - Rect.Height;
                        Path.AddArc(ArcRect, 270, 180);

                        break;
                        #endregion

                    case 1:
                        #region �Ľ�Բ��

                        ArcRect = new Rectangle(Rect.Location, new Size(8, 8));

                        //���Ͻ�
                        Path.AddArc(ArcRect, 180, 90);


                        ArcRect.X = Rect.Width - 9;
                        //���Ͻ�
                        Path.AddArc(ArcRect, 270, 90);

                        ArcRect.Y = Rect.Height - 9;
                        //���½�
                        Path.AddArc(ArcRect, 0, 90);

                        ArcRect.X = 0;
                        //���½�
                        Path.AddArc(ArcRect, 90, 90);

                        break;

                        #endregion

                    case 2:
                        #region �Ľ�ƽ��

                        Path.AddLine(0, 2, 2, 0);
                        Path.AddLine(Rect.Width - 2, 0, Rect.Width, 2);
                        Path.AddLine(Rect.Width, Rect.Height - 2, Rect.Width - 2, Rect.Height);
                        Path.AddLine(2, Rect.Height, 0, Rect.Height - 2);

                        break;
                        #endregion

                    default:
                        break;
                }

                //�պ�
                Path.CloseFigure();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Path;
        }

        /// <summary>
        /// ����Ŀ��ؼ�
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
