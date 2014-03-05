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
        /// ���캯��
        /// </summary>
        public FrmCommonSet()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �洢������Ҫ�������壬�����IDΪ������������Ϊֵ
        /// </summary>
        Hashtable PanelBuffer = new Hashtable();

        /// <summary>
        /// ������ʾ������б�
        /// </summary>
        List<IPanelCommonSet> PanelShowing = new List<IPanelCommonSet>(3);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrMes = string.Empty;

        #endregion

        #region ����

        /// <summary>
        /// ��ȡָ��ID�����
        /// </summary>
        /// <param name="ID">���ID</param>
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
        /// ��ȡָ������塣�������ڣ���ʹ��ָ�����Ͷ��󴴽�һ������壬����彫���뻺��
        /// </summary>
        /// <param name="ID">���ID</param>
        /// <param name="PanelType">�������</param>
        /// <returns>������</returns>
        public IPanelCommonSet this[object ID, Type PanelType]
        {
            get
            {
                IPanelCommonSet Result = null;

                do
                {
                    if (ID == null)
                    {
                        ErrMes = "���ID����������Ϊ�գ�";
                        break;
                    }

                    Result = PanelBuffer[ID] as IPanelCommonSet;

                    if (Result != null)
                    {
                        if (!(Result.GetType() == PanelType))
                        {
                            ErrMes = string.Format("�Ѵ���һ��IDΪ<{0}>������Ϊ<{1}>����壡", ID, PanelType.Name);
                            Result = null;                            
                        }
                        break;
                    }                    

                    if (PanelType == null)
                    {
                        this.ErrMes = "PanelType����������Ϊ�գ�";
                        break;
                    }

                    if (!PanelType.IsSubclassOf(typeof(UserControl)) || PanelType.GetInterface("IPanelCommonSet", false) == null)
                    {
                        this.ErrMes = "PanelType����ָ��������Ӧ�̳���UserControl��ʵ��IPanelCommonSet�ӿڣ�";
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
                        this.ErrMes = "����ʼ�������쳣����ȷ�����������һ�������޲ι��캯����";
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

        #region �¼���Ӧ

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

        #region ����

        /// <summary>
        /// ��ȡһ���µ�TabPage
        /// </summary>
        /// <param name="Panel">TabPage�а��������</param>
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
        /// �򻺴������һ�������
        /// </summary>
        /// <param name="PanelType">������Ͷ���</param>
        /// <param name="ID">���ID</param>
        /// <param name="Panel">������</param>
        private int AddPanel(object ID, IPanelCommonSet Panel)
        {
            if (PanelBuffer.ContainsKey(ID)) return -1;

            Panel.ID = ID;

            Panel.OwnerPage = NewTabPage(Panel as UserControl);

            this.PanelBuffer.Add(ID, Panel);

            return 1;
        }

        /// <summary>
        /// ��ȡָ��ID����壬�������ڣ��򴴽�һ������壬����彫���뻺��
        /// </summary>
        public T GetPanel<T>(object ID) where T : UserControl, IPanelCommonSet, new()
        {
            IPanelCommonSet Result = null;

            do
            {
                if (ID == null)
                {
                    ErrMes = "���ID����������Ϊ�գ�";
                    break;
                }

                Result = PanelBuffer[ID] as IPanelCommonSet;
                if(!(Result is T))
                {
                    ErrMes = string.Format("�Ѵ���һ��IDΪ<{0}>������Ϊ<{1}>����壡",ID,typeof(T).Name);
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
                    this.ErrMes = "����ʼ�������쳣��";
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

            #region ����Ҫ��ʾ����弯��

            foreach (object panel in Panels)
            {
                //����һ�����ID��һ������
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

                //����һ�����ID
                if (this.PanelBuffer.ContainsKey(panel))
                {
                    this.PanelShowing.Add(this.PanelBuffer[panel] as IPanelCommonSet);
                    continue;
                }

                //����һ��������
                Panel = panel as IPanelCommonSet;
                if (Panel is IPanelCommonSet && Panel is UserControl)
                {
                    Panel.OwnerPage = this.NewTabPage(Panel as UserControl);
                    this.PanelShowing.Add(Panel);
                }
            }

            #endregion

            //��ʼ�����
            for (int i = this.PanelShowing.Count - 1; i >= 0; --i)
            {
                if (this.PanelShowing[i].Show() != 1) this.PanelShowing.RemoveAt(i);
            }

            //����TabPage����
            this.tabControl.TabPages.Clear();
            foreach (IPanelCommonSet panel in this.PanelShowing)
            {
                panel.OwnerPage.Text = panel.Title;
                this.tabControl.TabPages.Add(panel.OwnerPage);
            }

            //���ñ���
            this.Text = Title;

            //��ʾ���öԻ���
            if (this.ShowDialog(Owner) == DialogResult.Cancel) return 0;

            //��������
            foreach (IPanelCommonSet panel in this.PanelShowing)
            {
                panel.Save();
            }

            return 1;
        }

        /// <summary>
        /// ��ʾָ�������
        /// </summary>
        public int ShowDialog(string Title, params object[] Panels)
        {
            return InnelShowDialog(Form.ActiveForm,Title, Panels);
        }

        /// <summary>
        /// ��ʾָ�������
        /// </summary>
        /// <param name="Title">�Ի������</param>
        /// <param name="Panels">���</param>
        /// <returns>1:�������;0:����ȡ��;-1:����</returns>
        public int ShowDialog(string Title, IEnumerable Panels)
        {
            return InnelShowDialog(Form.ActiveForm, Title, Panels);
        }

        /// <summary>
        /// �Ƴ�ָ��ID�����
        /// </summary>
        /// <param name="ID">���ID</param>
        /// <returns>δ����</returns>
        public int RemovePanel(object ID)
        {
            this.PanelBuffer.Remove(ID);

            return 1;
        }

        #endregion

    }
}