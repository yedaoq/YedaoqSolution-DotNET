using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.EditPanel
{
    /// <summary>
    /// ����༭������
    /// </summary>
    public class VirtualEditPanelBase : IEditPanel
    {
        #region Fields

        /// <summary>
        /// ��ʾ�����ʱ��ʾ����Ϣ
        /// </summary>
        private string _TipSave = "�����б�����Ƿ񱣴棿";

        /// <summary>
        /// ���ݸ��ı��
        /// </summary>
        private bool _DataChanged = false;

        /// <summary>
        /// �༭�Ƿ��Ѿ����
        /// </summary>
        private bool _EditCompleted = false;

        /// <summary>
        /// �༭ģʽ
        /// </summary>
        private EnumEditMode _EditMode = EnumEditMode.View;

        #endregion

        #region Properties

        /// <summary>
        /// ��ʾ�����ʱ��ʾ����Ϣ
        /// </summary>
        protected virtual string TipSave
        {
            get { return "�����б�����Ƿ񱣴棿"; }
        }

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public virtual bool DataChanged
        {
            get { return _DataChanged; }
            set { _DataChanged = value; }
        }

        /// <summary>
        /// �༭ģʽ
        /// </summary>
        public virtual EnumEditMode EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="obj">Ŀ������</param>
        /// <param name="editMode">�༭ģʽ</param>
        /// <returns></returns>
        public virtual int LoadData(object obj, EnumEditMode editMode)
        {
            //��鵱ǰ�༭�����Ƿ��ѱ���
            if (this.CheckToSave() != 1) return -1;

            return 1;
        }

        /// <summary>
        /// ��鲢��������
        /// </summary>
        /// <returns>1:����ɹ����û�ѡ���������;0:�û�ȡ������;-1:����ʧ��</returns>
        public int CheckToSave()
        {
            if (EditMode == EnumEditMode.View) return 1;

            //����ɱ༭
            CompleteEdit();

            //���༭ģʽ�����ݱ仯���
            if (!DataChanged) return 1;

            //��ʾ����
            switch (MessageBox.Show(TipSave, "��ʾ", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    return (this.SaveData() == 1) ? 1 : -1; //����ɹ�ʱ����1,���ɹ�����-1;

                case DialogResult.No:
                    CancelEdit();
                    DataChanged = false;
                    return 1;

                case DialogResult.Cancel:
                    return 0;
            }
            return 1;
        }

        /// <summary>
        /// ��������,�˹��̽�����CompleteEdit
        /// </summary>
        /// <returns>1:�ɹ�;-1:ʧ��</returns>
        public int Save()
        {
            if (EditMode == EnumEditMode.View) return 1;

            //����ɱ༭
            CompleteEdit();

            //���༭ģʽ�����ݱ仯���
            if (!DataChanged) return 1;

            //����
            if (SaveData() != 1) return -1;
            DataChanged = false;
            return 1;
        }

        /// <summary>
        /// ��ɱ༭
        /// </summary>
        /// <returns>1:�ɹ�;-1:����</returns>
        public virtual int CompleteEdit()
        {
            return 1;
        }

        /// <summary>
        /// ȡ���༭
        /// </summary>
        /// <returns></returns>
        public virtual int CancelEdit()
        {
            DataChanged = false;
            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual int SaveData()
        {
            return 1;
        }

        #endregion
    }
}
