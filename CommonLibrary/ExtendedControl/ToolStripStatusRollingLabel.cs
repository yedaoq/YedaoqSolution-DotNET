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
    /// ������ʾ��״̬����ǩ��
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

        #region ����

        /// <summary>
        /// ��ʱ��
        /// </summary>
        private Timer UpdateTimer;

        /// <summary>
        /// ��ˢ
        /// </summary>
        SolidBrush Brush;

        /// <summary>
        /// ������ʾ���ݵľ���
        /// </summary>
        Rectangle StringRect = Rectangle.Empty;

        /// <summary>
        /// ������ƫ����������
        /// </summary>
        IControlDataRollOffsetGenerater ControlDataRollOffsetGenerater;

        /// <summary>
        /// ͼ��
        /// </summary>
        Graphics Graphics;

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private string _Text;

        /// <summary>
        /// ��������
        /// </summary>
        private int _RollDistance = 0;

        /// <summary>
        /// �ؼ����ݵĹ�����ʽ
        /// </summary>
        private EnumTextRollStyle _RollStyle = EnumTextRollStyle.TurnLeft;

        /// <summary>
        /// �ı����
        /// </summary>
        private int _TextWidth;

        #endregion

        #region ����

        /// <summary>
        /// �ı�����
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
        /// �ı����
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
        /// ǰ��ɫ
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
        /// �ؼ���С
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
        /// �ڲ����
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
        /// �����ٶ�
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
        /// �����ٶȣ�ÿ��ˢ���ı���ʾλ�õļ��
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
        /// ��ǰ��ʾλ�����ʼλ�õľ���
        /// </summary>
        public int RollDistance
        {
            get { return _RollDistance; }
            set { _RollDistance = value; }
        }

        /// <summary>
        /// �ؼ����ݵĹ�����ʽ
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
    /// �ؼ��ı����ݹ�������ʽ
    /// </summary>
    public enum EnumTextRollStyle
    {
        /// <summary>
        /// �������
        /// </summary>
        TurnLeft,

        /// <summary>
        /// �������
        /// </summary>
        TurnRight,

        /// <summary>
        /// ���ع���
        /// </summary>
        PingPong
    }

    public class ControlDataRollOffsetGeneraterFactory
    {
        #region ����

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
        /// ��ȡһ�����ݹ�����ƫ����������
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
    /// �ؼ����ݹ���ƫ�����������Ľӿ�
    /// </summary>
    public interface IControlDataRollOffsetGenerater
    {
        /// <summary>
        /// ��ʾ������
        /// </summary>
        int WindowWidth { get;set;}

        /// <summary>
        /// ���ݿ��
        /// </summary>
        int DataWidth { get;set;}

        /// <summary>
        /// ƫ����
        /// </summary>
        int Offset { get;set;}

        /// <summary>
        /// �ƶ��ٶ�
        /// </summary>
        int Speed { get;set;}

        /// <summary>
        /// ��ȡ��һ��ƫ����
        /// </summary>
        /// <returns>��ƫ����</returns>
        int GetNextOffset();
    }

    /// <summary>
    /// �ؼ����ݹ���ƫ��������������
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

        #region ����

        /// <summary>
        /// ��ʾ������
        /// </summary>
        int _WindowWidth;

        /// <summary>
        /// ���ݿ��
        /// </summary>
        int _DataWidth;

        /// <summary>
        /// ƫ����
        /// </summary>
        int _Offset;

        /// <summary>
        /// �ƶ��ٶ�
        /// </summary>
        int _Speed;

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ������
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
        /// ���ݿ��
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
        /// ƫ����
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
        /// �ƶ��ٶ�
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
        /// ��ȡ��һ��ƫ����
        /// </summary>
        /// <returns>��ƫ����</returns>
        public virtual int GetNextOffset()
        {
            return 0;
        }
    }

    /// <summary>
    /// �ؼ��������������ƫ����������
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

        #region ����

        //private int _RollWidth;

        #endregion

        #region ����

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
        /// ��ȡһ���µ�ƫ����
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
