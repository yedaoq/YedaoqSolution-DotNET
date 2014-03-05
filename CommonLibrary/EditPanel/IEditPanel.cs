using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.EditPanel
{
    /// <summary>
    /// �༭���Ľӿ�
    /// </summary>
    public interface IEditPanel
    {
        /// <summary>
        /// �����Ƿ��и���
        /// </summary>
        bool DataChanged { get;set;}

        /// <summary>
        /// �༭ģʽ
        /// </summary>
        EnumEditMode EditMode
        {
            get;
            set;
        }

        /// <summary>
        /// �������ݽӿ�
        /// </summary>
        /// <param name="obj">Ŀ������</param>
        /// <param name="editMode">�༭ģʽ</param>
        int LoadData(object obj, EnumEditMode editMode);

        /// <summary>
        /// �����ģ���Ҫʱ��ʾ����
        /// </summary>
        int CheckToSave();

        /// <summary>
        /// ���δ��ı༭��������������У�飬ʹ���ݴ���ȷ��״̬��
        /// ���ɱ��棬�򷵻�1��������������ֵ
        /// </summary>
        int CompleteEdit();

        /// <summary>
        /// ȡ�������ı༭
        /// </summary>
        int CancelEdit();

        /// <summary>
        /// �������ݽӿ�
        /// </summary>
        int Save();
    }
}
