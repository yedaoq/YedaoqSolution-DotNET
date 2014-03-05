using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    

    /// <summary>
    /// 组合算法
    /// </summary>
    public class AlgorithmCombination
    {
        /// <summary>
        /// 组合处理的委托
        /// </summary>
        /// <typeparam name="T">组合的数据类型</typeparam>
        /// <param name="Combination">组合</param>
        /// <returns>true:继续产生组合;false:终止产生组合</returns>
        public delegate bool DelegateCombinationDeal<T>(List<T> Combination);

        /// <summary>
        /// 组合生成：从每组数据中取一个数据进行组合
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="GroupingData">分组数据</param>
        /// <param name="DealOp">处理操作</param>
        /// <returns>返回生成的组合数</returns>
        public static int GroupingdataCombinationGenerate<T>(IEnumerable<IEnumerable<T>> GroupingData,DelegateCombinationDeal<T> DealOp)
        {
            //结果，组数（第一维长度）
            int Result = 0;

            List<IEnumerator<T>> groupingData = new List<IEnumerator<T>>();
            List<T> Combination = new List<T>();

            bool NotEnd = true;

            #region 初始化

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

                #region 生成下一个组合

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
                            //进位
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
        /// 组合生成：从每组数据中取一个数据进行组合,GroupingdataCombinationGenerate<T>的非泛型版本,要求GroupingData中每个成员都能转换为IEnumerable
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="GroupingData">分组数据</param>
        /// <param name="DealOp">处理操作</param>
        /// <returns>返回生成的组合数</returns>
        public static int GroupingdataCombinationGenerate(IEnumerable GroupingData, DelegateCombinationDeal<object> DealOp)
        {
            //结果，组数（第一维长度）
            int Result = 0;

            List<IEnumerator> groupingData = new List<IEnumerator>();
            List<object> Combination = new List<object>();

            bool NotEnd = true;

            //初始化
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

                #region 生成下一个组合

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
                            //进位
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
