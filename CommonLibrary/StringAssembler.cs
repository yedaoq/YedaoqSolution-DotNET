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
        /// ����ƥ��������а���һ�����ֵı��ʽ
        /// </summary>
        private readonly static Regex Regex = new Regex("{[0-9]+}");

        /// <summary>
        /// �������������ַ���
        /// </summary>
        private StringBuilder StrBuilder = new StringBuilder();

        /// <summary>
        /// ��ʽ�ĸ�������
        /// </summary>
        string[] FormatParts;

        /// <summary>
        /// ҪǶ�뵽��ʽ�еĸ���ֵ�ڴ������������
        /// </summary>
        int[] ValueIndexs;

        /// <summary>
        /// ValueIndexs�е����ֵ
        /// </summary>
        int MaxValueIndex;

        /// <summary>
        /// ���캯��
        /// </summary>
        public StringAssembler()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="ConditionFormaterStr"></param>
        public StringAssembler(string FormaterStr)
        {
            this.SetFormat(FormaterStr);
        }

        /// <summary>
        /// ������ʽ�����ʽ
        /// </summary>
        /// <param name="ConditionFormaterStr"></param>
        /// <returns></returns>
        public int SetFormat(string FormaterStr)
        {
            //����{[0-9]+}�ָ��ĸ�ʽ�����ֽ�������
            FormatParts = StringAssembler.Regex.Split(FormaterStr);

            //������ʽ�а���������{[0-9]+}
            MatchCollection ValueIndexMatchs = StringAssembler.Regex.Matches(FormaterStr);

            MaxValueIndex = -1;
            ValueIndexs = new int[ValueIndexMatchs.Count];

            //������{[0-9]+}�е�����
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
        /// ���ɱ��ʽ
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
        /// ������ַ���ƴ��Ϊһ���ַ���
        /// </summary>
        /// <param name="Linker">�������ӵ��ַ���</param>
        /// <param name="Source">Դ�ַ���</param>
        /// <returns>ƴ�ӵõ����ַ���</returns>
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
        /// ������ַ���ƴ��Ϊһ���ַ���
        /// </summary>
        /// <param name="Linker">�������ӵ��ַ���</param>
        /// <param name="Source">Դ�ַ���</param>
        /// <returns>ƴ�ӵõ����ַ���</returns>
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
        /// ���������ƴ��Ϊһ���ַ���
        /// </summary>
        /// <param name="Linker">�������ӵ��ַ���</param>
        /// <param name="Source">Դ�ַ���</param>
        /// <returns>ƴ�ӵõ����ַ���</returns>
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
