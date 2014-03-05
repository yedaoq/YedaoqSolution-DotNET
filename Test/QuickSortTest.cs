using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class QuickSortTest
    {
        static byte[] Array;

        static Random Random = new Random();

        static QuickSorter QuickSorter = new QuickSorter();

        public static void Swap(int IdxA, int IdxB)
        {
            byte Temp = Array[IdxA];
            Array[IdxA] = Array[IdxB];
            Array[IdxB] = Temp;
        }

        public static int Compare(int IdxA, int IdxB)
        {
            return Array[IdxA] - Array[IdxB];
        }

        public static void Test()
        {
            int TestTimes = 1000;

            while (--TestTimes > 0)
            {
                Array = new byte[Random.Next(50)];

                Random.NextBytes(Array);

                Console.Write("\r\nBefore Sort : ");

                foreach (byte Data in Array)
                {
                    Console.Write(Data.ToString() + '\t');
                }

                QuickSorter.QuickSort(0, Array.Length - 1, Compare, Swap);

                Console.Write("\r\nAfter Sort : ");

                foreach (byte Data in Array)
                {
                    Console.Write(Data.ToString() + '\t');
                }

                if (Array.Length <= 1) continue;

                for (int Idx = 0; Idx < Array.Length - 1; Idx++)
                {
                    if (Array[Idx] > Array[Idx + 1])
                        Console.Write("\r\nSort Error");
                }
            }
        }
    }
}
