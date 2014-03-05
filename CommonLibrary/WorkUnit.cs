using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationInterface.CompanyImpedanceControl
{
    /// <summary>
    /// ��ʾ������Ԫ���״̬��ö��
    /// </summary>
    public enum WorkUnitResultEnum
    {
        /// <summary>
        /// �����������
        /// </summary>
        Complete,

        /// <summary>
        /// ����ȡ��
        /// </summary>
        Cancel
    }

    /// <summary>
    /// ������ɵ��¼�����
    /// </summary>
    public class WorkUnitEndEventArgs:EventArgs
    {
        /// <summary>
        /// �������״̬
        /// </summary>
        public readonly WorkUnitResultEnum WorkResult;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Result"></param>
        public WorkUnitEndEventArgs(WorkUnitResultEnum Result)
        {
            WorkResult = Result;
        }
    }

    /// <summary>
    /// ������ɵ���Ӧί��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void WorkUnitEndEventHandler(object sender,WorkUnitEndEventArgs e);

    /// <summary>
    /// ������Ԫ��Ҫִ�в�����ί��
    /// </summary>
    /// <param name="WorkUnit"></param>
    public delegate void WorkUnitOpDelegate(WorkUnit WorkUnit,object Parameter);

    /// <summary>
    /// ������Ԫ
    /// </summary>
    public class WorkUnit
    {
        /// <summary>
        /// Ҫִ�еĲ���
        /// </summary>
        private WorkUnitOpDelegate Op;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Op"></param>
        public WorkUnit(WorkUnitOpDelegate Op)
        {
            this.Op = Op;
        }

        /// <summary>
        /// ������ʼ
        /// </summary>
        /// <returns></returns>
        public int BeginWork(object Parameter)
        {
            Op(this,Parameter);
            return 1;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="Result"></param>
        public void EndWork(WorkUnitResultEnum Result)
        {
            if(_WorkEnd != null) _WorkEnd(this,new WorkUnitEndEventArgs(Result));
        }
        
        /// <summary>
        /// �������ʱ�������¼�
        /// </summary>
        private event WorkUnitEndEventHandler _WorkEnd;

        /// <summary>
        /// ���¼��ɵȴ�������ɵĴ�����Ӧ
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
