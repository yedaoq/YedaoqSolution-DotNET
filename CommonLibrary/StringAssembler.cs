using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace CommonLibrary
{
    public class StringAssembler
    {
        /// <summary>
        /// 用于匹配大括号中包含一个数字的表达式
        /// </summary>
        private readonly static Regex Regex = new Regex("{[0-9]+}");

        /// <summary>
        /// 用于生成条件字符串
        /// </summary>
        private StringBuilder StrBuilder = new StringBuilder();

        /// <summary>
        /// 格式的各个部分
        /// </summary>
        string[] FormatParts;

        /// <summary>
        /// 要嵌入到格式中的各个值在传入参数中索引
        /// </summary>
        int[] ValueIndexs;

        /// <summary>
        /// ValueIndexs中的最大值
        /// </summary>
        int MaxValueIndex;

        /// <summary>
        /// 构造函数
        /// </summary>
        public StringAssembler()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConditionFormaterStr"></param>
        public StringAssembler(string FormaterStr)
        {
            this.SetFormat(FormaterStr);
        }

        /// <summary>
        /// 解析格式化表达式
        /// </summary>
        /// <param name="ConditionFormaterStr"></param>
        /// <returns></returns>
        public int SetFormat(string FormaterStr)
        {
            //将以{[0-9]+}分隔的格式各部分解析出来
            FormatParts = StringAssembler.Regex.Split(FormaterStr);

            //解析格式中包含的所有{[0-9]+}
            MatchCollection ValueIndexMatchs = StringAssembler.Regex.Matches(FormaterStr);

            MaxValueIndex = -1;
            ValueIndexs = new int[ValueIndexMatchs.Count];

            //解析各{[0-9]+}中的数字
            for (int Idx = 0; Idx < ValueIndexMatchs.Count; ++Idx)
            {
                string Value = ValueIndexMatchs[Idx].Groups[0].Value;

                Value = Value.Substring(1, Value.Length - 2);
                ValueIndexs[Idx] = int.Parse(Value);

                if (ValueIndexs[Idx] > MaxValueIndex) MaxValueIndex = ValueIndexs[Idx];
            }

            return 1;
        }

        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public string Assemble(params string[] Values)
        {
            if (FormatParts == null || ValueIndexs == null || Values.Length <= MaxValueIndex) return string.Empty;

            StrBuilder.Remove(0, StrBuilder.Length);

            for (int Idx = 0; Idx < ValueIndexs.Length; ++Idx)
            {
                StrBuilder.Append(FormatParts[Idx]);
                StrBuilder.Append(Values[ValueIndexs[Idx]]);
            }
            StrBuilder.Append(FormatParts[FormatParts.Length - 1]);

            return StrBuilder.ToString();
        }

        /// <summary>
        /// 将多个字符串拼接为一个字符串
        /// </summary>
        /// <param name="Linker">用于连接的字符串</param>
        /// <param name="Source">源字符串</param>
        /// <returns>拼接得到的字符串</returns>
        public static string StringSplice(string Linker, params string[] Source)
        {
            if (Source.Length == 0) return string.Empty;

            string Result = Source[0];

            for (int Idx = 1; Idx < Source.Length; ++Idx)
            {
                Result += Linker + Source[Idx];
            }
            return Result;
        }

        /// <summary>
        /// 将多个字符串拼接为一个字符串
        /// </summary>
        /// <param name="Linker">用于连接的字符串</param>
        /// <param name="Source">源字符串</param>
        /// <returns>拼接得到的字符串</returns>
        public static string StringSplice(string Linker, IEnumerable<string> Source)
        {
            if (Source == null) return string.Empty;

            IEnumerator<string> Enumerator = Source.GetEnumerator();

            StringBuilder Result = new StringBuilder();

            while (Enumerator.MoveNext())
            {
                Result.Append(Linker);
                Result.Append(Enumerator.Current);
            }

            if (Result.Length > 0) Result.Remove(0, Linker.Length);
            return Result.ToString();
        }

        /// <summary>
        /// 将多个数据拼接为一个字符串
        /// </summary>
        /// <param name="Linker">用于连接的字符串</param>
        /// <param name="Source">源字符串</param>
        /// <returns>拼接得到的字符串</returns>
        public static string StringSplice(string Linker, IEnumerable Source)
        {
            if (Source == null) return string.Empty;

            IEnumerator Enumerator = Source.GetEnumerator();

            StringBuilder Result = new StringBuilder();

            while (Enumerator.MoveNext())
            {
                Result.Append(Linker);
                Result.Append(Enumerator.Current.ToString());
            }

            if (Result.Length > 0) Result.Remove(0, Linker.Length);
            return Result.ToString();
        }
    }
}
