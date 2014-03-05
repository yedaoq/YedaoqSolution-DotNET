using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// ���������㷨
    /// </summary>
    public class QuickSorter
    {
        public delegate int CompareDelegate(int IdxA, int IdxB);

        public delegate void SwapDelegate(int IdxA, int IdxB);

        private CompareDelegate Comparer;

        private SwapDelegate Swaper;

        public int QuickSort(int LBound, int UBound, CompareDelegate Comparer, SwapDelegate Swaper)
        {
            this.Comparer = Comparer;
            this.Swaper = Swaper;

            QuickSort(LBound, UBound);

            return 1;
        }

        private int QuickSort(int LBound, int UBound)
        {
            if (UBound - LBound < 1) return 0;

            int ForwardPointr = LBound, BackwardPointer = UBound + 1;

            //ע���Ե�һ��Ԫ��Ϊ�ο�

            while (ForwardPointr < BackwardPointer)
            {
                //��ǰ���ҵ�һ�����ڲο�����
                while (++ForwardPointr < BackwardPointer && Comparer(LBound, ForwardPointr) >= 0) ;

                //�Ӻ����ҵ�һ��С�ڵ��ڲο�����
                while (--BackwardPointer > ForwardPointr && Comparer(LBound, BackwardPointer) < 0) ;

                //����ҵ����򽻻�ֵ
                if (ForwardPointr < BackwardPointer) Swaper(BackwardPointer, ForwardPointr);
            }

            //���ο�ֵ�����м�
            Swaper(LBound, --ForwardPointr);

            QuickSort(LBound, ForwardPointr - 1);
            QuickSort(ForwardPointr + 1, UBound);

            return 1;
        }
    }
}
