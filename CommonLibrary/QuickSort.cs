using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 快速排序算法
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

            //注：以第一个元素为参考

            while (ForwardPointr < BackwardPointer)
            {
                //从前往找第一个大于参考的数
                while (++ForwardPointr < BackwardPointer && Comparer(LBound, ForwardPointr) >= 0) ;

                //从后往找第一个小于等于参考的数
                while (--BackwardPointer > ForwardPointr && Comparer(LBound, BackwardPointer) < 0) ;

                //如果找到，则交换值
                if (ForwardPointr < BackwardPointer) Swaper(BackwardPointer, ForwardPointr);
            }

            //将参考值换到中间
            Swaper(LBound, --ForwardPointr);

            QuickSort(LBound, ForwardPointr - 1);
            QuickSort(ForwardPointr + 1, UBound);

            return 1;
        }
    }
}
