/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：SearchAlgorithm.cs

 * 说明：搜索算法

 * 作者：叶道全
 
 * 时间：2009年4月29日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 搜索算法
    /// </summary>
    public class SearchAlgorithm
    {
        /// <summary>
        /// 折半查找的Match参数原型
        /// </summary>
        /// <param name="Index">BiSearch生成的索引</param>
        /// <returns>0:索引处的数据匹配成功;1:要寻找的数据在索引与上界之间;-1:要寻找的数据在索引与下限之间</returns>
        public delegate int BiSearchMatch(int Index);

        /// <summary>
        /// 折半查找：此函数将根据上下界按折半查找规则生成可能的索引，并使用生成的索引逐个调用Match委托，直到匹配成功或没有其它可能的索引。
        /// </summary>
        /// <param name="LBound">下界</param>
        /// <param name="UBound">上界</param>
        /// <param name="Match">匹配委托</param>
        /// <returns>若返回值在(LBound,UBound)区间内，则查找成功且返回值即为要寻找的索引;若返回值小于下限，则表示未找到要寻找的索引。</returns>
        public static int BiSearch(int LBound, int UBound, BiSearchMatch Match)
        {
            int Mid,Result,TempLBound = LBound;

            if (LBound > UBound || Match == null) return LBound - 1;

            while(LBound <= UBound)
            {
                Mid = LBound + ((UBound - LBound) >> 1);
                Result = Match(Mid);

                if (Result == 0)
                    return Mid;
                else if (Result > 0)
                    LBound = Mid + 1;
                else
                    UBound = Mid - 1;
            }

            return TempLBound - 1;
        }

        /// <summary>
        /// 使用指定的比较器来从列表中获取极大值与极小值
        /// </summary>
        /// <typeparam name="T">要比较的目标数据类型</typeparam>
        /// <param name="Enumerator">要从中取极值的数据列表的迭代器</param>
        /// <param name="Comparer">数据的比较器</param>
        /// <returns>未定义</returns>
        public static int GetMaxMin<T>(IEnumerator<T> Enumerator,Comparison<T> Comparer, ref T Max,ref T Min)
        {
            if (Enumerator.MoveNext())
            {
                Max = Enumerator.Current;
                Min = Max;
            }

            while (Enumerator.MoveNext())
            {
                if (Comparer(Enumerator.Current, Max) > 0)
                {
                    Max = Enumerator.Current;
                }
                else if (Comparer(Enumerator.Current, Min) < 0)
                {
                    Min = Enumerator.Current;
                }
            }
            return 1;
        }

        /// <summary>
        /// 使用比较器来获取极大（小）值在源中的索引
        /// </summary>
        /// <param name="LBound">源索引的下限</param>
        /// <param name="UBound">源索引的上限</param>
        /// <param name="Comparer">比较器，请注意此比较器获得的参数为索引</param>
        /// <param name="MaxIdx">极大值索引</param>
        /// <param name="MinIdx">极小值索引</param>
        /// <returns></returns>
        public static int GetIndexOfMaxMin(int LBound,int UBound,Comparison<int> Comparer, ref int MaxIdx,ref int MinIdx)
        {
            MaxIdx = -1;
            MinIdx = -1;

            if (LBound > UBound || Comparer == null) return 0;

            MaxIdx = LBound;
            MinIdx = LBound;

            for (int Idx = LBound; Idx <= UBound; ++Idx)
            {
                if (Comparer(Idx, MaxIdx) > 0)
                {
                    MaxIdx = Idx;
                }
                else if (Comparer(Idx, MinIdx) < 0)
                {
                    MinIdx = Idx;
                }
            }

            return 1;
        }
    }
}
