using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.ExtendedControl
{
    public partial class ucColorsSettingX : ListView
    {
        public ucColorsSettingX()
        {
            InitializeComponent();

            _Items = new List<object>(10);
            _Colors = new List<Color>(10);
        }

        #region Fileds

        /// <summary>
        /// 要设置的项列表
        /// </summary>
        private List<object> _Items;

        /// <summary>
        /// 各项的颜色列表
        /// </summary>
        private List<Color> _Colors;

        /// <summary>
        /// 默认颜色
        /// </summary>
        private Color _DefaultColor;

        #endregion        

        #region Properties

        /// <summary>
        /// 各项的颜色列表
        /// </summary>
        public IEnumerable<Color> Colors
        {
            get
            {
                return _Colors;
            }
            set
            {
                if (object.ReferenceEquals(null, value)) return;
                IEnumerator<Color> enumerator = value.GetEnumerator();
                int i = -1;

                while (enumerator.MoveNext() && ++i < _Colors.Count)
                {
                    _Colors[i] = enumerator.Current;
                    UpdateImage(i);
                }
            }
        }

        /// <summary>
        /// 要设置的项列表
        /// </summary>
        public new IEnumerable<object> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items.Clear();
                if (!object.ReferenceEquals(null, _Items)) _Items.AddRange(value);

                InitColorList();

                base.Items.Clear();
                foreach (object obj in _Items)
                {
                    base.Items.Add(new ListViewItem(obj.ToString()));
                }
                for (int i = 0; i < base.Items.Count; ++i)
                {
                    base.Items[i].ImageIndex = i;
                }
            }
        }

        /// <summary>
        /// 默认颜色
        /// </summary>
        public Color DefaultColor
        {
            get
            {
                return _DefaultColor;
            }
            set
            {
                _DefaultColor = value;
            }
        }

        /// <summary>
        /// 项数
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _Items.Count;
            }
        }

        #endregion

        #region interface

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="color">颜色</param>
        public void AddItem(object obj, Color color)
        {
            _Items.Add(obj);
            _Colors.Add(color);

            if (imageList.Images.Count < _Items.Count)
            {
                Bitmap image = new Bitmap(imageList.ImageSize.Width, imageList.ImageSize.Height);
                Graphics g = Graphics.FromImage(image);
                SolidBrush brush = new SolidBrush(DefaultColor);
                g.FillRectangle(brush, new Rectangle(new Point(0, 0), image.Size));

                imageList.Images.Add(image);  
            }

            UpdateImage(_Items.Count - 1);

            base.Items.Add(obj.ToString());
            base.Items[base.Items.Count - 1].ImageIndex = base.Items.Count - 1;            
        }

        /// <summary>
        /// 清空项
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
            _Colors.Clear();
            base.Items.Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化颜色列表
        /// </summary>
        protected void InitColorList()
        {
            if (_Items.Count > _Colors.Capacity) _Colors.Capacity = _Items.Count;

            //使Images的数量不少于Items的数量
            if (imageList.Images.Count < _Items.Count)
            {
                for (int i = imageList.Images.Count; i < _Items.Count; ++i)
                {
                    Bitmap image = new Bitmap(imageList.ImageSize.Width, imageList.ImageSize.Height);
                    Graphics g = Graphics.FromImage(image);
                    SolidBrush brush = new SolidBrush(DefaultColor);
                    g.FillRectangle(brush, new Rectangle(new Point(0, 0), image.Size));

                    imageList.Images.Add(image);                    
                }
            }

            //使Colors的数量与Items的数量保持一致
            if (_Colors.Count > _Items.Count)
            {
                for (int i = _Colors.Count - 1; i >= _Items.Count; --i)
                {
                    _Colors.RemoveAt(i);
                }
            }
            else if (_Colors.Count < _Items.Count)
            {
                for (int i = _Colors.Count; i < _Items.Count; ++i)
                {
                    _Colors.Add(DefaultColor);
                    UpdateImage(i);
                }
            }
        }

        /// <summary>
        /// 更新指定索引项的显示图形
        /// </summary>
        /// <param name="idx"></param>
        protected void UpdateImage(int idx)
        {
            Graphics g = Graphics.FromImage(imageList.Images[idx]);
            SolidBrush brush = new SolidBrush(_Colors[idx]);
            g.FillRectangle(brush, new Rectangle(new Point(0, 0), imageList.Images[idx].Size));
        }

        #endregion
    }
}

