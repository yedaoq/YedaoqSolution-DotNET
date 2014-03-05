using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    

    /// <summary>
    /// ����㷨
    /// </summary>
    public class AlgorithmCombination
    {
        /// <summary>
        /// ��ϴ����ί��
        /// </summary>
        /// <typeparam name="T">��ϵ���������</typeparam>
        /// <param name="Combination">���</param>
        /// <returns>true:�����������;false:��ֹ�������</returns>
        public delegate bool DelegateCombinationDeal<T>(List<T> Combination);

        /// <summary>
        /// ������ɣ���ÿ��������ȡһ�����ݽ������
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="GroupingData">��������</param>
        /// <param name="DealOp">�������</param>
        /// <returns>�������ɵ������</returns>
        public static int GroupingdataCombinationGenerate<T>(IEnumerable<IEnumerable<T>> GroupingData,DelegateCombinationDeal<T> DealOp)
        {
            //�������������һά���ȣ�
            int Result = 0;

            List<IEnumerator<T>> groupingData = new List<IEnumerator<T>>();
            List<T> Combination = new List<T>();

            bool NotEnd = true;

            #region ��ʼ��

            foreach (IEnumerable<T> group in GroupingData)
            {
                IEnumerator<T> Enumerator = group.GetEnumerator();
                if (!Enumerator.MoveNext()) return Result;

                groupingData.Add(Enumerator);
                Combination.Add(Enumerator.Current);
            }

            if (groupingData.Count == 0) return Result;

            groupingData.TrimExcess();
            Combination.TrimExcess();

            #endregion

            do
            {
                ++Result;

                DealOp(Combination);

                #region ������һ�����

                for (int idx = groupingData.Count - 1; idx >= 0; --idx)
                {
                    if (groupingData[idx].MoveNext())
                    {
                        Combination[idx] = groupingData[idx].Current;
                        break;
                    }
                    else
                    {
                        if (idx == 0)
                        {
                            NotEnd = false;
                            break;
                        }
                        else
                        {
                            //��λ
                            groupingData[idx].Reset();
                            groupingData[idx].MoveNext();
                            Combination[idx] = groupingData[idx].Current;
                        }
                    }
                }

                #endregion

            } while (NotEnd);

            return Result;
        }

        /// <summary>
        /// ������ɣ���ÿ��������ȡһ�����ݽ������,GroupingdataCombinationGenerate<T>�ķǷ��Ͱ汾,Ҫ��GroupingData��ÿ����Ա����ת��ΪIEnumerable
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="GroupingData">��������</param>
        /// <param name="DealOp">�������</param>
        /// <returns>�������ɵ������</returns>
        public static int GroupingdataCombinationGenerate(IEnumerable GroupingData, DelegateCombinationDeal<object> DealOp)
        {
            //�������������һά���ȣ�
            int Result = 0;

            List<IEnumerator> groupingData = new List<IEnumerator>();
            List<object> Combination = new List<object>();

            bool NotEnd = true;

            //��ʼ��
            foreach (object group in GroupingData)
            {
                if(!(group is IEnumerable)) return Result;

                IEnumerator Enumerator = (group as IEnumerable).GetEnumerator();
                if (!Enumerator.MoveNext()) return Result;

                groupingData.Add(Enumerator);
                Combination.Add(Enumerator.Current);
            }

            if (groupingData.Count == 0) return Result;

            groupingData.TrimExcess();
            Combination.TrimExcess();

            do
            {
                ++Result;

                DealOp(Combination);

                #region ������һ�����

                for (int idx = groupingData.Count - 1; idx >= 0; --idx)
                {
                    if (groupingData[idx].MoveNext())
                    {
                        Combination[idx] = groupingData[idx].Current;
                        break;
                    }
                    else
                    {
                        if (idx == 0)
                        {
                            NotEnd = false;
                            break;
                        }
                        else
                        {
                            //��λ
                            groupingData[idx].Reset();
                            groupingData[idx].MoveNext();
                            Combination[idx] = groupingData[idx].Current;
                        }
                    }
                }

                #endregion

            } while (NotEnd);

            return Result;
        }    
    }
}
