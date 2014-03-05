/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：ExTreeNode.cs

 * 说明：扩展的TreeNode对象

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
    /// 添加了一些扩展属性的树节点
    /// 此类将提供一些属性用于标记树节点类别、所指代物ID以及单击所要执行的操作等信息
    /// 此类是TreeNodeExtendAttribute类所提供效果的另一种实现
    /// </summary>
    public class ExTreeNode : TreeNode
    {
        #region 扩展属性

        /// <summary>
        /// 节点的类型代码
        /// 推荐用户根据业务定义一套节点种类的枚举，通过强制转换来利用此属性存储节点的类型
        /// </summary>
        public int Type;

        /// <summary>
        /// 节点所指代物的ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 子节点是否已生成的标记
        /// </summary>
        public bool ChildInitFlag = false;

        /// <summary>
        /// 单击节点前执行的操作
        /// </summary>
        public TreeViewCancelEventHandler BeforeClick;

        #endregion

        public ExTreeNode()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Type">节点类型</param>
        /// <param name="ID">节点所指代物的ID</param>
        /// <param name="Text">节点显示的文本</param>
        /// <param name="OnBeforeClick">单击节点所执行的操作</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Type">节点类型</param>
        /// <param name="ID">节点所指代物的ID</param>
        /// <param name="Text">节点显示的文本</param>
        /// <param name="OnBeforeClick">单击节点所执行的操作</param>
        /// <param name="ChildInitFlag">子节点是否已初始化的标记</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick, bool ChildInitFlag)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
            this.ChildInitFlag = ChildInitFlag;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Type">节点类型</param>
        /// <param name="ID">节点所指代物的ID</param>
        /// <param name="Text">节点显示的文本</param>
        /// <param name="OnBeforeClick">单击节点所执行的操作</param>
        /// <param name="Tag">用户自定义数据</param>
        public ExTreeNode(int Type, int ID, string Text, TreeViewCancelEventHandler OnBeforeClick, object Tag)
        {
            this.Type = Type;
            this.ID = ID;
            this.Text = Text;
            this.BeforeClick = OnBeforeClick;
            this.Tag = Tag;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Type">节点类型</param>
        /// <param name="ID">节点所指代物的ID</param>
        /// <param name="Text">节点显示的文本</param>
        /// <param name="OnBeforeClick">单击节点所执行的操作</param>
        /// <param name="Tag">用户自定义数据</param>
        /// <param name="ChildInitFlag">子节点是否已初始化的标记</param>
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
    /// 树节点的扩展属性：此类提供一些属性用于标记树节点类别、所指代物ID以及单击所要执行的操作等信息
    /// 此类是ExTreeNode类所提供效果的另一种实现
    /// </summary>
    public class TreeNodeExtendAttribute
    {
        /// <summary>
        /// 节点的类型代码
        /// 推荐用户根据业务定义一套节点种类的枚举，通过强制转换来利用此属性存储节点的类型
        /// </summary>
        public int Type;

        /// <summary>
        /// 节点所指代物的ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 子节点初始化标记
        /// </summary>
        public bool ChildInitFlag = false;

        /// <summary>
        /// 标记：用于存储其它用户数据
        /// 推荐用户用TreeNode.Tag来存储扩展属性对象，若有其它数据，则可以使用TreeNodeExtendAttribute.Tag来存储
        /// </summary>
        public object Tag;

        /// <summary>
        /// 单击节点前执行的操作
        /// </summary>
        public TreeViewCancelEventHandler OnBeforeClick;

        /// <summary>
        /// 构造函数
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
    /// 单击树节点时执行的操作的委托
    /// </summary>
    /// <param name="Node">所单击的节点</param>
    /// <returns></returns>
    //public delegate int TreeNodeBeforeClickedEventHandler(ExTreeNode Node);
}
