/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：PowerSupplyModel.cs

 * 说明：此文件包含图相关类型及部分图算法

 * 作者：叶道全
 
 * 时间：2009年2月17日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// 抽象图结点，与Graphic配合使用
    /// </summary>
    public class GraphicNode : ICloneable
    {
        #region 数据
        //标签
        private int _Tag;

        //遍历标记
        int _TraversalFlag;

        //用户数据
        private object _UserObject;

        //节点所连边的集合
        List<GraphicEdge> _Edges;

        #endregion

        #region 属性

        /// <summary>
        /// 关键字
        /// </summary>
        public object UserObject
        {
            get { return _UserObject; }
            set { _UserObject = value; }
        }

        /// <summary>
        /// 用户数据
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// 遍历标记
        /// </summary>
        public int TraversalFlag
        {
            get { return _TraversalFlag; }
            set { _TraversalFlag = value; }
        }

        /// <summary>
        /// 节点所连边的集合
        /// </summary>
        public List<GraphicEdge> Edges
        {
            get
            {
                if (_Edges == null)
                    _Edges = new List<GraphicEdge>(3);
                return _Edges;
            }
            set
            {
                _Edges.Clear();
                _Edges.AddRange(value);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GraphicNode()
        {
            _Edges = new List<GraphicEdge>(3);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="EdgeCount">边集的容量（预期的边数）</param>
        public GraphicNode(int EdgeCount)
        {
            _Edges = new List<GraphicEdge>(EdgeCount);
        }

        /// <summary>
        /// 拷贝构造函数，不会将新对象与原对象关联的边对象进行关联。
        /// </summary>
        /// <param name="Other">来源对象</param>
        /// <param name="CopyUserObject">是否拷贝用户数据</param>
        public GraphicNode(GraphicNode Other, bool CopyUserObject)
        {
            this._Edges = new List<GraphicEdge>(Other._Edges.Count);
            if (CopyUserObject) this._UserObject = Other._UserObject;
            this._Tag = Other._Tag;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public GraphicNode(object userData)
        {
            this.UserObject = userData;
        }

        /// <summary>
        /// 创建图节点的一个拷贝，拷贝用户数据（值或引用），但不会将新对象与原对象关联的边对象进行关联。
        /// </summary>
        /// <returns>新的拷贝对象。</returns>
        public object Clone()
        {
            return new GraphicNode(this, true);
        }

        /// <summary>
        /// 创建图节点的一个拷贝，但不会将新对象与原对象关联的边对象进行关联。
        /// </summary>
        /// <param name="CopyUserObject">是否拷贝用户数据。</param>
        /// <returns>新的拷贝对象。</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new GraphicNode(this, CopyUserObject);
        }

        #endregion
    }

    /// <summary>
    /// 抽象图边，与Graphic配合使用
    /// </summary>
    public class GraphicEdge : ICloneable
    {
        #region 数据

        //标签
        private int _Tag;

        //遍历标记
        private int _TraversalFlag;

        //用户数据
        private object _UserObject;

        //边的端点的集合
        GraphicNode[] _Nodes = new GraphicNode[2];

        #endregion

        #region 属性

        /// <summary>
        /// 关键字
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// 遍历标记
        /// </summary>
        public int TraversalFlag
        {
            get { return _TraversalFlag; }
            set { _TraversalFlag = value; }
        }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object UserObject
        {
            get { return _UserObject; }
            set { _UserObject = value; }
        }

        /// <summary>
        /// 节点所连边的集合
        /// </summary>
        public GraphicNode[] Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GraphicEdge()
        {

        }

        /// <summary>
        /// 拷贝构造函数，但不会将新对象与原对象关联的节点对象进行关联。
        /// </summary>
        /// <param name="Other">来源对象</param>
        /// <param name="CopyUserObject">是否拷贝用户数据</param>
        public GraphicEdge(GraphicEdge Other, bool CopyUserObject)
        {
            this._Tag = Other._Tag;
            if (CopyUserObject) this._UserObject = Other._UserObject;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="node1">边所连端点</param>
        /// <param name="node2">边所连端点</param>
        /// <param name="userData">边关联的用户数据</param>
        public GraphicEdge(GraphicNode node1, GraphicNode node2, object userData)
        {
            Nodes[0] = node1;
            Nodes[1] = node2;
            UserObject = userData;
        }

        /// <summary>
        /// 创建图节点的一个拷贝，此方法将复制用户数据（值或引用），但不会将新对象与原对象关联的节点对象进行关联。
        /// </summary>
        /// <returns>新的拷贝对象。</returns>
        public object Clone()
        {
            return new GraphicEdge(this, true);
        }

        /// <summary>
        /// 创建图边对象的一个拷贝。但不会将新对象与原对象关联的节点对象进行关联。
        /// </summary>
        /// <param name="CopyUserObject">是否拷贝用户数据。</param>
        /// <returns>新的拷贝对象。</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new GraphicEdge(this, CopyUserObject);
        }

        /// <summary>
        /// 求处在边另一端的节点，请保证传入的节点连到了该边。
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>返回另一端的节点</returns>
        public GraphicNode OtherNode(GraphicNode node)
        {
            return (object.ReferenceEquals(node, Nodes[0])) ? Nodes[1] : Nodes[0];
        }

        #endregion
    }

    /// <summary>
    /// 抽象图模型，轻量级，支持拷贝。此图中的所有方法均不保证多线程安全。
    /// </summary>
    public class Graphic : ICloneable
    {
        #region 类型

        /// <summary>
        /// 对遍历过程中经过的节点执行的方法原型。
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="edge">到达当前节点途经的边</param>
        /// <param name="counter">计数器。遍历过程每经过一个节点时递增。计数器生成规则可在遍历方法中指定。</param>
        /// 关于此原型的参数列表为何不包含遍历的起始节点的信息的问题：
        /// 　　调用方在调用图类中的遍历接口时，必须指定起点。即调用方知道起点。
        /// 　　此原型实际关联的代码由遍历接口的调用方提供并在调用方的代码段中执行。
        /// 　　因此此原型无需提供起点信息。
        /// 注：若需要知道节点在遍历过程中的顺序，请访问节点的TraversalFlag标识，从1开始。
        /// 警告：执行遍历操作时，请注意无论如何不要修改修改节点与边的Tag标记以及TraversalFlag。
        public delegate void TraversalOperate(GraphicNode node, List<GraphicEdge> path);

        /// <summary>
        /// 控制遍历过程的方法原型。
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="edge">到达当前节点途经的边</param>
        /// <param name="counter">计数器。遍历过程每经过一个节点时递增。计数器生成规则可在遍历方法中指定。</param>
        /// <returns>枚举:Normal:继续;Ignore:不从此节点探索相邻节点;Stop:终止遍历。</returns>
        /// 注：若需要知道节点在遍历过程中的顺序，请访问节点的TraversalFlag标识，从1开始。
        /// 警告：执行控制操作时，请注意无论如何不要修改修改节点与边的Tag标记。
        public delegate TraversalControlEnum TraversalControl(GraphicNode node, List<GraphicEdge> path);

        /// <summary>
        /// 枚举:遍历过程的可选控制。Normal:继续;Ignore:不从此节点探索相邻节点;Stop:终止遍历。
        /// </summary>
        public enum TraversalControlEnum { Normal, IgnoreChild, Stop }

        /// <summary>
        /// 枚举:可用的遍历方式。DFS:深度优先搜索;BFS:广度优先搜索
        /// </summary>
        public enum TraversalModeEnum { DFS, BFS };

        #endregion

        #region 边数据、顶点数据

        //图中的顶点集
        private List<GraphicNode> _Nodes = new List<GraphicNode>(10);

        //图中的边集
        private List<GraphicEdge> _Edges = new List<GraphicEdge>(15);

        #endregion

        #region 分析数据

        //图中的回路
        private List<List<GraphicEdge>> _Loops = new List<List<GraphicEdge>>(3);    //回路集合        
        private bool _LoopAnalysed = false;                                         //回路是否已分析标志

        private List<GraphicEdge> _LoopEdges = new List<GraphicEdge>(0);            //图中的构成回路的边集
        private bool _LoopEdgeAnalysed = false;                                     //成环支路是否已分析标志

        //图中的非环路径
        private List<Object>[] _SinglePath;
        private bool _SinglePathAnalysed = false;

        //分析数据是否有效
        private bool AnalysisDataValid = false;

        #endregion

        #region 遍历数据
        private int TraversalInnerCounter;  //内置计数器：用于记录已遍历的节点数

        private TraversalOperate TraversalOp;   //对遍历经过的节点执行的操作
        private TraversalControl TraversalCtrl; //遍历过程使用的控制器

        private List<GraphicEdge> TraversalPath; //遍历路径

        private bool TraversalMakeWholePath;      //指示遍历时向用户操作接口和控制接口传入完整路径还是最后一条边

        #endregion

        #region 属性

        /// <summary>
        /// 图形中的节点集合
        /// </summary>
        public List<GraphicNode> Nodes
        {
            get { return _Nodes; }
        }

        /// <summary>
        /// 图中的边集合
        /// </summary>
        public List<GraphicEdge> Edges
        {
            get { return _Edges; }
        }

        /// <summary>
        /// 图中的回路集合
        /// </summary>
        public List<List<GraphicEdge>> Loops
        {
            get
            {
                if (!LoopAnalysed) LoopAnalysis();
                return _Loops;
            }
        }

        /// <summary>
        /// 回路信息的有效性标记
        /// </summary>
        public bool LoopAnalysed
        {
            get { return _LoopAnalysed; }
            set { _LoopAnalysed = value; }
        }

        public List<GraphicEdge> LoopEdges
        {
            get
            {
                if (!LoopEdgeAnalysed) LoopEdgeAnalysis();
                return _LoopEdges;
            }
        }

        /// <summary>
        /// 回路边集的有效性标记
        /// </summary>
        public bool LoopEdgeAnalysed
        {
            get { return _LoopEdgeAnalysed; }
            set { _LoopEdgeAnalysed = value; }
        }

        /// <summary>
        /// 图中的单端路径，一个二元数组，元素1为单端路径中节点集合，元素2为单端路径中的边集合
        /// </summary>
        public List<Object>[] SinglePath
        {
            get
            {
                if (!_SinglePathAnalysed) SinglePathAnalysis();
                return _SinglePath;
            }
        }

        /// <summary>
        /// 无环路径信息的有效性标记
        /// </summary>
        public bool SinglePathAnalysed
        {
            get { return _SinglePathAnalysed; }
            set { _SinglePathAnalysed = value; }
        }

        #endregion

        #region 方法

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Graphic()
        {
            _Nodes = new List<GraphicNode>(10);
            _Edges = new List<GraphicEdge>(15);
        }

        /// <summary>
        /// 拷贝构造函数：将复制图中所有节点、边数据，并将复制节点与边上的用户数据的引用（浅拷贝）。
        /// </summary>
        /// <param name="Other"></param>
        public Graphic(Graphic Other)
        {
            GraphicEdge edge;
            GraphicNode node;

            //对原图中节点与边进行编号
            for (int index = 0; index < Other.Nodes.Count; ++index) Other.Nodes[index].Tag = index;
            for (int index = 0; index < Other.Edges.Count; ++index) Other.Edges[index].Tag = index;

            //初始化节点与边集合
            _Nodes = new List<GraphicNode>(Other.Nodes.Count);
            _Edges = new List<GraphicEdge>(Other.Edges.Count);

            //初始化节点与边
            for (int index = 0; index < Other.Nodes.Count; ++index)
            {
                this.Nodes.Add((GraphicNode)(Other.Nodes[index].Clone()));
            }
            for (int index = 0; index < Other.Edges.Count; ++index)
            {
                this.Edges.Add((GraphicEdge)(Other.Edges[index].Clone()));
            }

            //构造新对象节点与边的链接关系
            foreach (GraphicEdge GEdge in Other.Edges)
            {
                edge = this.Edges[GEdge.Tag];                       //找出新对象中与GEdge相对应的边
                edge.Nodes[0] = this.Nodes[GEdge.Nodes[0].Tag];     //新新对象中端点集链接到相应的节点
                edge.Nodes[1] = this.Nodes[GEdge.Nodes[1].Tag];
                edge.Nodes[0].Edges.Add(edge);                      //更新端节点的边列表
                edge.Nodes[1].Edges.Add(edge);
            }

        }

        #endregion

        #region 复制函数

        /// <summary>
        /// 创建图的一个拷贝，此拷贝将创建图中所有节点、边、分析数据的副本，并复制节点与边关联的用户数据的引用。
        /// </summary>
        /// <returns>新的拷贝对象</returns>
        public object Clone()
        {
            return new Graphic(this);
        }

        #endregion

        /// <summary>
        /// 初始化节点的标记
        /// </summary>
        /// <param name="InitValue">初始的Tag值</param>
        /// <param name="Step">不同节点间的Tag差值</param>
        /// <returns>返回最后一个未使用的Tag值</returns>
        public int InitNodeTag(int InitValue, int Step)
        {
            foreach (GraphicNode node in Nodes)
            {
                node.Tag = InitValue;
                InitValue += Step;
            }
            return InitValue;
        }

        /// <summary>
        /// 初始化节点的标记
        /// </summary>
        /// <param name="InitValue">初始的Tag值</param>
        /// <param name="Step">不同节点间的Tag差值</param>
        /// <returns>返回最后一个未使用的Tag值</returns>
        public int InitEdgeTag(int InitValue, int Step)
        {
            foreach (GraphicEdge edge in Edges)
            {
                edge.Tag = InitValue;
                InitValue += Step;
            }
            return InitValue;
        }

        /// <summary>
        /// 设置分析数据的有效性状态
        /// </summary>
        /// <param name="invalidFlag">目标状态</param>
        private void SetValidFlag(bool ValidFlag)
        {
            LoopAnalysed = ValidFlag;
            SinglePathAnalysed = ValidFlag;
            LoopEdgeAnalysed = ValidFlag;
        }

        /// <summary>
        /// 清除图形
        /// </summary>
        public void ClearGraphic()
        {
            this.Nodes.Clear();
            this.Edges.Clear();

            if (this._SinglePath != null)
            {
                this.SinglePath[0].Clear();
                this.SinglePath[1].Clear();
            }

            if (this._Loops != null) this._Loops.Clear();

            this.SetValidFlag(false);
        }

        #region 遍历函数

        /// <summary>
        /// 对图中节点进行遍历
        /// </summary>
        /// <param name="traversalMode">遍历方式</param>
        /// <param name="startNode">起始节点</param>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="ctrl">控制，若用户无需控制则请传入空值</param>
        /// <param name="getWholePath">true:遍历函数将向遍历操作接口和控制接口传入从起点至当前节点的完整路径;false:遍历函数将向遍历操作接口和控制接口传入仅包含从上一个节点至当前节点的边的路径。对于深度优先搜索，生成完整路径的代价几乎可以忽略；对于广度优先搜索，其代价与路径长度有关。</param>
        public void TraversalNode(TraversalModeEnum traversalMode, GraphicNode startNode, TraversalOperate op, TraversalControl ctrl, bool getWholePath)
        {
            //初始化遍历数据
            this.TraversalCtrl = ctrl;
            this.TraversalOp = op;
            this.TraversalInnerCounter = 1;
            this.TraversalMakeWholePath = getWholePath;

            //初始化路径
            if (this.TraversalPath == null) this.TraversalPath = new List<GraphicEdge>(this.Nodes.Count);

            //清除遍历标记
            ClearTraversalFlag();

            //将路径中数据清空
            TraversalPath.Clear();

            //选择合适的遍历算法
            switch (traversalMode)
            {
                case TraversalModeEnum.DFS:
                    if (ctrl == null)
                    {
                        DFSNodeNoUserCtrl(startNode, null);
                    }
                    else
                    {
                        DFSNodeWithUserCtrl(startNode, null);
                    }
                    break;
                case TraversalModeEnum.BFS:
                    if (ctrl == null)
                    {
                        BFSNodeNoUserCtrl(startNode);
                    }
                    else
                    {
                        BFSNodeWithUserCtrl(startNode);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 从指定节点开始进行深度优先遍历。
        /// </summary>
        /// <param name="StartNode">起始节点</param>
        /// <param name="PassEdge">遍历到当前节点时经过的边</param>
        /// <returns>返回值用于告之遍历是否终止</returns>
        private bool DFSNodeWithUserCtrl(GraphicNode node, GraphicEdge edge)
        {
            bool Result = true;
            GraphicNode NeighborNode; //相邻节点

            //对节点执行操作并标记            
            node.TraversalFlag = TraversalInnerCounter;

            //标记边
            if (edge != null)
            {
                edge.TraversalFlag = TraversalInnerCounter;

                if (!TraversalMakeWholePath) this.TraversalPath.Clear();

                this.TraversalPath.Add(edge);                   //将边加入路径
            }

            //调用用户操作
            TraversalOp(node, this.TraversalPath);

            //判断是否遍历完毕并更新遍历计数器
            if (TraversalInnerCounter == this.Nodes.Count) return false;
            ++TraversalInnerCounter;

            //根据控制器决定是否遍历相邻节点
            switch (TraversalCtrl(node, this.TraversalPath))
            {
                case TraversalControlEnum.Stop:
                    Result = false;
                    break;
                case TraversalControlEnum.Normal:       //遍历子节点
                    foreach (GraphicEdge LinkedEdge in node.Edges)
                    {
                        if (LinkedEdge.TraversalFlag > -1) continue; //不搜索已经遍历过的边

                        NeighborNode = LinkedEdge.OtherNode(node);

                        if (NeighborNode.TraversalFlag > -1) continue;   //不搜索已经遍历过的节点

                        if (!DFSNodeWithUserCtrl(NeighborNode, LinkedEdge))     //若有子节点要终止遍历，则不对剩余节点进行搜索，并返回false告之上一层递归。
                        {
                            Result = false;
                            break;
                        }
                    }
                    break;
                case TraversalControlEnum.IgnoreChild:
                    break;
                default:
                    Result = false;
                    break;
            }

            //将边从路径上删除，然后回返上一节点
            if (this.TraversalPath.Count > 0) this.TraversalPath.RemoveAt(this.TraversalPath.Count - 1);

            return Result;
        }

        /// <summary>
        /// 从指定节点开始进行广度优先遍历。
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="edge">遍历到当前节点时经过的边</param>
        private void BFSNodeWithUserCtrl(GraphicNode node)
        {
            List<GraphicNode> NodeList = new List<GraphicNode>(Nodes.Count);
            List<GraphicEdge> EdgeList = new List<GraphicEdge>(Nodes.Count);

            int idx = 0;

            GraphicEdge edge = null;
            GraphicNode NeighborNode;
            bool StopSearchFlag = false;

            //标记始节点
            node.TraversalFlag = TraversalInnerCounter;

            //将始节点压栈
            NodeList.Add(node);
            EdgeList.Add(edge);

            do
            {
                //从队列中取节点及边
                node = NodeList[idx];
                edge = EdgeList[idx];

                //求路径     
                this.TraversalPath.Clear();
                if (TraversalMakeWholePath)
                {
                    while (edge != null)
                    {
                        this.TraversalPath.Add(edge);
                        //节点的TraversalFlag即代表了节点间的遍历顺序，同时也代表了节点与边在List中的索引
                        edge = EdgeList[Math.Min(edge.Nodes[0].TraversalFlag, edge.Nodes[1].TraversalFlag) - 1];
                    }
                    this.TraversalPath.Reverse();
                }
                else
                    if (edge != null) this.TraversalPath.Add(edge);

                //对节点执行遍历操作
                TraversalOp(node, this.TraversalPath);

                //判断是否无需再搜索相邻节点
                if (StopSearchFlag) continue;

                //根据控制器确定是否遍历相邻节点
                switch (TraversalCtrl(node, this.TraversalPath))
                {
                    case TraversalControlEnum.Stop:
                        StopSearchFlag = true;
                        break;
                    case TraversalControlEnum.Normal:   //遍历相邻节点
                        foreach (GraphicEdge LinkedEdge in node.Edges)
                        {
                            if (LinkedEdge.TraversalFlag > -1) continue;
                            NeighborNode = LinkedEdge.OtherNode(node);
                            if (NeighborNode.TraversalFlag > -1) continue;

                            ++TraversalInnerCounter;

                            NeighborNode.TraversalFlag = TraversalInnerCounter;
                            LinkedEdge.TraversalFlag = TraversalInnerCounter;

                            NodeList.Add(NeighborNode);
                            EdgeList.Add(LinkedEdge);

                            if (TraversalInnerCounter == Nodes.Count)
                            {
                                StopSearchFlag = true;
                                break;
                            }
                        }
                        break;
                    case TraversalControlEnum.IgnoreChild:
                        break;
                    default:
                        StopSearchFlag = true;
                        break;

                }

            } while (++idx < NodeList.Count);
        }

        /// <summary>
        /// 从指定节点开始进行深度优先遍历。
        /// </summary>
        /// <param name="StartNode">起始节点</param>
        /// <param name="PassEdge">遍历到当前节点时经过的边</param>
        /// <returns>返回值用于告之遍历是否终止</returns>
        private bool DFSNodeNoUserCtrl(GraphicNode node, GraphicEdge edge)
        {
            GraphicNode NeighborNode; //相邻节点

            //对节点执行操作并标记
            node.TraversalFlag = TraversalInnerCounter;
            if (edge != null)
            {
                edge.TraversalFlag = TraversalInnerCounter;

                if (!TraversalMakeWholePath) this.TraversalPath.Clear();

                this.TraversalPath.Add(edge);               //将边加入路径                    
            }

            TraversalOp(node, this.TraversalPath);

            //更新遍历计数器并判断是否遍历完毕,由于停止遍历后将没有任何地方需要使用TraversalPath，故在此处可以直接返回
            if (TraversalInnerCounter == this.Nodes.Count) return false;

            ++TraversalInnerCounter;

            //遍历子节点
            foreach (GraphicEdge LinkedEdge in node.Edges)
            {
                if (LinkedEdge.TraversalFlag > -1) continue; //忽略已搜索过的路径

                NeighborNode = LinkedEdge.OtherNode(node);

                if (NeighborNode.TraversalFlag > -1) continue;   //忽略已搜索过的节点

                if (!DFSNodeNoUserCtrl(NeighborNode, LinkedEdge))
                {
                    //由于停止遍历后将没有任何地方需要使用TraversalPath，故在此处可以直接返回
                    return false;
                }
            }

            //将边从路径中移除
            if (this.TraversalPath.Count > 0) this.TraversalPath.RemoveAt(this.TraversalPath.Count - 1);

            return true;
        }

        /// <summary>
        /// 从指定节点开始进行广度优先遍历。
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="edge">遍历到当前节点时经过的边</param>
        /// <returns>返回值用于告之遍历是否终止</returns>
        private void BFSNodeNoUserCtrl(GraphicNode node)
        {
            List<GraphicNode> NodeList = new List<GraphicNode>(Nodes.Count);
            List<GraphicEdge> EdgeList = new List<GraphicEdge>(Nodes.Count);

            int idx = 0; //遍历索引器

            bool StopSearchFlag = false;

            GraphicEdge edge = null;
            GraphicNode NeighborNode;

            node.TraversalFlag = TraversalInnerCounter;

            NodeList.Add(node);
            EdgeList.Add(edge);

            do
            {
                //从队列中取节点及边
                node = NodeList[idx];
                edge = EdgeList[idx];

                //求路径
                this.TraversalPath.Clear();
                if (this.TraversalMakeWholePath)
                {
                    while (edge != null)
                    {
                        this.TraversalPath.Add(edge);
                        //节点的TraversalFlag即代表了节点间的遍历顺序，同时也代表了节点与边在List中的索引
                        edge = EdgeList[Math.Min(edge.Nodes[0].TraversalFlag, edge.Nodes[1].TraversalFlag) - 1];
                    }
                    this.TraversalPath.Reverse();
                }
                else
                    if (edge != null) this.TraversalPath.Add(edge);

                //对节点执行遍历操作
                TraversalOp(node, this.TraversalPath);

                //判断是否停止搜索相邻结点
                if (StopSearchFlag) continue;

                //搜索相邻节点
                foreach (GraphicEdge LinkedEdge in node.Edges)
                {
                    if (LinkedEdge.TraversalFlag > -1) continue;
                    NeighborNode = LinkedEdge.OtherNode(node);
                    if (NeighborNode.TraversalFlag > -1) continue;

                    ++TraversalInnerCounter;

                    NeighborNode.TraversalFlag = TraversalInnerCounter;
                    LinkedEdge.TraversalFlag = TraversalInnerCounter;

                    NodeList.Add(NeighborNode);
                    EdgeList.Add(LinkedEdge);

                    if (TraversalInnerCounter == this.Nodes.Count)
                    {
                        StopSearchFlag = true;
                        break;
                    }
                }

            } while (++idx < NodeList.Count);
        }

        /// <summary>
        /// 清除遍历标记
        /// </summary>
        private void ClearTraversalFlag()
        {
            foreach (GraphicNode node in Nodes) node.TraversalFlag = -1;
            foreach (GraphicEdge edge in Edges) edge.TraversalFlag = -1;
        }

        #endregion

        #region 图算法

        /// <summary>
        /// 分析系统中的回路
        /// </summary>
        /// <returns>1:成功; 0:用户取消; -1:错误</returns>
        private int LoopAnalysis()
        {
            TraversalOperate op;
            TraversalControl ctrl;

            //一个环
            List<GraphicEdge> loop;

            //用于存储遍历过程中未经过的边
            List<GraphicEdge> freeEdges = new List<GraphicEdge>(Math.Max(this.Edges.Count - this.Nodes.Count + 1, 0));

            //判断是否有剩余节点
            if (SinglePath[0].Count == Nodes.Count)
            {
                _Loops = new List<List<GraphicEdge>>(0);
                return 1;
            }

            //将图中的非环路径断开
            foreach (GraphicEdge singlePathEdge in SinglePath[1])
            {
                singlePathEdge.Nodes[0].Edges.Remove(singlePathEdge);
                singlePathEdge.Nodes[1].Edges.Remove(singlePathEdge);
                this.Edges.Remove(singlePathEdge);
            }
            //标记非环路径中的节点
            foreach (GraphicNode node in Nodes) node.Tag = 0;
            foreach (GraphicNode singlePathNode in SinglePath[0]) singlePathNode.Tag = 1;

            //从环路中的节点对图进行遍历
            op = delegate(GraphicNode node, List<GraphicEdge> path) { };
            foreach (GraphicNode singlePathNode in this.Nodes)
            {
                if (singlePathNode.Tag == 0) //寻找环路中的节点
                {
                    TraversalNode(TraversalModeEnum.BFS, singlePathNode, op, null, false);
                    break;
                }
            }

            //保存未遍历的边
            foreach (GraphicEdge edge in this.Edges)
            {
                if (edge.TraversalFlag == -1) freeEdges.Add(edge);
            }

            //根据freeEdges初始化环列表：在遍历过程中每条未经过的边与经过的边必构造一个环
            if (_Loops == null)
                _Loops = new List<List<GraphicEdge>>(freeEdges.Count);
            else
            {
                _Loops.Clear();
                _Loops.Capacity = freeEdges.Count;
            }


            //求删除任一条未遍历过的边后，该边两端点间的最短路径
            foreach (GraphicEdge fEdge in freeEdges)
            {
                //将边断开
                fEdge.Nodes[0].Edges.Remove(fEdge);
                fEdge.Nodes[1].Edges.Remove(fEdge);

                //从边的一端开始对图进行广度优先搜索遍历，遇到另一端点时停止，其路径与该边构成一个最短环。
                ctrl = delegate(GraphicNode node, List<GraphicEdge> path) //定义生成树控制器，怎么把这个放到foreach外呢？
                {
                    if (object.ReferenceEquals(node, fEdge.Nodes[1]))
                    {
                        loop = new List<GraphicEdge>(path.Count + 1);
                        loop.InsertRange(0, path);
                        loop.Add(fEdge);
                        _Loops.Add(loop);
                        return TraversalControlEnum.Stop;
                    }
                    return TraversalControlEnum.Normal;
                };
                TraversalNode(TraversalModeEnum.BFS, fEdge.Nodes[0], op, ctrl, true);

                //恢复边
                fEdge.Nodes[0].Edges.Add(fEdge);
                fEdge.Nodes[1].Edges.Add(fEdge);
            }

            //恢复非环路径
            foreach (GraphicEdge singlePathEdge in SinglePath[1])
            {
                singlePathEdge.Nodes[0].Edges.Add(singlePathEdge);
                singlePathEdge.Nodes[1].Edges.Add(singlePathEdge);
                this.Edges.Add(singlePathEdge);
            }

            _LoopAnalysed = true;
            return 1;
        }

        /// <summary>
        /// 求图中形成环的边的集合
        /// </summary>
        /// <returns></returns>
        private int LoopEdgeAnalysis()
        {
            XTreeNode.TraversalOperate TreeOp;

            Graphic.TraversalControl GraphicCtrl;

            int GenerationTreeNodeCount = 0,idx;                    //生成树中的节点数

            XTreeNode GenerationTree = null;　                      //图的生成树

            List<GraphicEdge> freeEdges;                            //用于存储遍历过程中未经过的边

            List<List<int>> NodeLinksList = new List<List<int>>(10);//此图用于存储每个节点所关联的FreeEdges中的边，其中值为FreeEdges中的边在FreeEdges中的索引，节点的Tag值对应其在NodeLinksList中索引


            XTreeNode[,] TreeNodePairs;                             //FreeEdges两端点对应的树节点集合

            //校验
            _LoopEdges.Clear();
            if (this.Nodes.Count <= SinglePath[0].Count) return 1;  //不存在非单端路径时，则表示图中无环

            #region 构造一棵不包含单端支路中节点的生成树

            //将图中节点与边标记为0
            foreach (GraphicNode node in Nodes) node.Tag = 0;
            foreach (GraphicEdge edge in Edges) edge.Tag = 0;

            //将单端路径中的节点与边标记为-1
            foreach (GraphicNode singlePathNode in SinglePath[0]) singlePathNode.Tag = -1;
            foreach (GraphicEdge singlePathEdge in SinglePath[1]) singlePathEdge.Tag = -1;

            //定义用于生成树的控制器：使在搜索图时，遇到单端路径中节点时，不搜索其相邻节点
            GraphicCtrl = delegate(GraphicNode Node,List<GraphicEdge> Path)
            {
                return (Node.Tag == -1)?Graphic.TraversalControlEnum.IgnoreChild:Graphic.TraversalControlEnum.Normal;
            };

            #region 从非单端支路中的节点开始创建一个构造树
            foreach (GraphicNode node in this.Nodes)
            {
                if (node.Tag == 0) //寻找环路中的节点
                {
                    GenerationTree = GetGenerationTree(node, TraversalModeEnum.BFS, GraphicCtrl, true);
                    break;
                }
            }
            #endregion

            #endregion

            #region 将生成树中的边的Tag置1，并对树中节点进行计数

            TreeOp = delegate(XTreeNode node, int Counter)
            {
                if (node.Father != null) ((GraphicEdge)node.EdgeUserObject).Tag = 1;
                GenerationTreeNodeCount = Counter;
            };
            GenerationTree.Traversal(XTreeNode.TraversalModeEnum.Pre, TreeOp, 1, 1);

            #endregion

            #region 初始化集合容量

            _LoopEdges.Capacity = this.Edges.Count - this.SinglePath[1].Count;

            freeEdges = new List<GraphicEdge>(Math.Max(this.Edges.Count - this.Nodes.Count + 1,0));

            #endregion

            #region 取得所有未遍历的边（将其标记为2,代表其构成环）

            foreach (GraphicEdge edge in this.Edges)
            {
                if (edge.Tag == 0)
                {
                    edge.Tag = 2;
                    freeEdges.Add(edge);
                    foreach (GraphicNode node in edge.Nodes)
                    {
                        if (node.Tag <= 0)
                        {
                            NodeLinksList.Add(new List<int>(3));
                            node.Tag = NodeLinksList.Count;
                        }
                        NodeLinksList[node.Tag - 1].Add(freeEdges.Count - 1);
                    }
                }
            }
            TreeNodePairs = new XTreeNode[freeEdges.Count, 2];

            #endregion

            #region 对生成树进行遍历，根据将所关联图节点的Tag值将其加入TreeNodePairs

            TreeOp = delegate(XTreeNode Node, int Counter)
            {
                int tag = ((GraphicNode)Node.NodeUserObject).Tag;  //FreeEdges两端节点的Tag从1开始编号，故要减1
                if ((tag) > 0)
                {
                    foreach (int row in NodeLinksList[tag - 1])
                    {
                        idx = (TreeNodePairs[row, 0] == null) ? 0 : 1; //判断TreeNodeParis[row]维中第一格是否已占用
                        TreeNodePairs[row, idx] = Node;
                    }
                }
            };
            GenerationTree.Traversal(XTreeNode.TraversalModeEnum.Pre, TreeOp, 0, 0);

            #endregion

            #region 遍历每对树节点，将其间路径所对应的图边的Tag标记置为2,代表相应的边构成环

            TreeOp = delegate(XTreeNode Node, int Counter)
            {
                if(Node.EdgeUserObject != null) ((GraphicEdge)Node.EdgeUserObject).Tag = 2;
            };
            for (idx = 0; idx < TreeNodePairs.GetLength(0); idx++)
            {
                GenerationTree.Traversal(TreeNodePairs[idx,0], TreeNodePairs[idx,1], true, TreeOp);
            }

            #endregion

            #region 图中所有标记为2的边构成环

            foreach (GraphicEdge edge in this.Edges) if (edge.Tag == 2) _LoopEdges.Add(edge);

            #endregion

            LoopEdgeAnalysed = true;

            return 1;
        }

        /// <summary>
        /// 判断指定的区域是否连通
        /// </summary>
        /// <param name="Area">目标区域</param>
        /// <param name="BeginNode">节点</param>
        /// <param name="AloneNodes">所有与BeginNode不在同一连通分量中的节点的集合</param>
        /// <returns></returns>
        public int ConnexAnalysis(List<GraphicNode> Area, GraphicNode BeginNode, List<GraphicNode> AloneNodes)
        {
            int Count = 0; //对BeginNode所在连通区域中的节点进行计数

            foreach (GraphicNode node in this.Nodes) node.Tag = 2;
            foreach (GraphicNode node in Area) node.Tag = 0;

            //从起点开始遍历，将遍历到的所有节点的Tag置为1
            TraversalOperate op = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                if (node.Tag != 2)
                {
                    ++Count;
                    node.Tag = 1;
                }
            };
            TraversalControl ctrl = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                if (node.Tag == 2) return TraversalControlEnum.IgnoreChild;
                return TraversalControlEnum.Normal;
            };

            TraversalNode(TraversalModeEnum.DFS, BeginNode, op, ctrl,false);

            //将Area中所有标记为0的节点加入AloneNodes
            AloneNodes.Clear();
            AloneNodes.Capacity = Area.Count - Count;
            foreach (GraphicNode node in Area) if (node.Tag == 0) AloneNodes.Add(node);
            if (AloneNodes.Count > 0) return 0;
            return 1;
        }

        /// <summary>
        /// 按指定搜索方式生成一棵生成树
        /// </summary>
        /// <param name="beginNode">起点</param>
        /// <param name="traversalMode">遍历方式</param>
        /// <param name="ctrl">控制器，完全遍历请置空</param>
        /// <param name="linkUserDataToGraphicElement">True:树中节点的UserData将关联到图的节点与边。false:树中节点的UserData关联到图节点和边的UserData</param>
        /// <returns>返回生成树的根节点</returns>
        public XTreeNode GetGenerationTree(GraphicNode startNode, TraversalModeEnum traversalMode, TraversalControl ctrl, bool linkUserDataToGraphicElement)
        {
            List<XTreeNode> treeNodes = new List<XTreeNode>(this.Nodes.Count);
            XTreeNode temp;

            //遍历操作
            TraversalOperate op = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                //生成树节点并添加到树节点列表中
                temp = new XTreeNode(3);
                treeNodes.Add(temp);

                //创建节点关联
                if (path.Count > 0) temp.Father = treeNodes[path[0].OtherNode(node).TraversalFlag - 1];      //TraversalFlag标识了节点的遍历顺序，故通过此标记判断节点对应的树节点在treeNodes中的位置

                //设置UserData
                if (linkUserDataToGraphicElement)
                {
                    temp.NodeUserObject = node;
                    if (path.Count > 0) temp.EdgeUserObject = path[0];
                }
                else
                {
                    temp.NodeUserObject = node.UserObject;
                    if (path.Count > 0) temp.EdgeUserObject = path[0].UserObject;
                }
            };

            //遍历图
            TraversalNode(traversalMode, startNode, op, ctrl, false);

            return treeNodes[0];
        }

        /// <summary>
        /// 得到图中不形成环的路径，暂时要求图连通
        /// </summary>
        /// <returns>返回图中不形成环的节点集合及边集合</returns>
        public int SinglePathAnalysis()
        {
            List<object> SinglePathNodes = new List<object>(Nodes.Count);
            List<object> SinglePathEdges = new List<object>(Edges.Count);

            //将每个节点的Tag值设为其当前连接的边数，设置所有边的Tag标记(表示该边有效)。并将仅与一条边相邻的节点保存到列表中。
            foreach (GraphicNode node in Nodes)
            {
                node.Tag = node.Edges.Count;
                if (node.Tag == 1) SinglePathNodes.Add(node);
            }
            foreach (GraphicEdge edge in Edges) edge.Tag = 1;

            //从列表中所有节点开始遍历：若当前节点的Tag值为1(即仅与一条有效边相连),则将该有效边两端的节点Tag减1，并将该边置无效，再遍历边另一端的节点。直到遇到Tag值大于1的节点时停止。
            //实际的算法为：对遍历经过的每个节点，先判断通过的边是否有效，有效则置无效，并将节点Tag减1，再遍历该节点；若无效，则直接返回。
            TraversalControl ctrl = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                if (path.Count < 1) return TraversalControlEnum.Normal;        //Path为空时为遍历首节点，因为此节点已经在列表中了，故无需加；又因edge参数不存在，亦不需进行设置。所以直接返回即可。
                if (path[0].Tag == 0) return TraversalControlEnum.IgnoreChild;  //若到达此节点通过边无效，则遍历返回。
                path[0].Tag = 0;                                                //置到达此节点的边为无效。为了避免桥拆了，人没过去的情况，只好把将置边无效的代码放在这了。
                SinglePathEdges.Add(path[0]);                                   //将边加入列表
                --node.Tag;                                                  //将当前节点的Tag减1
                if (node.Tag > 1||node.Tag == 0) return TraversalControlEnum.Stop;          //若当前节点还有多条有效边相连，则终止遍历。
                SinglePathNodes.Add(node);                                   //将节点加入列表
                return TraversalControlEnum.Normal;
            };
            TraversalOperate op = delegate(GraphicNode node, List<GraphicEdge> path) { }; //无操作

            for (int index = 0; index < SinglePathNodes.Count; ++index)
            {
                if (((GraphicNode)SinglePathNodes[index]).Tag == 1) TraversalNode(TraversalModeEnum.DFS, (GraphicNode)(SinglePathNodes[index]), op, ctrl, false);
            }
            _SinglePath = new List<object>[] { SinglePathNodes, SinglePathEdges };

            _SinglePathAnalysed = true;
            return 1;
        }

        #endregion

        #region 图形编辑接口

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="userData">目标节点关联的用户数据</param>
        /// <returns></returns>
        public GraphicNode AddNode(GraphicNode node)
        {
            node.Edges.Clear();
            this.Nodes.Add(node);
            this.SetValidFlag(false);
            return node;
        }

        /// <summary>
        /// 增加边，将自动更新相连节点的边信息
        /// </summary>
        /// <param name="node1">边相连的第一个节点数据（图将根据此值查找应将新增的边关联到哪个图节点）</param>
        /// <param name="node2">边相连的第二个节点数据（图将根据此值查找应将新增的边关联到哪个图节点）</param>
        /// <param name="userData">边关联的数据</param>
        /// <returns></returns>
        public GraphicEdge AddEdge(object node1, object node2, object userData)
        {
            GraphicNode[] LinkNodes = new GraphicNode[2];

            int idx = 0;

            //查找第一个节点
            for (; idx < this.Nodes.Count; ++idx)
            {
                if (this.Nodes[idx].UserObject == node1)
                {
                    LinkNodes[0] = this.Nodes[idx];
                    node1 = node2;
                    break;
                }
                else if (this.Nodes[idx].UserObject == node2)
                {
                    LinkNodes[0] = this.Nodes[idx];
                    break;
                }
            }
            if(LinkNodes[0] == null) return null;

            //查找第二个节点
            for (; idx < this.Nodes.Count; ++idx)
            {
                if (this.Nodes[idx].UserObject == node1)
                {
                    LinkNodes[1] = this.Nodes[idx];
                    break;
                }
            }
            if (LinkNodes[0] == null) return null;

            GraphicEdge edge = new GraphicEdge(LinkNodes[0], LinkNodes[1], userData);
            LinkNodes[0].Edges.Add(edge);
            LinkNodes[1].Edges.Add(edge);
            Edges.Add(edge);

            this.SetValidFlag(false);

            return edge;
        }

        /// <summary>
        /// 增加边，将自动更新相连节点的边信息
        /// </summary>
        /// <param name="edge">边对象</param>
        /// <returns>返回是否成功</returns>
        public bool AddEdge(GraphicEdge edge)
        {
            if (edge == null) return false;
            if (!(this.Nodes.Contains(edge.Nodes[0]) && this.Nodes.Contains(edge.Nodes[1]))) return false;
            edge.Nodes[0].Edges.Add(edge);
            edge.Nodes[1].Edges.Add(edge);
            Edges.Add(edge);

            this.SetValidFlag(false);

            return true;
        }

        /// <summary>
        /// 增加边，将自动更新相连节点的边信息，此函数不执行验证，您应确保边数据的合法性：所连节点均在图中
        /// </summary>
        /// <param name="edge">边对象</param>
        /// <returns>返回是否成功</returns>
        public bool AddEdgeNoCheck(GraphicEdge edge)
        {
            edge.Nodes[0].Edges.Add(edge);
            edge.Nodes[1].Edges.Add(edge);
            Edges.Add(edge);

            this.SetValidFlag(false);

            return true;
        }

        /// <summary>
        /// 移除节点，成功时返回true
        /// </summary>
        /// <param name="node">要移除的节点</param>
        /// <returns></returns>
        public bool RemoveNode(GraphicNode node)
        {
            if (Nodes.Contains(node))
            {
                foreach (GraphicEdge edge in node.Edges)
                {
                    edge.OtherNode(node).Edges.Remove(edge);
                    Edges.Remove(edge);
                }
                node.Edges.Clear();
                Nodes.Remove(node);
                this.SetValidFlag(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从图中移除边，成功则返回true
        /// </summary>
        /// <param name="edge">目标</param>
        /// <returns></returns>
        public bool RemoveEdge(GraphicEdge edge)
        {
            if (Edges.Contains(edge))
            {
                edge.Nodes[0].Edges.Remove(edge);
                edge.Nodes[1].Edges.Remove(edge);
                this.Edges.Remove(edge);
                this.SetValidFlag(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除节点，成功时返回true
        /// </summary>
        /// <param name="node">要移除的节点在Nodes中的位置</param>
        /// <returns></returns>
        public bool RemoveNodeAt(int index)
        {
            if (index < 0 || index >= this.Nodes.Count) return false;
            GraphicNode node = this.Nodes[index];

            foreach (GraphicEdge edge in node.Edges)
            {
                edge.OtherNode(node).Edges.Remove(edge);
                Edges.Remove(edge);
            }
            node.Edges.Clear();
            Nodes.RemoveAt(index);
            this.SetValidFlag(false);

            return true;
        }

        /// <summary>
        /// 从图中移除边，成功则返回true
        /// </summary>
        /// <param name="edge">要移除的边在Edges中的位置</param>
        /// <returns></returns>
        public bool RemoveEdgeAt(int index)
        {
            if (index < 0 || index >= this.Edges.Count) return false;
            GraphicEdge edge = this.Edges[index];

            edge.Nodes[0].Edges.Remove(edge);
            edge.Nodes[1].Edges.Remove(edge);
            this.Edges.RemoveAt(index);

            this.SetValidFlag(false);
            return true;
        }

        #endregion        

        #region 图形元素查找接口

        /// <summary>
        /// 根据指定匹配条件查找节点
        /// </summary>
        /// <param name="match">匹配条件</param>
        /// <returns>返回符合条件的第一个节点</returns>
        public GraphicNode GetNode(Predicate<GraphicNode> match)
        {
            foreach (GraphicNode node in Nodes)
                if (match(node)) return node;
            return null;
        }

        /// <summary>
        /// 根据指定匹配条件查找元素
        /// </summary>
        /// <param name="match">匹配条件</param>
        /// <returns>返回符合条件的第一条边</returns>
        public GraphicEdge GetEdge(Predicate<GraphicEdge> match)
        {
            foreach (GraphicEdge edge in Edges)
                if (match(edge)) return edge;
            return null;
        }        

        /// <summary>
        /// 查找具有指定用户数据的节点（根据引用是否相同判断）
        /// </summary>
        /// <param name="UserData">用户数据</param>
        /// <returns></returns>
        public GraphicNode GetNodeByUserData(object UserData)
        {
            foreach (GraphicNode node in this.Nodes) if (object.ReferenceEquals(node.UserObject, UserData)) return node;
            return null;
        }

        /// <summary>
        /// 查找具有指定用户数据的边（根据引用是否相同判断）
        /// </summary>
        /// <param name="UserData">用户数据</param>
        /// <returns></returns>
        public GraphicEdge GetEdgeByUserData(object UserData)
        {
            foreach (GraphicEdge edge in this.Edges) if (object.ReferenceEquals(edge.UserObject, UserData)) return edge;
            return null;
        }

        /// <summary>
        /// 取一个用户数据列表对应的节点列表,但返回的节点列表顺序不会与UserDataList中的数据顺序对应
        /// </summary>
        /// <param name="UserDataList"></param>
        /// <returns></returns>
        public List<GraphicNode> GetNodesByUserDataList(List<object> UserDataList)
        {
            List<GraphicNode> Result;
            List<object> UserDataListCopy = new List<object>(UserDataList);
            int TravelIndex = 0,FindIndex = -1,Count = 0;

            Predicate<object> match = delegate(object obj)
            {
                return object.ReferenceEquals(this.Nodes[TravelIndex].UserObject, obj);
            };

            foreach (GraphicNode node in this.Nodes) node.Tag = 0;

            for(TravelIndex = 0; TravelIndex < this.Nodes.Count; ++TravelIndex)
            {
                FindIndex = UserDataListCopy.FindIndex(match);
                if(FindIndex == -1) continue;

                this.Nodes[TravelIndex].Tag = 1;
                ++Count;
                UserDataListCopy.RemoveAt(FindIndex);
                if(UserDataListCopy.Count < 1) break;
            }

            Result = new List<GraphicNode>(Count);
            foreach (GraphicNode node in this.Nodes) if (node.Tag == 1) Result.Add(node);
            return Result;
        }

        #endregion

        #endregion

        #region 静态方法

        #endregion
    }

    /// <summary>
    /// 抽象树
    /// </summary>
    public class XTreeNode : ICloneable
    {
        #region 类型

        /// <summary>
        /// 对遍历过程中经过的节点执行的方法原型。
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="counter">计数器。遍历过程每经过一个节点时递增。计数器生成规则可在遍历方法中指定。</param>
        public delegate void TraversalOperate(XTreeNode node, int counter);

        /// <summary>
        /// 控制遍历过程的方法原型。
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="counter">计数器。遍历过程每经过一个节点时递增。计数器生成规则可在遍历方法中指定。</param>
        /// <returns>枚举:Normal:继续;IgnoreChild:不搜索子节点;Stop:终止遍历。对于向上遍历，遇到任何非Normal的控制项都将停止。</returns>
        public delegate TraversalControlEnum TraversalControl(XTreeNode node, int counter);

        /// <summary>
        /// 枚举:遍历过程的可选控制。Normal:继续;IgnoreChild:不搜索子节点;Stop:终止遍历。对于向上遍历，遇到任何非Normal的控制项都将停止。
        /// </summary>
        public enum TraversalControlEnum { Normal, IgnoreChild, Stop }

        /// <summary>
        /// 枚举:可用的遍历方式。Pre:先序遍历;Post:后序遍历;Up:特殊方式(遍历从当前节点到根节点的路径);
        /// </summary>
        public enum TraversalModeEnum { Pre, Post, Up };

        #endregion

        #region 数据

        //用户数据
        private int _Tag;

        //子结点集
        private List<XTreeNode> _Sons;

        //父结点
        private XTreeNode _Father;

        //该节点关联的用户数据
        public object _NodeUserObject;

        //节点至父节点的边关联的用户数据
        public object _EdgeUserObject;

        #endregion

        #region 属性

        /// <summary>
        /// 关键字
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// 节点关联的用户数据
        /// </summary>
        public object NodeUserObject
        {
            get { return _NodeUserObject; }
            set { _NodeUserObject = value; }
        }

        /// <summary>
        /// 节点至父节点的边关联的用户数据
        /// </summary>
        public object EdgeUserObject
        {
            get { return _EdgeUserObject; }
            set { _EdgeUserObject = value; }
        }

        /// <summary>
        /// 节点所在树的根节点
        /// </summary>
        public XTreeNode Root
        {
            get
            {
                XTreeNode root = this;
                while (root._Father != null) root = root._Father;
                return root;
            }
        }

        /// <summary>
        /// 节点的子节点列表
        /// </summary>
        public List<XTreeNode> Sons
        {
            get
            {
                return _Sons;
            }
        }

        /// <summary>
        /// 节点的父节点，注意：设置此属性时，将自动将节点添加到父节点的子节点列表中。
        /// </summary>
        public XTreeNode Father
        {
            get
            {
                return _Father;
            }
            set
            {
                if (object.ReferenceEquals(value, _Father)) return;

                if (this._Father != null) this.Father.Sons.Remove(this);
                this._Father = value;

                if (value != null) value.Sons.Add(this);
            }
        }

        #endregion

        #region 方法

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public XTreeNode()
        {
            _Sons = new List<XTreeNode>(2);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="EdgeCount">边集的容量（预期的边数）</param>
        public XTreeNode(int SonCount)
        {
            _Sons = new List<XTreeNode>(SonCount);
        }

        /// <summary>
        /// 创建树节点的一个拷贝，但不与原节点的父子节点关联。
        /// </summary>
        /// <param name="Other">来源对象</param>
        /// <param name="CopyUserObject">是否拷贝用户数据</param>
        public XTreeNode(XTreeNode Other, bool CopyUserObject)
        {
            this._Sons = new List<XTreeNode>(Other._Sons.Count);
            this.Tag = Other.Tag;

            if (CopyUserObject)
            {
                this.NodeUserObject = Other.NodeUserObject;
                this.EdgeUserObject = Other.EdgeUserObject;
            }
        }

        #endregion

        #region 复制函数

        /// <summary>
        /// 创建树节点的一个拷贝，复制原节点的节点数据和边数据信息，但不与原节点的父子节点关联。
        /// </summary>
        /// <returns>新的拷贝对象。</returns>
        public object Clone()
        {
            return new XTreeNode(this, true);
        }

        /// <summary>
        /// 创建图节点的一个拷贝。
        /// </summary>
        /// <param name="CopyTag">是否拷贝用户数据。</param>
        /// <returns>新的拷贝对象。</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new XTreeNode(this, CopyUserObject);
        }

        /// <summary>
        /// 从节点所在树中克隆出一棵以此节点为根节点的子树，该子树各节点保存与原始节点相同的用户数据。
        /// </summary>
        /// <returns></returns>
        public XTreeNode CloneTree()
        {
            int NodeCount = 0;              //子树的节点数
            List<XTreeNode> NewNodeList;

            //对节点进行编号并计数
            TraversalOperate op = delegate(XTreeNode node, int Counter)
            {
                node.Tag = Counter;
                ++NodeCount;
            };

            //对子树中节点按先序遍历顺序进行编号，初始编号为0.在以后把子树中所有节点克隆到NewNodeList中时，其中节点的Tag值将与其在NewNodeList中的索引相同
            this.Traversal(TraversalModeEnum.Pre, op, 0, 1);

            //初始化容器
            NewNodeList = new List<XTreeNode>(NodeCount);

            //对子树中节点进行克隆，并拷贝到NewNodeList，并根据每个节点的父节点的Tag值设置NewNodeList中新节点间的引用关系
            op = delegate(XTreeNode node, int Counter)
            {
                //将节点克隆到NewNodeList
                NewNodeList.Add((XTreeNode)(node.Clone()));
                //对非根节点，设置其父引用
                if (node.Tag != 0) NewNodeList[node.Tag].Father = NewNodeList[node.Father.Tag];
            };

            this.Traversal(TraversalModeEnum.Pre, op, 0, 0);

            return NewNodeList[0];
        }

        #endregion

        /// <summary>
        /// 求两个节点间的路径
        /// </summary>
        /// <param name="BeginNode">起点</param>
        /// <param name="EndNode">终点</param>
        /// <param name="UserPath">返回的路径的形式。true:用户数据列成的路径。false:树节点列成的路径。</param>
        /// <returns>用户数据路径：返回一个包含两个元素的数组，分别为路径中节点关联的节点数据集合、路径中节点关联的边数据集合。树节点路径：返回一个包含单个元素的数组，其元素为一个树节点列表。若两节点不在同一棵树中，此函数返回null。</returns>
        /// 注：如果您想得路径中各条边关联的用户数据，请最好设置UserPath。因为您无法判断一个树节点列表中哪个节点属于两条支路的顶点。
        public static List<Object>[] GetPath(XTreeNode BeginNode, XTreeNode EndNode, bool UserPath)
        {
            //TreeNode.TraversalOperate op = new TraversalOperate(delegate(TreeNode node,int Counter) //OK  

            XTreeNode JoinNode = null;   //交点
            int LBLen = 0;      //左支长度　1.起点至树根节点的路径节点数　2.起点至交点的路径节点数
            int NodeCount = 0;  //节点数　1.记录总的遍历过程中经历的节点数 2.最终路径的节点数

            List<object> NodeList; //起点到根节点的路径中节点信息
            List<object> EdgeList;   //路径经过的边列表

            //参数校验
            if (BeginNode == null || EndNode == null) return null;

            //从起点开始往上遍历时执行的操作
            XTreeNode.TraversalOperate BOp = delegate(XTreeNode node, int Counter)
            {
                node.Tag = Counter; //标记并记数
                ++NodeCount;
            };

            //从末节点开始往上遍历时进行的操作
            XTreeNode.TraversalOperate EOp = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag == 0)   //若节点未标记，则标记并记数
                {
                    node.Tag = Counter;
                    ++NodeCount;
                }
                else                //若节点已标记，则为交点
                {
                    JoinNode = node;
                }
            };

            //从末节点往上遍历时的控制：遇到设置过Tag的节点时停止
            XTreeNode.TraversalControl control = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag > 0) return TraversalControlEnum.Stop;
                return TraversalControlEnum.Normal;
            };

            //清除始末端节点至根节点路径中各节点的Tag
            //TraversalOperate setTag = new TraversalOperate(TreeNode.SetTag);
            BeginNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);
            EndNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);

            //记录起点至根节点的路径并进行标记（Tag从1开始递增）                      
            BeginNode.Traversal(TraversalModeEnum.Up, BOp, 1, 1); //使用递增计数器是为了计算路径长度
            LBLen = NodeCount;

            //从终点往上走，对路经节点进行标记。在遇到设置了Tag的节点（即两条路径交点）时停止。
            EndNode.Traversal(TraversalModeEnum.Up, EOp, control, 1, 0);

            //不在同一棵树中时返回null
            if (JoinNode == null) return null;

            //初始化容器
            NodeCount = NodeCount - (JoinNode.Tag - LBLen);     //此计算后NodeCount表示最终路径的节点数
            LBLen = JoinNode.Tag;                               //此计算后BRLen表示起点至交点路径的节点数
            NodeList = new List<object>(NodeCount + 1);         //交点会重复添加，故要多备一个节点的内存


            //清除交点的Tag
            JoinNode.Tag = 0;

            //遇到Tag被清除的节点时停止
            control = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag == 0) return TraversalControlEnum.Stop;
                return TraversalControlEnum.Normal;
            };

            //将节点加入列表
            TraversalOperate op = delegate(XTreeNode node, int Counter)
            {
                NodeList.Add(node);
            };

            //将起点至交点的路径中各节点加入列表(除交点外)
            BeginNode.Traversal(TraversalModeEnum.Up, op, control, 0, 0);

            //将终点至交点的路径中各节点加入列表(除交点外)
            EndNode.Traversal(TraversalModeEnum.Up, op, control, 0, 0);

            //去掉重复添加的交点
            NodeList.RemoveAt(NodeCount);

            //将终点至交点的路径翻转
            NodeList.Reverse(LBLen - 1, NodeCount - LBLen);

            if (UserPath)
            {
                //初始化边容器
                EdgeList = new List<object>(NodeCount - 1);

                //生成边集(排除交点)
                for (int index = 0; index < LBLen - 1; ++index) EdgeList.Add(((XTreeNode)(NodeList[index])).EdgeUserObject);
                for (int index = LBLen; index < NodeList.Count; ++index) EdgeList.Add(((XTreeNode)(NodeList[index])).EdgeUserObject);

                //将BRNodeList中保存的节点替换为节点中的NodeUserObject
                for (int index = 0; index < NodeList.Count; ++index) NodeList[index] = ((XTreeNode)(NodeList[index])).NodeUserObject;

                return new List<object>[] { NodeList, EdgeList };
            }
            else
            {
                return new List<object>[] { NodeList };
            }
        }

        /// <summary>
        /// 将节点的Tag置为指定值
        /// </summary>
        /// <param name="node">目标节点</param>
        /// <param name="Tag">Tag值</param>
        public static void SetTag(XTreeNode node, int Tag)
        {
            node.Tag = Tag;
        }

        /// <summary>
        /// 改变节点的Tag值。
        /// </summary>
        /// <param name="node">目标节点</param>
        /// <param name="Num">Tag增量</param>
        public static void TagIncrease(XTreeNode node, int Num)
        {
            node.Tag += Num;
        }

        #region 遍历函数

        /// <summary>
        /// 对树中节点按指定遍历顺序执行op操作。
        /// </summary>
        /// <param name="mode">遍历方式</param>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        public void Traversal(TraversalModeEnum mode, TraversalOperate op, int Counter, int Step)
        {
            switch (mode)
            {
                case TraversalModeEnum.Pre:
                    PreOTraversal(op, ref Counter, Step);
                    break;
                case TraversalModeEnum.Post:
                    PostOTraversal(op, ref Counter, Step);
                    break;
                case TraversalModeEnum.Up:
                    UpTraversal(op, ref Counter, Step);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 对树中节点按指定遍历顺序执行op操作，可通过控制器影响遍历过程。
        /// </summary>
        /// <param name="mode">遍历方式</param>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="control">控制器</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        public void Traversal(TraversalModeEnum mode, TraversalOperate op, TraversalControl control, int Counter, int Step)
        {
            switch (mode)
            {
                case TraversalModeEnum.Pre:
                    PreOTraversal(op, control, ref Counter, Step);
                    break;
                case TraversalModeEnum.Post:
                    PostOTraversal(op, control, ref Counter, Step);
                    break;
                case TraversalModeEnum.Up:
                    UpTraversal(op, control, ref Counter, Step);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 从一个节点遍历至另一个节点
        /// </summary>
        /// <param name="BeginNode">起始节点</param>
        /// <param name="EndNode">终止节点</param>
        /// <param name="IgnoreTopNode">标识是否要忽略路径中的节点中，在树中位置最高的节点（即路径的拐点）</param>
        /// <param name="op">对遍历过的节点要执行的操作</param>
        public int Traversal(XTreeNode BeginNode, XTreeNode EndNode, bool IgnoreTopNode,TraversalOperate op)
        {
            XTreeNode JoinNode = null;   //交点
            List<XTreeNode> NodeList = new List<XTreeNode>(3); //终点至顶点的节点列表（不包含顶点）

            XTreeNode.TraversalControl ctrl;
            XTreeNode.TraversalOperate InnelOp;

            //参数校验
            if (BeginNode == null || EndNode == null || op == null) return -1;           

            //清除末端节点至根节点路径中各节点的Tag
            EndNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);

            //对起点至根节点的路径进行标记                      
            BeginNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 1, 0); //使用递增计数器是为了计算路径长度

            #region 从终点往上走，对路经节点进行标记。在遇到设置了Tag的节点（即两条路径交点）时停止。
            InnelOp = delegate(XTreeNode node, int Counter){ };

            ctrl = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag > 0)
                {
                    JoinNode = node;
                    return TraversalControlEnum.Stop;
                }
                else
                {
                    node.Tag = 1;
                    NodeList.Add(node);
                    return TraversalControlEnum.Normal;
                }
            };
            EndNode.Traversal(TraversalModeEnum.Up, InnelOp, ctrl, 1, 0);

            #endregion

            //不在同一棵树中时返回错误代码
            if (JoinNode == null) return -1;

            //要求忽略顶点时，将交点Tag清除，否则将交点的父节点的Tag清除
            if(IgnoreTopNode) 
                JoinNode.Tag = 0;
            else
            {
                if (JoinNode.Father != null) JoinNode.Father.Tag = 0;
            }

            #region 对路径进行遍历

            //从起点开始向上遍历执行用户的遍历操作，遇到Tag被清除的节点时停止
            InnelOp = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag != 0) op(node, Counter);
            };

            ctrl = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag == 0) 
                    return TraversalControlEnum.Stop;
                return TraversalControlEnum.Normal;
            };

            if (JoinNode != BeginNode)
            {
                //从起点开始向上执行用户的遍历操作
                BeginNode.Traversal(TraversalModeEnum.Up, InnelOp, ctrl, 0, 0);
            }

            //对终点与交点之间的路径（不包含交点）执行从上至下的遍历，并执行用户操作
            NodeList.Reverse();
            foreach (XTreeNode node in NodeList) op(node, 0);

            #endregion

            return 1;
        }

        /// <summary>
        /// 前序遍历，对树中节点按遍历顺序执行op操作。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        private void PreOTraversal(TraversalOperate op, ref int Counter, int Step)
        {
            op(this, Counter);
            Counter += Step;
            foreach (XTreeNode son in Sons) son.PreOTraversal(op, ref Counter, Step);
        }

        /// <summary>
        /// 后序遍历，对树中节点按遍历顺序执行op操作。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        private void PostOTraversal(TraversalOperate op, ref int Counter, int Step)
        {
            foreach (XTreeNode Son in Sons) Son.PostOTraversal(op, ref Counter, Step);
            op(this, Counter);
            Counter += Step;
        }

        /// <summary>
        /// 前序遍历，对树中节点按遍历顺序执行op操作。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        private void PreOTraversal(TraversalOperate op)
        {
            op(this, 0);
            foreach (XTreeNode Son in Sons) Son.PreOTraversal(op);
        }

        /// <summary>
        /// 后序遍历，对树中节点按遍历顺序执行op操作。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        private void PostOTraversal(TraversalOperate op)
        {
            foreach (XTreeNode Son in Sons) Son.PostOTraversal(op);
            op(this, 0);

        }

        /// <summary>
        /// 前序遍历，对树中节点按遍历顺序执行op操作。可通过Control影响遍历过程。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="control">控制器</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        /// <returns>用于控制遍历过程，无需关注此返回值</returns>
        private bool PreOTraversal(TraversalOperate op, TraversalControl control, ref int Counter, int Step)
        {
            bool Result = true;

            //执行遍历操作
            op(this, Counter);
            Counter += Step;

            //控制
            switch (control(this, Counter - Step))
            {
                case TraversalControlEnum.Stop:     //返回false
                    Result = false;
                    break;

                case TraversalControlEnum.IgnoreChild:  //不对子节点进行遍历
                    break;

                case TraversalControlEnum.Normal:       //对子节点进行遍历
                    foreach (XTreeNode son in Sons)
                    {
                        if (!son.PreOTraversal(op, control, ref Counter, Step))     //若某子节点决定终止遍历，则不对剩下的子节点进行遍历，并通过返回false告之父节点终止遍历。
                        {
                            Result = false;
                            break;
                        }
                    }
                    break;

                default:
                    Result = false;
                    break;
            }
            return Result;
        }

        /// <summary>
        /// 后序遍历，对树中节点按遍历顺序执行op操作。可通过Control影响遍历过程。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="control">控制器</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        /// <returns>用于控制遍历过程，无需关注此返回值</returns>
        private bool PostOTraversal(TraversalOperate op, TraversalControl control, ref int Counter, int Step)
        {
            bool Result = true;

            switch (control(this, Counter))
            {
                case TraversalControlEnum.Stop:
                    Result = false;
                    break;

                case TraversalControlEnum.IgnoreChild:
                    break;

                case TraversalControlEnum.Normal:   //遍历子节点
                    foreach (XTreeNode son in Sons)
                    {
                        if (!son.PostOTraversal(op, control, ref Counter, Step))    //若某子节点决定终止遍历，则不对剩下的子节点进行遍历，并通过返回false告之父节点终止遍历。
                        {
                            Result = false;
                            break;
                        }
                    }
                    break;

                default:
                    Result = false;
                    break;
            }

            op(this, Counter);
            Counter += Step;

            return Result;
        }

        /// <summary>
        /// 对从当前节点到根节点的路径中节点执行op操作，通过控制器可控制遍历过程。
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="control">控制器</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        private void UpTraversal(TraversalOperate op, TraversalControl control, ref int Counter, int Step)
        {
            XTreeNode node = this;

            do
            {
                op(node, Counter);
                if (control(node, Counter) != TraversalControlEnum.Normal) break;
                Counter += Step;
                node = node.Father;
            } while (node != null);
        }

        /// <summary>
        /// 对从当前节点到根节点的路径中节点执行op操作
        /// </summary>
        /// <param name="op">对节点执行的操作</param>
        /// <param name="Counter">计数器初值</param>
        /// <param name="Step">计数器步进值</param>
        private void UpTraversal(TraversalOperate op, ref int Counter, int Step)
        {
            XTreeNode node = this;

            do
            {
                op(node, Counter);
                Counter += Step;
                node = node.Father;
            } while (node != null);
        }

        #endregion

        #endregion
    }
}