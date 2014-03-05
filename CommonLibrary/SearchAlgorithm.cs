/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��SearchAlgorithm.cs

 * ˵���������㷨

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��4��29��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// �����㷨
    /// </summary>
    public class SearchAlgorithm
    {
        /// <summary>
        /// �۰���ҵ�Match����ԭ��
        /// </summary>
        /// <param name="Index">BiSearch���ɵ�����</param>
        /// <returns>0:������������ƥ��ɹ�;1:ҪѰ�ҵ��������������Ͻ�֮��;-1:ҪѰ�ҵ�����������������֮��</returns>
        public delegate int BiSearchMatch(int Index);

        /// <summary>
        /// �۰���ң��˺������������½簴�۰���ҹ������ɿ��ܵ���������ʹ�����ɵ������������Matchί�У�ֱ��ƥ��ɹ���û���������ܵ�������
        /// </summary>
        /// <param name="LBound">�½�</param>
        /// <param name="UBound">�Ͻ�</param>
        /// <param name="Match">ƥ��ί��</param>
        /// <returns>������ֵ��(LBound,UBound)�����ڣ�����ҳɹ��ҷ���ֵ��ΪҪѰ�ҵ�����;������ֵС�����ޣ����ʾδ�ҵ�ҪѰ�ҵ�������</returns>
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
        /// ʹ��ָ���ıȽ��������б��л�ȡ����ֵ�뼫Сֵ
        /// </summary>
        /// <typeparam name="T">Ҫ�Ƚϵ�Ŀ����������</typeparam>
        /// <param name="Enumerator">Ҫ����ȡ��ֵ�������б�ĵ�����</param>
        /// <param name="Comparer">���ݵıȽ���</param>
        /// <returns>δ����</returns>
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
        /// ʹ�ñȽ�������ȡ����С��ֵ��Դ�е�����
        /// </summary>
        /// <param name="LBound">Դ����������</param>
        /// <param name="UBound">Դ����������</param>
        /// <param name="Comparer">�Ƚ�������ע��˱Ƚ�����õĲ���Ϊ����</param>
        /// <param name="MaxIdx">����ֵ����</param>
        /// <param name="MinIdx">��Сֵ����</param>
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
