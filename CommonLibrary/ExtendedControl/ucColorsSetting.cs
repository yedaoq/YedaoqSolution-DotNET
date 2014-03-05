using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CommonLibrary.ExtendedControl
{
    public partial class ucColorsSetting : UserControl
    {
        private List<object> _ObjectItems;
        private List<Color> _ColorItems;
    
        public ucColorsSetting()
        {
            InitializeComponent();
        }

        public IEnumerable<Color> ObjectItems
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IEnumerable ColorItems
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
