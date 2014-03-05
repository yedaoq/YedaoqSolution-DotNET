using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.CommonImput
{
    /// <summary>
    /// ͨ���������Ľӿ�
    /// </summary>
    public interface IPanelCommonSet
    {
        /// <summary>
        /// ���Ĺؼ��֣�����Ψһ��ʶ���
        /// </summary>
        object ID { get;set;}

        /// <summary>
        /// ��������TabPage����ֵ�ṩ��FrmCommonSetʹ�á��ӿڵ�ʵ���������޸Ĵ�ֵ��
        /// </summary>
        TabPage OwnerPage { get;set;}

        /// <summary>
        /// ����ʼ�����
        /// </summary>
        //bool InitFlag { get;set;}

        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        bool ReadOnly { get;set;}

        /// <summary>
        /// �Ƿ��������ѡ��(��ȫѡ,��ѡ)
        /// </summary>
        bool BatchSelectEnabled { get;set;}

        /// <summary>
        /// �ؼ���ر�ǩҳ����ʾ���ı�
        /// </summary>
        string Title { get;set;}

        /// <summary>
        /// ����ʼ��
        /// </summary>
        /// <returns></returns>
        //int Init();

        /// <summary>
        /// ��ʾǰ����
        /// </summary>
        /// <returns></returns>
        int Show();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        int Save();

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <returns></returns>
        int SelectAll();

        /// <summary>
        /// ��ѡ
        /// </summary>
        /// <returns></returns>
        int SelectReverse();
    }
}
