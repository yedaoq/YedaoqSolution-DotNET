/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��SourceGridHelper.cs

 * ˵����TreeNode�ĸ�����

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
    /// TreeNode��ĸ�����,��������зǾ�̬�ӿڶ��Ƿ��̰߳�ȫ��
    /// </summary>
    public class TreeNodeHelper
    {
        #region ����

        static Singleton<TreeNodeHelper> Singletin;

        /// <summary>
        /// ʵ��
        /// </summary>
        public static TreeNodeHelper Instance
        {
            get
            {
                if (Singletin == null) Singletin = new Singleton<TreeNodeHelper>();
                return Singletin.Instance;
            }
        }

        #endregion

        /// <summary>
        /// ���ҵ�ƥ����������Find��InnerFind�������ʹ��
        /// </summary>
        private Predicate<TreeNode> FindMatch;

        /// <summary>
        /// ����ָ���ڵ�Ϊ����TreeNode���в��ҽڵ�
        /// </summary>
        /// <param name="Match">ƥ������</param>
        /// <returns>�����ҵ��ĵ�һ���ڵ�</returns>
        public TreeNode Find(TreeNode Root, Predicate<TreeNode> Match)
        {
            if (Root == null) return null;

            if (Match(Root)) return Root;

            this.FindMatch = Match;
            return InnerFind(Root);
        }

        /// <summary>
        /// �ڲ���������Find�������ʹ��
        /// <param name="Root">Ҫ������Ŀ�����ĸ��ڵ�</param>
        /// </summary>
        /// <returns>�����ҵ��Ľڵ�</returns>
        private TreeNode InnerFind(TreeNode Root)
        {
            TreeNode Result = null;

            foreach (TreeNode Child in Root.Nodes)
            {
                if (this.FindMatch(Child)) Result = Child;
                TreeNode Temp = InnerFind(Child);
                if (Temp != null) Result = Temp;
            }

            return Result;
        }
    }
}
