/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��ExTreeNode.cs

 * ˵������չ��TreeNode����

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��6��9��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// �����һЩ��չ���Ե����ڵ�
    /// ���ཫ�ṩһЩ�������ڱ�����ڵ������ָ����ID�Լ�������Ҫִ�еĲ�������Ϣ
    /// ������TreeNodeExtendAttribute�����ṩЧ������һ��ʵ��
    /// </summary>
    public class ExTreeNode : TreeNode
    {
        #region ��չ����

        /// <summary>
        /// �ڵ�����ʹ���
        /// �Ƽ��û�����ҵ����һ�׽ڵ������ö�٣�ͨ��ǿ��ת�������ô����Դ洢�ڵ������
        /// </summary>
        public int Type;

        /// <summary>
        /// �ڵ���ָ�����ID
        /// </summary>
        public int ID;

        /// <summary>
        /// �ӽڵ��Ƿ������ɵı��
        /// </summary>
        public bool ChildInitFlag = false;

        /// <summary>
        /// �����ڵ�ǰִ�еĲ���
        /// </summary>
        public TreeViewCancelEventHandler BeforeClick;

        #endregion

        public ExTreeNode()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Type">�ڵ�����</param>
        /// <param name="ID">�ڵ���ָ�����ID</param>
        /// <param name="Text">�ڵ���ʾ���ı�</param>
        /// <param name="OnBeforeClick">�����ڵ���ִ�еĲ���</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Type">�ڵ�����</param>
        /// <param name="ID">�ڵ���ָ�����ID</param>
        /// <param name="Text">�ڵ���ʾ���ı�</param>
        /// <param name="OnBeforeClick">�����ڵ���ִ�еĲ���</param>
        /// <param name="ChildInitFlag">�ӽڵ��Ƿ��ѳ�ʼ���ı��</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick, bool ChildInitFlag)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
            this.ChildInitFlag = ChildInitFlag;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Type">�ڵ�����</param>
        /// <param name="ID">�ڵ���ָ�����ID</param>
        /// <param name="Text">�ڵ���ʾ���ı�</param>
        /// <param name="OnBeforeClick">�����ڵ���ִ�еĲ���</param>
        /// <param name="Tag">�û��Զ�������</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick, object Tag)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
            this.Tag = Tag;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Type">�ڵ�����</param>
        /// <param name="ID">�ڵ���ָ�����ID</param>
        /// <param name="Text">�ڵ���ʾ���ı�</param>
        /// <param name="OnBeforeClick">�����ڵ���ִ�еĲ���</param>
        /// <param name="Tag">�û��Զ�������</param>
        /// <param name="ChildInitFlag">�ӽڵ��Ƿ��ѳ�ʼ���ı��</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick, object Tag, bool ChildInitFlag)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
            this.Tag = Tag;
            this.ChildInitFlag = ChildInitFlag;
        }
    }

    /// <summary>
    /// ���ڵ����չ���ԣ������ṩһЩ�������ڱ�����ڵ������ָ����ID�Լ�������Ҫִ�еĲ�������Ϣ
    /// ������ExTreeNode�����ṩЧ������һ��ʵ��
    /// </summary>
    public class TreeNodeExtendAttribute
    {
        /// <summary>
        /// �ڵ�����ʹ���
        /// �Ƽ��û�����ҵ����һ�׽ڵ������ö�٣�ͨ��ǿ��ת�������ô����Դ洢�ڵ������
        /// </summary>
        public int Type;

        /// <summary>
        /// �ڵ���ָ�����ID
        /// </summary>
        public int ID;

        /// <summary>
        /// �ӽڵ��ʼ�����
        /// </summary>
        public bool ChildInitFlag = false;

        /// <summary>
        /// ��ǣ����ڴ洢�����û�����
        /// �Ƽ��û���TreeNode.Tag���洢��չ���Զ��������������ݣ������ʹ��TreeNodeExtendAttribute.Tag���洢
        /// </summary>
        public object Tag;

        /// <summary>
        /// �����ڵ�ǰִ�еĲ���
        /// </summary>
        public TreeViewCancelEventHandler OnBeforeClick;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="ID"></param>
        /// <param name="OnBeforeClick"></param>
        public TreeNodeExtendAttribute(int Type, int ID, TreeViewCancelEventHandler OnBeforeClick)
        {
            this.Type = Type;
            this.ID = ID;
            this.OnBeforeClick = OnBeforeClick;
        }
    }

    /// <summary>
    /// �������ڵ�ʱִ�еĲ�����ί��
    /// </summary>
    /// <param name="Node">�������Ľڵ�</param>
    /// <returns></returns>
    //public delegate int TreeNodeBeforeClickedEventHandler(ExTreeNode Node);
}
