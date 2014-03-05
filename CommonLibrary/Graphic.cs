/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��PowerSupplyModel.cs

 * ˵�������ļ�����ͼ������ͼ�����ͼ�㷨

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��2��17��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// ����ͼ��㣬��Graphic���ʹ��
    /// </summary>
    public class GraphicNode : ICloneable
    {
        #region ����
        //��ǩ
        private int _Tag;

        //�������
        int _TraversalFlag;

        //�û�����
        private object _UserObject;

        //�ڵ������ߵļ���
        List<GraphicEdge> _Edges;

        #endregion

        #region ����

        /// <summary>
        /// �ؼ���
        /// </summary>
        public object UserObject
        {
            get { return _UserObject; }
            set { _UserObject = value; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int TraversalFlag
        {
            get { return _TraversalFlag; }
            set { _TraversalFlag = value; }
        }

        /// <summary>
        /// �ڵ������ߵļ���
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

        #region ����

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GraphicNode()
        {
            _Edges = new List<GraphicEdge>(3);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="EdgeCount">�߼���������Ԥ�ڵı�����</param>
        public GraphicNode(int EdgeCount)
        {
            _Edges = new List<GraphicEdge>(EdgeCount);
        }

        /// <summary>
        /// �������캯�������Ὣ�¶�����ԭ��������ı߶�����й�����
        /// </summary>
        /// <param name="Other">��Դ����</param>
        /// <param name="CopyUserObject">�Ƿ񿽱��û�����</param>
        public GraphicNode(GraphicNode Other, bool CopyUserObject)
        {
            this._Edges = new List<GraphicEdge>(Other._Edges.Count);
            if (CopyUserObject) this._UserObject = Other._UserObject;
            this._Tag = Other._Tag;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userData">�û�����</param>
        public GraphicNode(object userData)
        {
            this.UserObject = userData;
        }

        /// <summary>
        /// ����ͼ�ڵ��һ�������������û����ݣ�ֵ�����ã��������Ὣ�¶�����ԭ��������ı߶�����й�����
        /// </summary>
        /// <returns>�µĿ�������</returns>
        public object Clone()
        {
            return new GraphicNode(this, true);
        }

        /// <summary>
        /// ����ͼ�ڵ��һ�������������Ὣ�¶�����ԭ��������ı߶�����й�����
        /// </summary>
        /// <param name="CopyUserObject">�Ƿ񿽱��û����ݡ�</param>
        /// <returns>�µĿ�������</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new GraphicNode(this, CopyUserObject);
        }

        #endregion
    }

    /// <summary>
    /// ����ͼ�ߣ���Graphic���ʹ��
    /// </summary>
    public class GraphicEdge : ICloneable
    {
        #region ����

        //��ǩ
        private int _Tag;

        //�������
        private int _TraversalFlag;

        //�û�����
        private object _UserObject;

        //�ߵĶ˵�ļ���
        GraphicNode[] _Nodes = new GraphicNode[2];

        #endregion

        #region ����

        /// <summary>
        /// �ؼ���
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int TraversalFlag
        {
            get { return _TraversalFlag; }
            set { _TraversalFlag = value; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public object UserObject
        {
            get { return _UserObject; }
            set { _UserObject = value; }
        }

        /// <summary>
        /// �ڵ������ߵļ���
        /// </summary>
        public GraphicNode[] Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GraphicEdge()
        {

        }

        /// <summary>
        /// �������캯���������Ὣ�¶�����ԭ��������Ľڵ������й�����
        /// </summary>
        /// <param name="Other">��Դ����</param>
        /// <param name="CopyUserObject">�Ƿ񿽱��û�����</param>
        public GraphicEdge(GraphicEdge Other, bool CopyUserObject)
        {
            this._Tag = Other._Tag;
            if (CopyUserObject) this._UserObject = Other._UserObject;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="node1">�������˵�</param>
        /// <param name="node2">�������˵�</param>
        /// <param name="userData">�߹������û�����</param>
        public GraphicEdge(GraphicNode node1, GraphicNode node2, object userData)
        {
            Nodes[0] = node1;
            Nodes[1] = node2;
            UserObject = userData;
        }

        /// <summary>
        /// ����ͼ�ڵ��һ���������˷����������û����ݣ�ֵ�����ã��������Ὣ�¶�����ԭ��������Ľڵ������й�����
        /// </summary>
        /// <returns>�µĿ�������</returns>
        public object Clone()
        {
            return new GraphicEdge(this, true);
        }

        /// <summary>
        /// ����ͼ�߶����һ�������������Ὣ�¶�����ԭ��������Ľڵ������й�����
        /// </summary>
        /// <param name="CopyUserObject">�Ƿ񿽱��û����ݡ�</param>
        /// <returns>�µĿ�������</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new GraphicEdge(this, CopyUserObject);
        }

        /// <summary>
        /// ���ڱ���һ�˵Ľڵ㣬�뱣֤����Ľڵ������˸ñߡ�
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <returns>������һ�˵Ľڵ�</returns>
        public GraphicNode OtherNode(GraphicNode node)
        {
            return (object.ReferenceEquals(node, Nodes[0])) ? Nodes[1] : Nodes[0];
        }

        #endregion
    }

    /// <summary>
    /// ����ͼģ�ͣ���������֧�ֿ�������ͼ�е����з���������֤���̰߳�ȫ��
    /// </summary>
    public class Graphic : ICloneable
    {
        #region ����

        /// <summary>
        /// �Ա��������о����Ľڵ�ִ�еķ���ԭ�͡�
        /// </summary>
        /// <param name="node">��ǰ�ڵ�</param>
        /// <param name="edge">���ﵱǰ�ڵ�;���ı�</param>
        /// <param name="counter">����������������ÿ����һ���ڵ�ʱ���������������ɹ�����ڱ���������ָ����</param>
        /// ���ڴ�ԭ�͵Ĳ����б�Ϊ�β�������������ʼ�ڵ����Ϣ�����⣺
        /// �������÷��ڵ���ͼ���еı����ӿ�ʱ������ָ����㡣�����÷�֪����㡣
        /// ������ԭ��ʵ�ʹ����Ĵ����ɱ����ӿڵĵ��÷��ṩ���ڵ��÷��Ĵ������ִ�С�
        /// ������˴�ԭ�������ṩ�����Ϣ��
        /// ע������Ҫ֪���ڵ��ڱ��������е�˳������ʽڵ��TraversalFlag��ʶ����1��ʼ��
        /// ���棺ִ�б�������ʱ����ע��������β�Ҫ�޸��޸Ľڵ���ߵ�Tag����Լ�TraversalFlag��
        public delegate void TraversalOperate(GraphicNode node, List<GraphicEdge> path);

        /// <summary>
        /// ���Ʊ������̵ķ���ԭ�͡�
        /// </summary>
        /// <param name="node">��ǰ�ڵ�</param>
        /// <param name="edge">���ﵱǰ�ڵ�;���ı�</param>
        /// <param name="counter">����������������ÿ����һ���ڵ�ʱ���������������ɹ�����ڱ���������ָ����</param>
        /// <returns>ö��:Normal:����;Ignore:���Ӵ˽ڵ�̽�����ڽڵ�;Stop:��ֹ������</returns>
        /// ע������Ҫ֪���ڵ��ڱ��������е�˳������ʽڵ��TraversalFlag��ʶ����1��ʼ��
        /// ���棺ִ�п��Ʋ���ʱ����ע��������β�Ҫ�޸��޸Ľڵ���ߵ�Tag��ǡ�
        public delegate TraversalControlEnum TraversalControl(GraphicNode node, List<GraphicEdge> path);

        /// <summary>
        /// ö��:�������̵Ŀ�ѡ���ơ�Normal:����;Ignore:���Ӵ˽ڵ�̽�����ڽڵ�;Stop:��ֹ������
        /// </summary>
        public enum TraversalControlEnum { Normal, IgnoreChild, Stop }

        /// <summary>
        /// ö��:���õı�����ʽ��DFS:�����������;BFS:�����������
        /// </summary>
        public enum TraversalModeEnum { DFS, BFS };

        #endregion

        #region �����ݡ���������

        //ͼ�еĶ��㼯
        private List<GraphicNode> _Nodes = new List<GraphicNode>(10);

        //ͼ�еı߼�
        private List<GraphicEdge> _Edges = new List<GraphicEdge>(15);

        #endregion

        #region ��������

        //ͼ�еĻ�·
        private List<List<GraphicEdge>> _Loops = new List<List<GraphicEdge>>(3);    //��·����        
        private bool _LoopAnalysed = false;                                         //��·�Ƿ��ѷ�����־

        private List<GraphicEdge> _LoopEdges = new List<GraphicEdge>(0);            //ͼ�еĹ��ɻ�·�ı߼�
        private bool _LoopEdgeAnalysed = false;                                     //�ɻ�֧·�Ƿ��ѷ�����־

        //ͼ�еķǻ�·��
        private List<Object>[] _SinglePath;
        private bool _SinglePathAnalysed = false;

        //���������Ƿ���Ч
        private bool AnalysisDataValid = false;

        #endregion

        #region ��������
        private int TraversalInnerCounter;  //���ü����������ڼ�¼�ѱ����Ľڵ���

        private TraversalOperate TraversalOp;   //�Ա��������Ľڵ�ִ�еĲ���
        private TraversalControl TraversalCtrl; //��������ʹ�õĿ�����

        private List<GraphicEdge> TraversalPath; //����·��

        private bool TraversalMakeWholePath;      //ָʾ����ʱ���û������ӿںͿ��ƽӿڴ�������·���������һ����

        #endregion

        #region ����

        /// <summary>
        /// ͼ���еĽڵ㼯��
        /// </summary>
        public List<GraphicNode> Nodes
        {
            get { return _Nodes; }
        }

        /// <summary>
        /// ͼ�еı߼���
        /// </summary>
        public List<GraphicEdge> Edges
        {
            get { return _Edges; }
        }

        /// <summary>
        /// ͼ�еĻ�·����
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
        /// ��·��Ϣ����Ч�Ա��
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
        /// ��·�߼�����Ч�Ա��
        /// </summary>
        public bool LoopEdgeAnalysed
        {
            get { return _LoopEdgeAnalysed; }
            set { _LoopEdgeAnalysed = value; }
        }

        /// <summary>
        /// ͼ�еĵ���·����һ����Ԫ���飬Ԫ��1Ϊ����·���нڵ㼯�ϣ�Ԫ��2Ϊ����·���еı߼���
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
        /// �޻�·����Ϣ����Ч�Ա��
        /// </summary>
        public bool SinglePathAnalysed
        {
            get { return _SinglePathAnalysed; }
            set { _SinglePathAnalysed = value; }
        }

        #endregion

        #region ����

        #region ���캯��

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Graphic()
        {
            _Nodes = new List<GraphicNode>(10);
            _Edges = new List<GraphicEdge>(15);
        }

        /// <summary>
        /// �������캯����������ͼ�����нڵ㡢�����ݣ��������ƽڵ�����ϵ��û����ݵ����ã�ǳ��������
        /// </summary>
        /// <param name="Other"></param>
        public Graphic(Graphic Other)
        {
            GraphicEdge edge;
            GraphicNode node;

            //��ԭͼ�нڵ���߽��б��
            for (int index = 0; index < Other.Nodes.Count; ++index) Other.Nodes[index].Tag = index;
            for (int index = 0; index < Other.Edges.Count; ++index) Other.Edges[index].Tag = index;

            //��ʼ���ڵ���߼���
            _Nodes = new List<GraphicNode>(Other.Nodes.Count);
            _Edges = new List<GraphicEdge>(Other.Edges.Count);

            //��ʼ���ڵ����
            for (int index = 0; index < Other.Nodes.Count; ++index)
            {
                this.Nodes.Add((GraphicNode)(Other.Nodes[index].Clone()));
            }
            for (int index = 0; index < Other.Edges.Count; ++index)
            {
                this.Edges.Add((GraphicEdge)(Other.Edges[index].Clone()));
            }

            //�����¶���ڵ���ߵ����ӹ�ϵ
            foreach (GraphicEdge GEdge in Other.Edges)
            {
                edge = this.Edges[GEdge.Tag];                       //�ҳ��¶�������GEdge���Ӧ�ı�
                edge.Nodes[0] = this.Nodes[GEdge.Nodes[0].Tag];     //���¶����ж˵㼯���ӵ���Ӧ�Ľڵ�
                edge.Nodes[1] = this.Nodes[GEdge.Nodes[1].Tag];
                edge.Nodes[0].Edges.Add(edge);                      //���¶˽ڵ�ı��б�
                edge.Nodes[1].Edges.Add(edge);
            }

        }

        #endregion

        #region ���ƺ���

        /// <summary>
        /// ����ͼ��һ���������˿���������ͼ�����нڵ㡢�ߡ��������ݵĸ����������ƽڵ���߹������û����ݵ����á�
        /// </summary>
        /// <returns>�µĿ�������</returns>
        public object Clone()
        {
            return new Graphic(this);
        }

        #endregion

        /// <summary>
        /// ��ʼ���ڵ�ı��
        /// </summary>
        /// <param name="InitValue">��ʼ��Tagֵ</param>
        /// <param name="Step">��ͬ�ڵ���Tag��ֵ</param>
        /// <returns>�������һ��δʹ�õ�Tagֵ</returns>
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
        /// ��ʼ���ڵ�ı��
        /// </summary>
        /// <param name="InitValue">��ʼ��Tagֵ</param>
        /// <param name="Step">��ͬ�ڵ���Tag��ֵ</param>
        /// <returns>�������һ��δʹ�õ�Tagֵ</returns>
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
        /// ���÷������ݵ���Ч��״̬
        /// </summary>
        /// <param name="invalidFlag">Ŀ��״̬</param>
        private void SetValidFlag(bool ValidFlag)
        {
            LoopAnalysed = ValidFlag;
            SinglePathAnalysed = ValidFlag;
            LoopEdgeAnalysed = ValidFlag;
        }

        /// <summary>
        /// ���ͼ��
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

        #region ��������

        /// <summary>
        /// ��ͼ�нڵ���б���
        /// </summary>
        /// <param name="traversalMode">������ʽ</param>
        /// <param name="startNode">��ʼ�ڵ�</param>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="ctrl">���ƣ����û�����������봫���ֵ</param>
        /// <param name="getWholePath">true:��������������������ӿںͿ��ƽӿڴ�����������ǰ�ڵ������·��;false:��������������������ӿںͿ��ƽӿڴ������������һ���ڵ�����ǰ�ڵ�ıߵ�·�����������������������������·���Ĵ��ۼ������Ժ��ԣ����ڹ�������������������·�������йء�</param>
        public void TraversalNode(TraversalModeEnum traversalMode, GraphicNode startNode, TraversalOperate op, TraversalControl ctrl, bool getWholePath)
        {
            //��ʼ����������
            this.TraversalCtrl = ctrl;
            this.TraversalOp = op;
            this.TraversalInnerCounter = 1;
            this.TraversalMakeWholePath = getWholePath;

            //��ʼ��·��
            if (this.TraversalPath == null) this.TraversalPath = new List<GraphicEdge>(this.Nodes.Count);

            //����������
            ClearTraversalFlag();

            //��·�����������
            TraversalPath.Clear();

            //ѡ����ʵı����㷨
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
        /// ��ָ���ڵ㿪ʼ����������ȱ�����
        /// </summary>
        /// <param name="StartNode">��ʼ�ڵ�</param>
        /// <param name="PassEdge">��������ǰ�ڵ�ʱ�����ı�</param>
        /// <returns>����ֵ���ڸ�֮�����Ƿ���ֹ</returns>
        private bool DFSNodeWithUserCtrl(GraphicNode node, GraphicEdge edge)
        {
            bool Result = true;
            GraphicNode NeighborNode; //���ڽڵ�

            //�Խڵ�ִ�в��������            
            node.TraversalFlag = TraversalInnerCounter;

            //��Ǳ�
            if (edge != null)
            {
                edge.TraversalFlag = TraversalInnerCounter;

                if (!TraversalMakeWholePath) this.TraversalPath.Clear();

                this.TraversalPath.Add(edge);                   //���߼���·��
            }

            //�����û�����
            TraversalOp(node, this.TraversalPath);

            //�ж��Ƿ������ϲ����±���������
            if (TraversalInnerCounter == this.Nodes.Count) return false;
            ++TraversalInnerCounter;

            //���ݿ����������Ƿ�������ڽڵ�
            switch (TraversalCtrl(node, this.TraversalPath))
            {
                case TraversalControlEnum.Stop:
                    Result = false;
                    break;
                case TraversalControlEnum.Normal:       //�����ӽڵ�
                    foreach (GraphicEdge LinkedEdge in node.Edges)
                    {
                        if (LinkedEdge.TraversalFlag > -1) continue; //�������Ѿ��������ı�

                        NeighborNode = LinkedEdge.OtherNode(node);

                        if (NeighborNode.TraversalFlag > -1) continue;   //�������Ѿ��������Ľڵ�

                        if (!DFSNodeWithUserCtrl(NeighborNode, LinkedEdge))     //�����ӽڵ�Ҫ��ֹ�������򲻶�ʣ��ڵ����������������false��֮��һ��ݹ顣
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

            //���ߴ�·����ɾ����Ȼ��ط���һ�ڵ�
            if (this.TraversalPath.Count > 0) this.TraversalPath.RemoveAt(this.TraversalPath.Count - 1);

            return Result;
        }

        /// <summary>
        /// ��ָ���ڵ㿪ʼ���й�����ȱ�����
        /// </summary>
        /// <param name="node">��ǰ�ڵ�</param>
        /// <param name="edge">��������ǰ�ڵ�ʱ�����ı�</param>
        private void BFSNodeWithUserCtrl(GraphicNode node)
        {
            List<GraphicNode> NodeList = new List<GraphicNode>(Nodes.Count);
            List<GraphicEdge> EdgeList = new List<GraphicEdge>(Nodes.Count);

            int idx = 0;

            GraphicEdge edge = null;
            GraphicNode NeighborNode;
            bool StopSearchFlag = false;

            //���ʼ�ڵ�
            node.TraversalFlag = TraversalInnerCounter;

            //��ʼ�ڵ�ѹջ
            NodeList.Add(node);
            EdgeList.Add(edge);

            do
            {
                //�Ӷ�����ȡ�ڵ㼰��
                node = NodeList[idx];
                edge = EdgeList[idx];

                //��·��     
                this.TraversalPath.Clear();
                if (TraversalMakeWholePath)
                {
                    while (edge != null)
                    {
                        this.TraversalPath.Add(edge);
                        //�ڵ��TraversalFlag�������˽ڵ��ı���˳��ͬʱҲ�����˽ڵ������List�е�����
                        edge = EdgeList[Math.Min(edge.Nodes[0].TraversalFlag, edge.Nodes[1].TraversalFlag) - 1];
                    }
                    this.TraversalPath.Reverse();
                }
                else
                    if (edge != null) this.TraversalPath.Add(edge);

                //�Խڵ�ִ�б�������
                TraversalOp(node, this.TraversalPath);

                //�ж��Ƿ��������������ڽڵ�
                if (StopSearchFlag) continue;

                //���ݿ�����ȷ���Ƿ�������ڽڵ�
                switch (TraversalCtrl(node, this.TraversalPath))
                {
                    case TraversalControlEnum.Stop:
                        StopSearchFlag = true;
                        break;
                    case TraversalControlEnum.Normal:   //�������ڽڵ�
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
        /// ��ָ���ڵ㿪ʼ����������ȱ�����
        /// </summary>
        /// <param name="StartNode">��ʼ�ڵ�</param>
        /// <param name="PassEdge">��������ǰ�ڵ�ʱ�����ı�</param>
        /// <returns>����ֵ���ڸ�֮�����Ƿ���ֹ</returns>
        private bool DFSNodeNoUserCtrl(GraphicNode node, GraphicEdge edge)
        {
            GraphicNode NeighborNode; //���ڽڵ�

            //�Խڵ�ִ�в��������
            node.TraversalFlag = TraversalInnerCounter;
            if (edge != null)
            {
                edge.TraversalFlag = TraversalInnerCounter;

                if (!TraversalMakeWholePath) this.TraversalPath.Clear();

                this.TraversalPath.Add(edge);               //���߼���·��                    
            }

            TraversalOp(node, this.TraversalPath);

            //���±������������ж��Ƿ�������,����ֹͣ������û���κεط���Ҫʹ��TraversalPath�����ڴ˴�����ֱ�ӷ���
            if (TraversalInnerCounter == this.Nodes.Count) return false;

            ++TraversalInnerCounter;

            //�����ӽڵ�
            foreach (GraphicEdge LinkedEdge in node.Edges)
            {
                if (LinkedEdge.TraversalFlag > -1) continue; //��������������·��

                NeighborNode = LinkedEdge.OtherNode(node);

                if (NeighborNode.TraversalFlag > -1) continue;   //�������������Ľڵ�

                if (!DFSNodeNoUserCtrl(NeighborNode, LinkedEdge))
                {
                    //����ֹͣ������û���κεط���Ҫʹ��TraversalPath�����ڴ˴�����ֱ�ӷ���
                    return false;
                }
            }

            //���ߴ�·�����Ƴ�
            if (this.TraversalPath.Count > 0) this.TraversalPath.RemoveAt(this.TraversalPath.Count - 1);

            return true;
        }

        /// <summary>
        /// ��ָ���ڵ㿪ʼ���й�����ȱ�����
        /// </summary>
        /// <param name="node">��ǰ�ڵ�</param>
        /// <param name="edge">��������ǰ�ڵ�ʱ�����ı�</param>
        /// <returns>����ֵ���ڸ�֮�����Ƿ���ֹ</returns>
        private void BFSNodeNoUserCtrl(GraphicNode node)
        {
            List<GraphicNode> NodeList = new List<GraphicNode>(Nodes.Count);
            List<GraphicEdge> EdgeList = new List<GraphicEdge>(Nodes.Count);

            int idx = 0; //����������

            bool StopSearchFlag = false;

            GraphicEdge edge = null;
            GraphicNode NeighborNode;

            node.TraversalFlag = TraversalInnerCounter;

            NodeList.Add(node);
            EdgeList.Add(edge);

            do
            {
                //�Ӷ�����ȡ�ڵ㼰��
                node = NodeList[idx];
                edge = EdgeList[idx];

                //��·��
                this.TraversalPath.Clear();
                if (this.TraversalMakeWholePath)
                {
                    while (edge != null)
                    {
                        this.TraversalPath.Add(edge);
                        //�ڵ��TraversalFlag�������˽ڵ��ı���˳��ͬʱҲ�����˽ڵ������List�е�����
                        edge = EdgeList[Math.Min(edge.Nodes[0].TraversalFlag, edge.Nodes[1].TraversalFlag) - 1];
                    }
                    this.TraversalPath.Reverse();
                }
                else
                    if (edge != null) this.TraversalPath.Add(edge);

                //�Խڵ�ִ�б�������
                TraversalOp(node, this.TraversalPath);

                //�ж��Ƿ�ֹͣ�������ڽ��
                if (StopSearchFlag) continue;

                //�������ڽڵ�
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
        /// ����������
        /// </summary>
        private void ClearTraversalFlag()
        {
            foreach (GraphicNode node in Nodes) node.TraversalFlag = -1;
            foreach (GraphicEdge edge in Edges) edge.TraversalFlag = -1;
        }

        #endregion

        #region ͼ�㷨

        /// <summary>
        /// ����ϵͳ�еĻ�·
        /// </summary>
        /// <returns>1:�ɹ�; 0:�û�ȡ��; -1:����</returns>
        private int LoopAnalysis()
        {
            TraversalOperate op;
            TraversalControl ctrl;

            //һ����
            List<GraphicEdge> loop;

            //���ڴ洢����������δ�����ı�
            List<GraphicEdge> freeEdges = new List<GraphicEdge>(Math.Max(this.Edges.Count - this.Nodes.Count + 1, 0));

            //�ж��Ƿ���ʣ��ڵ�
            if (SinglePath[0].Count == Nodes.Count)
            {
                _Loops = new List<List<GraphicEdge>>(0);
                return 1;
            }

            //��ͼ�еķǻ�·���Ͽ�
            foreach (GraphicEdge singlePathEdge in SinglePath[1])
            {
                singlePathEdge.Nodes[0].Edges.Remove(singlePathEdge);
                singlePathEdge.Nodes[1].Edges.Remove(singlePathEdge);
                this.Edges.Remove(singlePathEdge);
            }
            //��Ƿǻ�·���еĽڵ�
            foreach (GraphicNode node in Nodes) node.Tag = 0;
            foreach (GraphicNode singlePathNode in SinglePath[0]) singlePathNode.Tag = 1;

            //�ӻ�·�еĽڵ��ͼ���б���
            op = delegate(GraphicNode node, List<GraphicEdge> path) { };
            foreach (GraphicNode singlePathNode in this.Nodes)
            {
                if (singlePathNode.Tag == 0) //Ѱ�һ�·�еĽڵ�
                {
                    TraversalNode(TraversalModeEnum.BFS, singlePathNode, op, null, false);
                    break;
                }
            }

            //����δ�����ı�
            foreach (GraphicEdge edge in this.Edges)
            {
                if (edge.TraversalFlag == -1) freeEdges.Add(edge);
            }

            //����freeEdges��ʼ�����б��ڱ���������ÿ��δ�����ı��뾭���ı߱ع���һ����
            if (_Loops == null)
                _Loops = new List<List<GraphicEdge>>(freeEdges.Count);
            else
            {
                _Loops.Clear();
                _Loops.Capacity = freeEdges.Count;
            }


            //��ɾ����һ��δ�������ıߺ󣬸ñ����˵������·��
            foreach (GraphicEdge fEdge in freeEdges)
            {
                //���߶Ͽ�
                fEdge.Nodes[0].Edges.Remove(fEdge);
                fEdge.Nodes[1].Edges.Remove(fEdge);

                //�ӱߵ�һ�˿�ʼ��ͼ���й����������������������һ�˵�ʱֹͣ����·����ñ߹���һ����̻���
                ctrl = delegate(GraphicNode node, List<GraphicEdge> path) //��������������������ô������ŵ�foreach���أ�
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

                //�ָ���
                fEdge.Nodes[0].Edges.Add(fEdge);
                fEdge.Nodes[1].Edges.Add(fEdge);
            }

            //�ָ��ǻ�·��
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
        /// ��ͼ���γɻ��ıߵļ���
        /// </summary>
        /// <returns></returns>
        private int LoopEdgeAnalysis()
        {
            XTreeNode.TraversalOperate TreeOp;

            Graphic.TraversalControl GraphicCtrl;

            int GenerationTreeNodeCount = 0,idx;                    //�������еĽڵ���

            XTreeNode GenerationTree = null;��                      //ͼ��������

            List<GraphicEdge> freeEdges;                            //���ڴ洢����������δ�����ı�

            List<List<int>> NodeLinksList = new List<List<int>>(10);//��ͼ���ڴ洢ÿ���ڵ���������FreeEdges�еıߣ�����ֵΪFreeEdges�еı���FreeEdges�е��������ڵ��Tagֵ��Ӧ����NodeLinksList������


            XTreeNode[,] TreeNodePairs;                             //FreeEdges���˵��Ӧ�����ڵ㼯��

            //У��
            _LoopEdges.Clear();
            if (this.Nodes.Count <= SinglePath[0].Count) return 1;  //�����ڷǵ���·��ʱ�����ʾͼ���޻�

            #region ����һ�ò���������֧·�нڵ��������

            //��ͼ�нڵ���߱��Ϊ0
            foreach (GraphicNode node in Nodes) node.Tag = 0;
            foreach (GraphicEdge edge in Edges) edge.Tag = 0;

            //������·���еĽڵ���߱��Ϊ-1
            foreach (GraphicNode singlePathNode in SinglePath[0]) singlePathNode.Tag = -1;
            foreach (GraphicEdge singlePathEdge in SinglePath[1]) singlePathEdge.Tag = -1;

            //���������������Ŀ�������ʹ������ͼʱ����������·���нڵ�ʱ�������������ڽڵ�
            GraphicCtrl = delegate(GraphicNode Node,List<GraphicEdge> Path)
            {
                return (Node.Tag == -1)?Graphic.TraversalControlEnum.IgnoreChild:Graphic.TraversalControlEnum.Normal;
            };

            #region �ӷǵ���֧·�еĽڵ㿪ʼ����һ��������
            foreach (GraphicNode node in this.Nodes)
            {
                if (node.Tag == 0) //Ѱ�һ�·�еĽڵ�
                {
                    GenerationTree = GetGenerationTree(node, TraversalModeEnum.BFS, GraphicCtrl, true);
                    break;
                }
            }
            #endregion

            #endregion

            #region ���������еıߵ�Tag��1���������нڵ���м���

            TreeOp = delegate(XTreeNode node, int Counter)
            {
                if (node.Father != null) ((GraphicEdge)node.EdgeUserObject).Tag = 1;
                GenerationTreeNodeCount = Counter;
            };
            GenerationTree.Traversal(XTreeNode.TraversalModeEnum.Pre, TreeOp, 1, 1);

            #endregion

            #region ��ʼ����������

            _LoopEdges.Capacity = this.Edges.Count - this.SinglePath[1].Count;

            freeEdges = new List<GraphicEdge>(Math.Max(this.Edges.Count - this.Nodes.Count + 1,0));

            #endregion

            #region ȡ������δ�����ıߣ�������Ϊ2,�����乹�ɻ���

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

            #region �����������б��������ݽ�������ͼ�ڵ��Tagֵ�������TreeNodePairs

            TreeOp = delegate(XTreeNode Node, int Counter)
            {
                int tag = ((GraphicNode)Node.NodeUserObject).Tag;  //FreeEdges���˽ڵ��Tag��1��ʼ��ţ���Ҫ��1
                if ((tag) > 0)
                {
                    foreach (int row in NodeLinksList[tag - 1])
                    {
                        idx = (TreeNodePairs[row, 0] == null) ? 0 : 1; //�ж�TreeNodeParis[row]ά�е�һ���Ƿ���ռ��
                        TreeNodePairs[row, idx] = Node;
                    }
                }
            };
            GenerationTree.Traversal(XTreeNode.TraversalModeEnum.Pre, TreeOp, 0, 0);

            #endregion

            #region ����ÿ�����ڵ㣬�����·������Ӧ��ͼ�ߵ�Tag�����Ϊ2,������Ӧ�ı߹��ɻ�

            TreeOp = delegate(XTreeNode Node, int Counter)
            {
                if(Node.EdgeUserObject != null) ((GraphicEdge)Node.EdgeUserObject).Tag = 2;
            };
            for (idx = 0; idx < TreeNodePairs.GetLength(0); idx++)
            {
                GenerationTree.Traversal(TreeNodePairs[idx,0], TreeNodePairs[idx,1], true, TreeOp);
            }

            #endregion

            #region ͼ�����б��Ϊ2�ı߹��ɻ�

            foreach (GraphicEdge edge in this.Edges) if (edge.Tag == 2) _LoopEdges.Add(edge);

            #endregion

            LoopEdgeAnalysed = true;

            return 1;
        }

        /// <summary>
        /// �ж�ָ���������Ƿ���ͨ
        /// </summary>
        /// <param name="Area">Ŀ������</param>
        /// <param name="BeginNode">�ڵ�</param>
        /// <param name="AloneNodes">������BeginNode����ͬһ��ͨ�����еĽڵ�ļ���</param>
        /// <returns></returns>
        public int ConnexAnalysis(List<GraphicNode> Area, GraphicNode BeginNode, List<GraphicNode> AloneNodes)
        {
            int Count = 0; //��BeginNode������ͨ�����еĽڵ���м���

            foreach (GraphicNode node in this.Nodes) node.Tag = 2;
            foreach (GraphicNode node in Area) node.Tag = 0;

            //����㿪ʼ�������������������нڵ��Tag��Ϊ1
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

            //��Area�����б��Ϊ0�Ľڵ����AloneNodes
            AloneNodes.Clear();
            AloneNodes.Capacity = Area.Count - Count;
            foreach (GraphicNode node in Area) if (node.Tag == 0) AloneNodes.Add(node);
            if (AloneNodes.Count > 0) return 0;
            return 1;
        }

        /// <summary>
        /// ��ָ��������ʽ����һ��������
        /// </summary>
        /// <param name="beginNode">���</param>
        /// <param name="traversalMode">������ʽ</param>
        /// <param name="ctrl">����������ȫ�������ÿ�</param>
        /// <param name="linkUserDataToGraphicElement">True:���нڵ��UserData��������ͼ�Ľڵ���ߡ�false:���нڵ��UserData������ͼ�ڵ�ͱߵ�UserData</param>
        /// <returns>�����������ĸ��ڵ�</returns>
        public XTreeNode GetGenerationTree(GraphicNode startNode, TraversalModeEnum traversalMode, TraversalControl ctrl, bool linkUserDataToGraphicElement)
        {
            List<XTreeNode> treeNodes = new List<XTreeNode>(this.Nodes.Count);
            XTreeNode temp;

            //��������
            TraversalOperate op = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                //�������ڵ㲢��ӵ����ڵ��б���
                temp = new XTreeNode(3);
                treeNodes.Add(temp);

                //�����ڵ����
                if (path.Count > 0) temp.Father = treeNodes[path[0].OtherNode(node).TraversalFlag - 1];      //TraversalFlag��ʶ�˽ڵ�ı���˳�򣬹�ͨ���˱���жϽڵ��Ӧ�����ڵ���treeNodes�е�λ��

                //����UserData
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

            //����ͼ
            TraversalNode(traversalMode, startNode, op, ctrl, false);

            return treeNodes[0];
        }

        /// <summary>
        /// �õ�ͼ�в��γɻ���·������ʱҪ��ͼ��ͨ
        /// </summary>
        /// <returns>����ͼ�в��γɻ��Ľڵ㼯�ϼ��߼���</returns>
        public int SinglePathAnalysis()
        {
            List<object> SinglePathNodes = new List<object>(Nodes.Count);
            List<object> SinglePathEdges = new List<object>(Edges.Count);

            //��ÿ���ڵ��Tagֵ��Ϊ�䵱ǰ���ӵı������������бߵ�Tag���(��ʾ�ñ���Ч)����������һ�������ڵĽڵ㱣�浽�б��С�
            foreach (GraphicNode node in Nodes)
            {
                node.Tag = node.Edges.Count;
                if (node.Tag == 1) SinglePathNodes.Add(node);
            }
            foreach (GraphicEdge edge in Edges) edge.Tag = 1;

            //���б������нڵ㿪ʼ����������ǰ�ڵ��TagֵΪ1(������һ����Ч������),�򽫸���Ч�����˵Ľڵ�Tag��1�������ñ�����Ч���ٱ�������һ�˵Ľڵ㡣ֱ������Tagֵ����1�Ľڵ�ʱֹͣ��
            //ʵ�ʵ��㷨Ϊ���Ա���������ÿ���ڵ㣬���ж�ͨ���ı��Ƿ���Ч����Ч������Ч�������ڵ�Tag��1���ٱ����ýڵ㣻����Ч����ֱ�ӷ��ء�
            TraversalControl ctrl = delegate(GraphicNode node, List<GraphicEdge> path)
            {
                if (path.Count < 1) return TraversalControlEnum.Normal;        //PathΪ��ʱΪ�����׽ڵ㣬��Ϊ�˽ڵ��Ѿ����б����ˣ�������ӣ�����edge���������ڣ��಻��������á�����ֱ�ӷ��ؼ��ɡ�
                if (path[0].Tag == 0) return TraversalControlEnum.IgnoreChild;  //������˽ڵ�ͨ������Ч����������ء�
                path[0].Tag = 0;                                                //�õ���˽ڵ�ı�Ϊ��Ч��Ϊ�˱����Ų��ˣ���û��ȥ�������ֻ�ðѽ��ñ���Ч�Ĵ���������ˡ�
                SinglePathEdges.Add(path[0]);                                   //���߼����б�
                --node.Tag;                                                  //����ǰ�ڵ��Tag��1
                if (node.Tag > 1||node.Tag == 0) return TraversalControlEnum.Stop;          //����ǰ�ڵ㻹�ж�����Ч������������ֹ������
                SinglePathNodes.Add(node);                                   //���ڵ�����б�
                return TraversalControlEnum.Normal;
            };
            TraversalOperate op = delegate(GraphicNode node, List<GraphicEdge> path) { }; //�޲���

            for (int index = 0; index < SinglePathNodes.Count; ++index)
            {
                if (((GraphicNode)SinglePathNodes[index]).Tag == 1) TraversalNode(TraversalModeEnum.DFS, (GraphicNode)(SinglePathNodes[index]), op, ctrl, false);
            }
            _SinglePath = new List<object>[] { SinglePathNodes, SinglePathEdges };

            _SinglePathAnalysed = true;
            return 1;
        }

        #endregion

        #region ͼ�α༭�ӿ�

        /// <summary>
        /// ���ӽڵ�
        /// </summary>
        /// <param name="userData">Ŀ��ڵ�������û�����</param>
        /// <returns></returns>
        public GraphicNode AddNode(GraphicNode node)
        {
            node.Edges.Clear();
            this.Nodes.Add(node);
            this.SetValidFlag(false);
            return node;
        }

        /// <summary>
        /// ���ӱߣ����Զ����������ڵ�ı���Ϣ
        /// </summary>
        /// <param name="node1">�������ĵ�һ���ڵ����ݣ�ͼ�����ݴ�ֵ����Ӧ�������ı߹������ĸ�ͼ�ڵ㣩</param>
        /// <param name="node2">�������ĵڶ����ڵ����ݣ�ͼ�����ݴ�ֵ����Ӧ�������ı߹������ĸ�ͼ�ڵ㣩</param>
        /// <param name="userData">�߹���������</param>
        /// <returns></returns>
        public GraphicEdge AddEdge(object node1, object node2, object userData)
        {
            GraphicNode[] LinkNodes = new GraphicNode[2];

            int idx = 0;

            //���ҵ�һ���ڵ�
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

            //���ҵڶ����ڵ�
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
        /// ���ӱߣ����Զ����������ڵ�ı���Ϣ
        /// </summary>
        /// <param name="edge">�߶���</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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
        /// ���ӱߣ����Զ����������ڵ�ı���Ϣ���˺�����ִ����֤����Ӧȷ�������ݵĺϷ��ԣ������ڵ����ͼ��
        /// </summary>
        /// <param name="edge">�߶���</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool AddEdgeNoCheck(GraphicEdge edge)
        {
            edge.Nodes[0].Edges.Add(edge);
            edge.Nodes[1].Edges.Add(edge);
            Edges.Add(edge);

            this.SetValidFlag(false);

            return true;
        }

        /// <summary>
        /// �Ƴ��ڵ㣬�ɹ�ʱ����true
        /// </summary>
        /// <param name="node">Ҫ�Ƴ��Ľڵ�</param>
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
        /// ��ͼ���Ƴ��ߣ��ɹ��򷵻�true
        /// </summary>
        /// <param name="edge">Ŀ��</param>
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
        /// �Ƴ��ڵ㣬�ɹ�ʱ����true
        /// </summary>
        /// <param name="node">Ҫ�Ƴ��Ľڵ���Nodes�е�λ��</param>
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
        /// ��ͼ���Ƴ��ߣ��ɹ��򷵻�true
        /// </summary>
        /// <param name="edge">Ҫ�Ƴ��ı���Edges�е�λ��</param>
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

        #region ͼ��Ԫ�ز��ҽӿ�

        /// <summary>
        /// ����ָ��ƥ���������ҽڵ�
        /// </summary>
        /// <param name="match">ƥ������</param>
        /// <returns>���ط��������ĵ�һ���ڵ�</returns>
        public GraphicNode GetNode(Predicate<GraphicNode> match)
        {
            foreach (GraphicNode node in Nodes)
                if (match(node)) return node;
            return null;
        }

        /// <summary>
        /// ����ָ��ƥ����������Ԫ��
        /// </summary>
        /// <param name="match">ƥ������</param>
        /// <returns>���ط��������ĵ�һ����</returns>
        public GraphicEdge GetEdge(Predicate<GraphicEdge> match)
        {
            foreach (GraphicEdge edge in Edges)
                if (match(edge)) return edge;
            return null;
        }        

        /// <summary>
        /// ���Ҿ���ָ���û����ݵĽڵ㣨���������Ƿ���ͬ�жϣ�
        /// </summary>
        /// <param name="UserData">�û�����</param>
        /// <returns></returns>
        public GraphicNode GetNodeByUserData(object UserData)
        {
            foreach (GraphicNode node in this.Nodes) if (object.ReferenceEquals(node.UserObject, UserData)) return node;
            return null;
        }

        /// <summary>
        /// ���Ҿ���ָ���û����ݵıߣ����������Ƿ���ͬ�жϣ�
        /// </summary>
        /// <param name="UserData">�û�����</param>
        /// <returns></returns>
        public GraphicEdge GetEdgeByUserData(object UserData)
        {
            foreach (GraphicEdge edge in this.Edges) if (object.ReferenceEquals(edge.UserObject, UserData)) return edge;
            return null;
        }

        /// <summary>
        /// ȡһ���û������б��Ӧ�Ľڵ��б�,�����صĽڵ��б�˳�򲻻���UserDataList�е�����˳���Ӧ
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

        #region ��̬����

        #endregion
    }

    /// <summary>
    /// ������
    /// </summary>
    public class XTreeNode : ICloneable
    {
        #region ����

        /// <summary>
        /// �Ա��������о����Ľڵ�ִ�еķ���ԭ�͡�
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="counter">����������������ÿ����һ���ڵ�ʱ���������������ɹ�����ڱ���������ָ����</param>
        public delegate void TraversalOperate(XTreeNode node, int counter);

        /// <summary>
        /// ���Ʊ������̵ķ���ԭ�͡�
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="counter">����������������ÿ����һ���ڵ�ʱ���������������ɹ�����ڱ���������ָ����</param>
        /// <returns>ö��:Normal:����;IgnoreChild:�������ӽڵ�;Stop:��ֹ�������������ϱ����������κη�Normal�Ŀ������ֹͣ��</returns>
        public delegate TraversalControlEnum TraversalControl(XTreeNode node, int counter);

        /// <summary>
        /// ö��:�������̵Ŀ�ѡ���ơ�Normal:����;IgnoreChild:�������ӽڵ�;Stop:��ֹ�������������ϱ����������κη�Normal�Ŀ������ֹͣ��
        /// </summary>
        public enum TraversalControlEnum { Normal, IgnoreChild, Stop }

        /// <summary>
        /// ö��:���õı�����ʽ��Pre:�������;Post:�������;Up:���ⷽʽ(�����ӵ�ǰ�ڵ㵽���ڵ��·��);
        /// </summary>
        public enum TraversalModeEnum { Pre, Post, Up };

        #endregion

        #region ����

        //�û�����
        private int _Tag;

        //�ӽ�㼯
        private List<XTreeNode> _Sons;

        //�����
        private XTreeNode _Father;

        //�ýڵ�������û�����
        public object _NodeUserObject;

        //�ڵ������ڵ�ı߹������û�����
        public object _EdgeUserObject;

        #endregion

        #region ����

        /// <summary>
        /// �ؼ���
        /// </summary>
        public int Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// �ڵ�������û�����
        /// </summary>
        public object NodeUserObject
        {
            get { return _NodeUserObject; }
            set { _NodeUserObject = value; }
        }

        /// <summary>
        /// �ڵ������ڵ�ı߹������û�����
        /// </summary>
        public object EdgeUserObject
        {
            get { return _EdgeUserObject; }
            set { _EdgeUserObject = value; }
        }

        /// <summary>
        /// �ڵ��������ĸ��ڵ�
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
        /// �ڵ���ӽڵ��б�
        /// </summary>
        public List<XTreeNode> Sons
        {
            get
            {
                return _Sons;
            }
        }

        /// <summary>
        /// �ڵ�ĸ��ڵ㣬ע�⣺���ô�����ʱ�����Զ����ڵ���ӵ����ڵ���ӽڵ��б��С�
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

        #region ����

        #region ���캯��

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public XTreeNode()
        {
            _Sons = new List<XTreeNode>(2);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="EdgeCount">�߼���������Ԥ�ڵı�����</param>
        public XTreeNode(int SonCount)
        {
            _Sons = new List<XTreeNode>(SonCount);
        }

        /// <summary>
        /// �������ڵ��һ��������������ԭ�ڵ�ĸ��ӽڵ������
        /// </summary>
        /// <param name="Other">��Դ����</param>
        /// <param name="CopyUserObject">�Ƿ񿽱��û�����</param>
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

        #region ���ƺ���

        /// <summary>
        /// �������ڵ��һ������������ԭ�ڵ�Ľڵ����ݺͱ�������Ϣ��������ԭ�ڵ�ĸ��ӽڵ������
        /// </summary>
        /// <returns>�µĿ�������</returns>
        public object Clone()
        {
            return new XTreeNode(this, true);
        }

        /// <summary>
        /// ����ͼ�ڵ��һ��������
        /// </summary>
        /// <param name="CopyTag">�Ƿ񿽱��û����ݡ�</param>
        /// <returns>�µĿ�������</returns>
        public object Clone(Boolean CopyUserObject)
        {
            return new XTreeNode(this, CopyUserObject);
        }

        /// <summary>
        /// �ӽڵ��������п�¡��һ���Դ˽ڵ�Ϊ���ڵ�����������������ڵ㱣����ԭʼ�ڵ���ͬ���û����ݡ�
        /// </summary>
        /// <returns></returns>
        public XTreeNode CloneTree()
        {
            int NodeCount = 0;              //�����Ľڵ���
            List<XTreeNode> NewNodeList;

            //�Խڵ���б�Ų�����
            TraversalOperate op = delegate(XTreeNode node, int Counter)
            {
                node.Tag = Counter;
                ++NodeCount;
            };

            //�������нڵ㰴�������˳����б�ţ���ʼ���Ϊ0.���Ժ�����������нڵ��¡��NewNodeList��ʱ�����нڵ��Tagֵ��������NewNodeList�е�������ͬ
            this.Traversal(TraversalModeEnum.Pre, op, 0, 1);

            //��ʼ������
            NewNodeList = new List<XTreeNode>(NodeCount);

            //�������нڵ���п�¡����������NewNodeList��������ÿ���ڵ�ĸ��ڵ��Tagֵ����NewNodeList���½ڵ������ù�ϵ
            op = delegate(XTreeNode node, int Counter)
            {
                //���ڵ��¡��NewNodeList
                NewNodeList.Add((XTreeNode)(node.Clone()));
                //�ԷǸ��ڵ㣬�����丸����
                if (node.Tag != 0) NewNodeList[node.Tag].Father = NewNodeList[node.Father.Tag];
            };

            this.Traversal(TraversalModeEnum.Pre, op, 0, 0);

            return NewNodeList[0];
        }

        #endregion

        /// <summary>
        /// �������ڵ���·��
        /// </summary>
        /// <param name="BeginNode">���</param>
        /// <param name="EndNode">�յ�</param>
        /// <param name="UserPath">���ص�·������ʽ��true:�û������гɵ�·����false:���ڵ��гɵ�·����</param>
        /// <returns>�û�����·��������һ����������Ԫ�ص����飬�ֱ�Ϊ·���нڵ�����Ľڵ����ݼ��ϡ�·���нڵ�����ı����ݼ��ϡ����ڵ�·��������һ����������Ԫ�ص����飬��Ԫ��Ϊһ�����ڵ��б������ڵ㲻��ͬһ�����У��˺�������null��</returns>
        /// ע����������·���и����߹������û����ݣ����������UserPath����Ϊ���޷��ж�һ�����ڵ��б����ĸ��ڵ���������֧·�Ķ��㡣
        public static List<Object>[] GetPath(XTreeNode BeginNode, XTreeNode EndNode, bool UserPath)
        {
            //TreeNode.TraversalOperate op = new TraversalOperate(delegate(TreeNode node,int Counter) //OK  

            XTreeNode JoinNode = null;   //����
            int LBLen = 0;      //��֧���ȡ�1.����������ڵ��·���ڵ�����2.����������·���ڵ���
            int NodeCount = 0;  //�ڵ�����1.��¼�ܵı��������о����Ľڵ��� 2.����·���Ľڵ���

            List<object> NodeList; //��㵽���ڵ��·���нڵ���Ϣ
            List<object> EdgeList;   //·�������ı��б�

            //����У��
            if (BeginNode == null || EndNode == null) return null;

            //����㿪ʼ���ϱ���ʱִ�еĲ���
            XTreeNode.TraversalOperate BOp = delegate(XTreeNode node, int Counter)
            {
                node.Tag = Counter; //��ǲ�����
                ++NodeCount;
            };

            //��ĩ�ڵ㿪ʼ���ϱ���ʱ���еĲ���
            XTreeNode.TraversalOperate EOp = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag == 0)   //���ڵ�δ��ǣ����ǲ�����
                {
                    node.Tag = Counter;
                    ++NodeCount;
                }
                else                //���ڵ��ѱ�ǣ���Ϊ����
                {
                    JoinNode = node;
                }
            };

            //��ĩ�ڵ����ϱ���ʱ�Ŀ��ƣ��������ù�Tag�Ľڵ�ʱֹͣ
            XTreeNode.TraversalControl control = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag > 0) return TraversalControlEnum.Stop;
                return TraversalControlEnum.Normal;
            };

            //���ʼĩ�˽ڵ������ڵ�·���и��ڵ��Tag
            //TraversalOperate setTag = new TraversalOperate(TreeNode.SetTag);
            BeginNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);
            EndNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);

            //��¼��������ڵ��·�������б�ǣ�Tag��1��ʼ������                      
            BeginNode.Traversal(TraversalModeEnum.Up, BOp, 1, 1); //ʹ�õ�����������Ϊ�˼���·������
            LBLen = NodeCount;

            //���յ������ߣ���·���ڵ���б�ǡ�������������Tag�Ľڵ㣨������·�����㣩ʱֹͣ��
            EndNode.Traversal(TraversalModeEnum.Up, EOp, control, 1, 0);

            //����ͬһ������ʱ����null
            if (JoinNode == null) return null;

            //��ʼ������
            NodeCount = NodeCount - (JoinNode.Tag - LBLen);     //�˼����NodeCount��ʾ����·���Ľڵ���
            LBLen = JoinNode.Tag;                               //�˼����BRLen��ʾ���������·���Ľڵ���
            NodeList = new List<object>(NodeCount + 1);         //������ظ���ӣ���Ҫ�౸һ���ڵ���ڴ�


            //��������Tag
            JoinNode.Tag = 0;

            //����Tag������Ľڵ�ʱֹͣ
            control = delegate(XTreeNode node, int Counter)
            {
                if (node.Tag == 0) return TraversalControlEnum.Stop;
                return TraversalControlEnum.Normal;
            };

            //���ڵ�����б�
            TraversalOperate op = delegate(XTreeNode node, int Counter)
            {
                NodeList.Add(node);
            };

            //������������·���и��ڵ�����б�(��������)
            BeginNode.Traversal(TraversalModeEnum.Up, op, control, 0, 0);

            //���յ��������·���и��ڵ�����б�(��������)
            EndNode.Traversal(TraversalModeEnum.Up, op, control, 0, 0);

            //ȥ���ظ���ӵĽ���
            NodeList.RemoveAt(NodeCount);

            //���յ��������·����ת
            NodeList.Reverse(LBLen - 1, NodeCount - LBLen);

            if (UserPath)
            {
                //��ʼ��������
                EdgeList = new List<object>(NodeCount - 1);

                //���ɱ߼�(�ų�����)
                for (int index = 0; index < LBLen - 1; ++index) EdgeList.Add(((XTreeNode)(NodeList[index])).EdgeUserObject);
                for (int index = LBLen; index < NodeList.Count; ++index) EdgeList.Add(((XTreeNode)(NodeList[index])).EdgeUserObject);

                //��BRNodeList�б���Ľڵ��滻Ϊ�ڵ��е�NodeUserObject
                for (int index = 0; index < NodeList.Count; ++index) NodeList[index] = ((XTreeNode)(NodeList[index])).NodeUserObject;

                return new List<object>[] { NodeList, EdgeList };
            }
            else
            {
                return new List<object>[] { NodeList };
            }
        }

        /// <summary>
        /// ���ڵ��Tag��Ϊָ��ֵ
        /// </summary>
        /// <param name="node">Ŀ��ڵ�</param>
        /// <param name="Tag">Tagֵ</param>
        public static void SetTag(XTreeNode node, int Tag)
        {
            node.Tag = Tag;
        }

        /// <summary>
        /// �ı�ڵ��Tagֵ��
        /// </summary>
        /// <param name="node">Ŀ��ڵ�</param>
        /// <param name="Num">Tag����</param>
        public static void TagIncrease(XTreeNode node, int Num)
        {
            node.Tag += Num;
        }

        #region ��������

        /// <summary>
        /// �����нڵ㰴ָ������˳��ִ��op������
        /// </summary>
        /// <param name="mode">������ʽ</param>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
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
        /// �����нڵ㰴ָ������˳��ִ��op��������ͨ��������Ӱ��������̡�
        /// </summary>
        /// <param name="mode">������ʽ</param>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="control">������</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
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
        /// ��һ���ڵ��������һ���ڵ�
        /// </summary>
        /// <param name="BeginNode">��ʼ�ڵ�</param>
        /// <param name="EndNode">��ֹ�ڵ�</param>
        /// <param name="IgnoreTopNode">��ʶ�Ƿ�Ҫ����·���еĽڵ��У�������λ����ߵĽڵ㣨��·���Ĺյ㣩</param>
        /// <param name="op">�Ա������Ľڵ�Ҫִ�еĲ���</param>
        public int Traversal(XTreeNode BeginNode, XTreeNode EndNode, bool IgnoreTopNode,TraversalOperate op)
        {
            XTreeNode JoinNode = null;   //����
            List<XTreeNode> NodeList = new List<XTreeNode>(3); //�յ�������Ľڵ��б����������㣩

            XTreeNode.TraversalControl ctrl;
            XTreeNode.TraversalOperate InnelOp;

            //����У��
            if (BeginNode == null || EndNode == null || op == null) return -1;           

            //���ĩ�˽ڵ������ڵ�·���и��ڵ��Tag
            EndNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 0, 0);

            //����������ڵ��·�����б��                      
            BeginNode.Traversal(TraversalModeEnum.Up, XTreeNode.SetTag, 1, 0); //ʹ�õ�����������Ϊ�˼���·������

            #region ���յ������ߣ���·���ڵ���б�ǡ�������������Tag�Ľڵ㣨������·�����㣩ʱֹͣ��
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

            //����ͬһ������ʱ���ش������
            if (JoinNode == null) return -1;

            //Ҫ����Զ���ʱ��������Tag��������򽫽���ĸ��ڵ��Tag���
            if(IgnoreTopNode) 
                JoinNode.Tag = 0;
            else
            {
                if (JoinNode.Father != null) JoinNode.Father.Tag = 0;
            }

            #region ��·�����б���

            //����㿪ʼ���ϱ���ִ���û��ı�������������Tag������Ľڵ�ʱֹͣ
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
                //����㿪ʼ����ִ���û��ı�������
                BeginNode.Traversal(TraversalModeEnum.Up, InnelOp, ctrl, 0, 0);
            }

            //���յ��뽻��֮���·�������������㣩ִ�д������µı�������ִ���û�����
            NodeList.Reverse();
            foreach (XTreeNode node in NodeList) op(node, 0);

            #endregion

            return 1;
        }

        /// <summary>
        /// ǰ������������нڵ㰴����˳��ִ��op������
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
        private void PreOTraversal(TraversalOperate op, ref int Counter, int Step)
        {
            op(this, Counter);
            Counter += Step;
            foreach (XTreeNode son in Sons) son.PreOTraversal(op, ref Counter, Step);
        }

        /// <summary>
        /// ��������������нڵ㰴����˳��ִ��op������
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
        private void PostOTraversal(TraversalOperate op, ref int Counter, int Step)
        {
            foreach (XTreeNode Son in Sons) Son.PostOTraversal(op, ref Counter, Step);
            op(this, Counter);
            Counter += Step;
        }

        /// <summary>
        /// ǰ������������нڵ㰴����˳��ִ��op������
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        private void PreOTraversal(TraversalOperate op)
        {
            op(this, 0);
            foreach (XTreeNode Son in Sons) Son.PreOTraversal(op);
        }

        /// <summary>
        /// ��������������нڵ㰴����˳��ִ��op������
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        private void PostOTraversal(TraversalOperate op)
        {
            foreach (XTreeNode Son in Sons) Son.PostOTraversal(op);
            op(this, 0);

        }

        /// <summary>
        /// ǰ������������нڵ㰴����˳��ִ��op��������ͨ��ControlӰ��������̡�
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="control">������</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
        /// <returns>���ڿ��Ʊ������̣������ע�˷���ֵ</returns>
        private bool PreOTraversal(TraversalOperate op, TraversalControl control, ref int Counter, int Step)
        {
            bool Result = true;

            //ִ�б�������
            op(this, Counter);
            Counter += Step;

            //����
            switch (control(this, Counter - Step))
            {
                case TraversalControlEnum.Stop:     //����false
                    Result = false;
                    break;

                case TraversalControlEnum.IgnoreChild:  //�����ӽڵ���б���
                    break;

                case TraversalControlEnum.Normal:       //���ӽڵ���б���
                    foreach (XTreeNode son in Sons)
                    {
                        if (!son.PreOTraversal(op, control, ref Counter, Step))     //��ĳ�ӽڵ������ֹ�������򲻶�ʣ�µ��ӽڵ���б�������ͨ������false��֮���ڵ���ֹ������
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
        /// ��������������нڵ㰴����˳��ִ��op��������ͨ��ControlӰ��������̡�
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="control">������</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
        /// <returns>���ڿ��Ʊ������̣������ע�˷���ֵ</returns>
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

                case TraversalControlEnum.Normal:   //�����ӽڵ�
                    foreach (XTreeNode son in Sons)
                    {
                        if (!son.PostOTraversal(op, control, ref Counter, Step))    //��ĳ�ӽڵ������ֹ�������򲻶�ʣ�µ��ӽڵ���б�������ͨ������false��֮���ڵ���ֹ������
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
        /// �Դӵ�ǰ�ڵ㵽���ڵ��·���нڵ�ִ��op������ͨ���������ɿ��Ʊ������̡�
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="control">������</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
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
        /// �Դӵ�ǰ�ڵ㵽���ڵ��·���нڵ�ִ��op����
        /// </summary>
        /// <param name="op">�Խڵ�ִ�еĲ���</param>
        /// <param name="Counter">��������ֵ</param>
        /// <param name="Step">����������ֵ</param>
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