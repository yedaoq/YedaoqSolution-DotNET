using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.CommonImput
{
    /// <summary>
    /// ͨ���������Ļ���
    /// </summary>
    public class ucPanelCommonSetBase : UserControl, IPanelCommonSet
    {
        public ucPanelCommonSetBase()
        {
            if (DesignMode) return;

            this.BackColor = System.Drawing.SystemColors.Window;
        }

        #region ����

        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        protected bool _ReadOnly;

        /// <summary>
        /// �Ƿ��������ѡ��(��ȫѡ,��ѡ)
        /// </summary>
        protected bool _BatchSelectEnabled;

        /// <summary>
        /// ���ID������Ψһ��ʶ���
        /// </summary>
        protected object _ID;

        /// <summary>
        /// ��������TabPage����ֵ�ṩ��FrmCommonSetʹ�á��ӿڵ�ʵ���������޸Ĵ�ֵ��
        /// </summary>
        protected TabPage _OwnerPage;

        /// <summary>
        /// �ؼ���ر�ǩҳ����ʾ���ı�
        /// </summary>
        string _Title;

        #endregion

        #region ����

        /// <summary>
        /// ����Ψһ��ʶ���
        /// </summary>
        public virtual object ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// ��������TabPage����ֵ�ṩ��FrmCommonSetʹ�á��ӿڵ�ʵ���������޸Ĵ�ֵ��
        /// </summary>
        public TabPage OwnerPage
        {
            get { return _OwnerPage; }
            set { _OwnerPage = value; }
        }

        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return _ReadOnly; }
            set { _ReadOnly = value; }

        }

        /// <summary>
        /// �Ƿ��������ѡ��(��ȫѡ,��ѡ)
        /// </summary>
        public virtual bool BatchSelectEnabled
        {
            get { return _BatchSelectEnabled; }
            set { _BatchSelectEnabled = value; }
        }

        /// <summary>
        /// �ؼ���ر�ǩҳ����ʾ���ı�
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ʼ��
        /// </summary>
        /// <returns></returns>
        public virtual int Init()
        {
            return 1;
        }

        /// <summary>
        /// ��ʾǰ����
        /// </summary>
        /// <returns></returns>
        public virtual int Show()
        {
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            return 1;
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <returns></returns>
        public virtual int SelectAll()
        {
            return 1;
        }

        /// <summary>
        /// ��ѡ
        /// </summary>
        /// <returns></returns>
        public virtual int SelectReverse()
        {
            return 1;
        }

        #endregion
    }
}
