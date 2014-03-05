using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime;

namespace CommonLibrary.CommonImput
{
    public partial class FrmCommonSet : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCommonSet()
        {
            InitializeComponent();
        }

        #region 数据

        /// <summary>
        /// 存储所有需要缓存的面板，以面板ID为键，以面板对象为值
        /// </summary>
        Hashtable PanelBuffer = new Hashtable();

        /// <summary>
        /// 本次显示的面板列表
        /// </summary>
        List<IPanelCommonSet> PanelShowing = new List<IPanelCommonSet>(3);

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMes = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 获取指定ID的面板
        /// </summary>
        /// <param name="ID">面板ID</param>
        /// <returns></returns>
        public IPanelCommonSet this[object ID]
        {
            get
            {
                if (ID == null) return null;
                return PanelBuffer[ID] as IPanelCommonSet;
            }
        }

        /// <summary>
        /// 获取指定的面板。若不存在，则使用指定类型对象创建一个新面板，新面板将加入缓存
        /// </summary>
        /// <param name="ID">面板ID</param>
        /// <param name="PanelType">面板类型</param>
        /// <returns>面板对象</returns>
        public IPanelCommonSet this[object ID, Type PanelType]
        {
            get
            {
                IPanelCommonSet Result = null;

                do
                {
                    if (ID == null)
                    {
                        ErrMes = "面板ID参数不允许为空！";
                        break;
                    }

                    Result = PanelBuffer[ID] as IPanelCommonSet;

                    if (Result != null)
                    {
                        if (!(Result.GetType() == PanelType))
                        {
                            ErrMes = string.Format("已存在一个ID为<{0}>，类型为<{1}>的面板！", ID, PanelType.Name);
                            Result = null;                            
                        }
                        break;
                    }                    

                    if (PanelType == null)
                    {
                        this.ErrMes = "PanelType参数不允许为空！";
                        break;
                    }

                    if (!PanelType.IsSubclassOf(typeof(UserControl)) || PanelType.GetInterface("IPanelCommonSet", false) == null)
                    {
                        this.ErrMes = "PanelType参数指定的类型应继承自UserControl并实现IPanelCommonSet接口！";
                        break;
                    }

#if !DEBUG
                    try
#endif
                    {
                        Result = System.Activator.CreateInstance(PanelType) as IPanelCommonSet; ;
                    }
#if !DEBUG
                    catch (Exception ex)
                    {
                        this.ErrMes = "面板初始化出现异常，请确保面板类型有一个公共无参构造函数！";
                    }
#endif
                    
                }
                while (false);

                if (Result == null)
                {
                    MessageBox.Show(this.ErrMes);
                }
                else
                {
                    AddPanel(ID, Result);
                }

                return Result;
            }
        }

        #endregion

        #region 事件响应

        private void buttonAll_Click(object sender, EventArgs e)
        {
            this.PanelShowing[this.tabControl.SelectedIndex].SelectAll();
        }

        private void buttonReverse_Click(object sender, EventArgs e)
        {
            this.PanelShowing[this.tabControl.SelectedIndex].SelectReverse();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (IPanelCommonSet panel in this.PanelShowing) panel.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmCommonSet_Load(object sender, EventArgs e)
        {
            tabControl_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool BatchSelecEnabled = (this.tabControl.SelectedIndex < 0) ? false : this.PanelShowing[this.tabControl.SelectedIndex].BatchSelectEnabled;
            //this.buttonReverse.Visible = BatchSelecEnabled;
            //this.buttonAll.Visible = BatchSelecEnabled;

            this.buttonReverse.Enabled = BatchSelecEnabled;
            this.buttonAll.Enabled = BatchSelecEnabled;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取一个新的TabPage
        /// </summary>
        /// <param name="Panel">TabPage中包含的面板</param>
        private TabPage NewTabPage(UserControl Panel)
        {
            TabPage Result = new TabPage();
            Result.SuspendLayout();
            Result.Controls.Add(Panel);
            Panel.Dock = DockStyle.Fill;
            Result.ResumeLayout();

            return Result;
        }

        /// <summary>
        /// 向缓存中添加一个新面板
        /// </summary>
        /// <param name="PanelType">面板类型对象</param>
        /// <param name="ID">面板ID</param>
        /// <param name="Panel">面板对象</param>
        private int AddPanel(object ID, IPanelCommonSet Panel)
        {
            if (PanelBuffer.ContainsKey(ID)) return -1;

            Panel.ID = ID;

            Panel.OwnerPage = NewTabPage(Panel as UserControl);

            this.PanelBuffer.Add(ID, Panel);

            return 1;
        }

        /// <summary>
        /// 获取指定ID的面板，若不存在，则创建一个新面板，新面板将加入缓存
        /// </summary>
        public T GetPanel<T>(object ID) where T : UserControl, IPanelCommonSet, new()
        {
            IPanelCommonSet Result = null;

            do
            {
                if (ID == null)
                {
                    ErrMes = "面板ID参数不允许为空！";
                    break;
                }

                Result = PanelBuffer[ID] as IPanelCommonSet;
                if(!(Result is T))
                {
                    ErrMes = string.Format("已存在一个ID为<{0}>，类型为<{1}>的面板！",ID,typeof(T).Name);
                }

                if (Result != null) break;

#if DEBUG
                try
#endif
                {
                    Result = new T();
                }
#if DEBUG
                catch (Exception ex)
                {
                    this.ErrMes = "面板初始化出现异常！";
                }
#endif
            }
            while (false);

            if (Result == null)
            {
                MessageBox.Show(this.ErrMes);
            }
            else
            {
                AddPanel(ID, Result);
            }

            return Result as T;
        }

        public int InnelShowDialog(IWin32Window Owner, string Title, IEnumerable Panels)
        {
            IPanelCommonSet Panel;

            this.PanelShowing.Clear();

            #region 生成要显示的面板集合

            foreach (object panel in Panels)
            {
                //传入一个面板ID和一个类型
                Pair<object, Type> pair = panel as Pair<object, Type>;
                if (pair != null)
                {
                    Panel = this[pair.First, pair.Second];
                    if (Panel != null)
                    {
                        this.PanelShowing.Add(Panel);
                    }
                    continue;
                }

                //传入一个面板ID
                if (this.PanelBuffer.ContainsKey(panel))
                {
                    this.PanelShowing.Add(this.PanelBuffer[panel] as IPanelCommonSet);
                    continue;
                }

                //传入一个面板对象
                Panel = panel as IPanelCommonSet;
                if (Panel is IPanelCommonSet && Panel is UserControl)
                {
                    Panel.OwnerPage = this.NewTabPage(Panel as UserControl);
                    this.PanelShowing.Add(Panel);
                }
            }

            #endregion

            //初始化面板
            for (int i = this.PanelShowing.Count - 1; i >= 0; --i)
            {
                if (this.PanelShowing[i].Show() != 1) this.PanelShowing.RemoveAt(i);
            }

            //生成TabPage集合
            this.tabControl.TabPages.Clear();
            foreach (IPanelCommonSet panel in this.PanelShowing)
            {
                panel.OwnerPage.Text = panel.Title;
                this.tabControl.TabPages.Add(panel.OwnerPage);
            }

            //设置标题
            this.Text = Title;

            //显示设置对话框
            if (this.ShowDialog(Owner) == DialogResult.Cancel) return 0;

            //保存数据
            foreach (IPanelCommonSet panel in this.PanelShowing)
            {
                panel.Save();
            }

            return 1;
        }

        /// <summary>
        /// 显示指定的面板
        /// </summary>
        public int ShowDialog(string Title, params object[] Panels)
        {
            return InnelShowDialog(Form.ActiveForm,Title, Panels);
        }

        /// <summary>
        /// 显示指定的面板
        /// </summary>
        /// <param name="Title">对话框标题</param>
        /// <param name="Panels">面板</param>
        /// <returns>1:设置完成;0:设置取消;-1:出错</returns>
        public int ShowDialog(string Title, IEnumerable Panels)
        {
            return InnelShowDialog(Form.ActiveForm, Title, Panels);
        }

        /// <summary>
        /// 移除指定ID的面板
        /// </summary>
        /// <param name="ID">面板ID</param>
        /// <returns>未定义</returns>
        public int RemovePanel(object ID)
        {
            this.PanelBuffer.Remove(ID);

            return 1;
        }

        #endregion

    }
}