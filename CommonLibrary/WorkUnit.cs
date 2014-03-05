using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationInterface.CompanyImpedanceControl
{
    /// <summary>
    /// 表示工作单元完成状态的枚举
    /// </summary>
    public enum WorkUnitResultEnum
    {
        /// <summary>
        /// 工作正常完成
        /// </summary>
        Complete,

        /// <summary>
        /// 工作取消
        /// </summary>
        Cancel
    }

    /// <summary>
    /// 工作完成的事件参数
    /// </summary>
    public class WorkUnitEndEventArgs:EventArgs
    {
        /// <summary>
        /// 工作完成状态
        /// </summary>
        public readonly WorkUnitResultEnum WorkResult;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Result"></param>
        public WorkUnitEndEventArgs(WorkUnitResultEnum Result)
        {
            WorkResult = Result;
        }
    }

    /// <summary>
    /// 工作完成的响应委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void WorkUnitEndEventHandler(object sender,WorkUnitEndEventArgs e);

    /// <summary>
    /// 工作单元中要执行操作的委托
    /// </summary>
    /// <param name="WorkUnit"></param>
    public delegate void WorkUnitOpDelegate(WorkUnit WorkUnit,object Parameter);

    /// <summary>
    /// 工作单元
    /// </summary>
    public class WorkUnit
    {
        /// <summary>
        /// 要执行的操作
        /// </summary>
        private WorkUnitOpDelegate Op;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Op"></param>
        public WorkUnit(WorkUnitOpDelegate Op)
        {
            this.Op = Op;
        }

        /// <summary>
        /// 工作开始
        /// </summary>
        /// <returns></returns>
        public int BeginWork(object Parameter)
        {
            Op(this,Parameter);
            return 1;
        }

        /// <summary>
        /// 工作完成
        /// </summary>
        /// <param name="Result"></param>
        public void EndWork(WorkUnitResultEnum Result)
        {
            if(_WorkEnd != null) _WorkEnd(this,new WorkUnitEndEventArgs(Result));
        }
        
        /// <summary>
        /// 工作完成时触发的事件
        /// </summary>
        private event WorkUnitEndEventHandler _WorkEnd;

        /// <summary>
        /// 此事件由等待工作完成的代码响应
        /// </summary>
        public event WorkUnitEndEventHandler WorkEnd
        {
            add
            {
                _WorkEnd += value;
            }
            remove
            {
                _WorkEnd -= value;
            }
        }
    }


    public class WorkChain
    {
        private WorkUnit[] WorkUnits;

        private int CurrentWorkIdx = -1;

        private object Parameter;

        private WorkUnitResultEnum CurrentWorkResult = WorkUnitResultEnum.Complete;

        private bool _IsComplete = false;

        public bool IsComplete
        {
            get { return _IsComplete; }
        }

        public WorkChain(params WorkUnit[] WorkUnits)
        {
            this.WorkUnits = WorkUnits;
        }

        public int Start(object Parameter)
        {
            this.Parameter = Parameter;

            WorkCycle(null, null);

            return 1;
        }

        private void WorkCycle(object sender, WorkUnitEndEventArgs e)
        {
            if (e != null)
            {
                WorkUnits[CurrentWorkIdx].WorkEnd -= WorkCycle;
                CurrentWorkResult = e.WorkResult;
                if (CurrentWorkResult == WorkUnitResultEnum.Cancel) return;
            }

            if (++CurrentWorkIdx > WorkUnits.Length) return;

            WorkUnits[CurrentWorkIdx].WorkEnd += WorkCycle;

            WorkUnits[CurrentWorkIdx].BeginWork(Parameter);
        }
    }
}
