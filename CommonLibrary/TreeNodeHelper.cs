/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：SourceGridHelper.cs

 * 说明：TreeNode的辅助类

 * 作者：叶道全
 
 * 时间：2009年6月9日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// TreeNode类的辅助类,此类的所有非静态接口都是非线程安全的
    /// </summary>
    public class TreeNodeHelper
    {
        #region 单例

        static Singleton<TreeNodeHelper> Singletin;

        /// <summary>
        /// 实例
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
        /// 查找的匹配条件，与Find、InnerFind函数配合使用
        /// </summary>
        private Predicate<TreeNode> FindMatch;

        /// <summary>
        /// 从以指定节点为根的TreeNode树中查找节点
        /// </summary>
        /// <param name="Match">匹配条件</param>
        /// <returns>返回找到的第一个节点</returns>
        public TreeNode Find(TreeNode Root, Predicate<TreeNode> Match)
        {
            if (Root == null) return null;

            if (Match(Root)) return Root;

            this.FindMatch = Match;
            return InnerFind(Root);
        }

        /// <summary>
        /// 内部函数，与Find函数配合使用
        /// <param name="Root">要搜索的目标树的根节点</param>
        /// </summary>
        /// <returns>返回找到的节点</returns>
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
