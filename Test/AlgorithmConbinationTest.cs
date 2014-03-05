using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;

namespace Test
{
    class AlgorithmConbinationTest
    {
        public static void Test()
        {
            Random Random = new Random(5);

            for (int j = 0; j < 10; j++)
            {
                int[][] Array = new int[Random.Next(1, 5)][];

                Console.WriteLine("===================== 分组组合算法测试(GroupingdataCombinationGenerate) ==========================");

                for (int idx = 0; idx < Array.Length; ++idx)
                {
                    Array[idx] = new int[Random.Next(1, 5)];
                    for (int i = 0; i < Array[idx].Length; i++)
                    {
                        Array[idx][i] = Random.Next();
                    }

                    Console.WriteLine("数组 : " + StringAssembler.StringSplice("   ", Array[idx]));
                }

                AlgorithmCombination.GroupingdataCombinationGenerate(Array, Print);
            }
        }

        public static bool Print(List<int> Combo)
        {
            Console.WriteLine("组合 : ");
            foreach (int i in Combo)
            {
                Console.Write(i.ToString());
                Console.Write("   ");
            }
            Console.WriteLine();
            return true;
        }
    }
}
