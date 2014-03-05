/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��SqlBuilder.cs

 * ˵�����򵥷�Ƕ�׵�DML���������

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��10��10��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary
{
    /// <summary>
    /// ���ڴ���Sql���ĸ�����
    /// </summary>
    public class SqlBuilder
    {
        #region ����

        /// <summary>
        /// ��ȡ�м�
        /// </summary>
        public readonly List<ColValuePair> AccessColList = new List<ColValuePair>();

        /// <summary>
        /// ��
        /// </summary>
        public readonly List<string> TableList = new List<string>(1);

        /// <summary>
        /// ѡ��������ʽ���ַ���
        /// </summary>
        public string SelectConditionFormater;

        /// <summary>
        /// �����������ʽ�б�
        /// </summary>
        public readonly XList<Pair<string, string>> UpdateConditionFormaterList = new XList<Pair<string, string>>();

        /// <summary>
        /// ɾ���������ʽ�б�
        /// </summary>
        public readonly XList<Pair<string, string>> DeleteConditionFormaterList = new XList<Pair<string, string>>();

        /// <summary>
        /// ���ڹ���Sql�Ķ���
        /// </summary>
        private StringBuilder StrBuilder = new StringBuilder();

        /// <summary>
        /// һ�������õ�StringBuilder��Ŀǰ�����ڹ���Insert���
        /// </summary>
        private StringBuilder AssistStrBuilder = new StringBuilder();

        #endregion

        #region ����

        #region ����Sql�ӿ�

        /// <summary>
        /// ��ȡ��ѯ��¼���
        /// </summary>
        public string GetSelectSql(params string[] ConditionValue)
        {
            //��ն���
            StrBuilder.Remove(0, StrBuilder.Length);

            StrBuilder.Append("SELECT ");

            AppendColSpliceStr(StrBuilder);

            StrBuilder.Append(" FROM ");

            StrBuilder.Append(StringAssembler.StringSplice(",", this.TableList));

            if (SelectConditionFormater != null && SelectConditionFormater != string.Empty)
            {
                StrBuilder.Append(" WHERE ");

                StrBuilder.AppendFormat(SelectConditionFormater, ConditionValue);
            }

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        /// <summary>
        /// ��ȡ���¼�¼���
        /// </summary>
        public string GetUpdateSql(string Table, params string[] ConditionValue)
        {
            int UpdateColCounts = 0;

            //��ն���
            StrBuilder.Remove(0, StrBuilder.Length);

            StrBuilder.Append("UPDATE ");

            StrBuilder.Append(Table);

            StrBuilder.Append(" SET ");

            //�����
            foreach (ColValuePair Pair in this.AccessColList)
            {
                if (Pair.Col.TabName != Table || Pair.Value == DBValue.NoSet) continue;

                if (UpdateColCounts != 0) StrBuilder.Append(", ");

                Pair.AppendAssignStrToBuilder(StrBuilder);

                ++UpdateColCounts;
            }

            if (UpdateColCounts == 0) return string.Empty;

            //����������ʽ�����û����Ӧ��ĸ���������ʽ���ַ�������ʹ�ò�ѯ��������ʽ���ַ���
            int Idx = FindIdxOfConditionFormater(Table, UpdateConditionFormaterList);

            string Formater = (Idx == -1) ? SelectConditionFormater : UpdateConditionFormaterList[Idx].Second;

            AppendConditionStr(StrBuilder, Formater, ConditionValue);

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        /// <summary>
        /// ��ȡɾ����¼���
        /// </summary>
        public string GetDeleteSql(string Table, params string[] ConditionValue)
        {
            //��ն���
            StrBuilder.Remove(0, StrBuilder.Length);

            StrBuilder.Append("DELETE FROM ");

            StrBuilder.Append(Table);

            //����������ʽ�����û����Ӧ��ĸ���������ʽ���ַ�������ʹ�ò�ѯ��������ʽ���ַ���
            int Idx = FindIdxOfConditionFormater(Table, DeleteConditionFormaterList);

            string Formater = (Idx == -1) ? SelectConditionFormater : UpdateConditionFormaterList[Idx].Second;

            AppendConditionStr(StrBuilder, Formater, ConditionValue);

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        /// <summary>
        /// ��ȡ��Ӽ�¼���
        /// </summary>
        public string GetInsertSql(string Table)
        {
            int UpdateColCounts = 0;

            //��ն���
            StrBuilder.Remove(0, StrBuilder.Length);
            AssistStrBuilder.Remove(0, AssistStrBuilder.Length);

            StrBuilder.Append("INSERT INTO ");

            StrBuilder.Append(Table);

            StrBuilder.Append(" ( ");

            //�����
            foreach (ColValuePair Pair in this.AccessColList)
            {
                if (Pair.Col.TabName != Table || Pair.Value == DBValue.NoSet) continue;

                if (UpdateColCounts != 0)
                {
                    StrBuilder.Append(", ");
                    AssistStrBuilder.Append(", ");
                }

                StrBuilder.Append(Pair.Col.ColName);

                Pair.AppendValueStrToBuilder(AssistStrBuilder);

                ++UpdateColCounts;
            }

            if (UpdateColCounts == 0) return string.Empty;

            StrBuilder.Append(") VALUES (");
            StrBuilder.Append(AssistStrBuilder.ToString());
            StrBuilder.Append(')');

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        #endregion

        #region �༭���ݽӿ�

        /// <summary>
        /// ���һ��Ҫ��ȡ����
        /// </summary>
        public int AddColBatch(string Table, params string[] ColumnName)
        {
            //Table = PreTreatTableName(Table);

            foreach (string Col in ColumnName)
            {
                this.AccessColList.Add(new ColValuePair(new DBCol(Col, Table)));
            }

            Predicate<string> Match = delegate(string Str) { return Str == Table; };

            if (TableList.FindIndex(Match) == -1)
            {
                TableList.Add(Table);
            }

            return 1;
        }

        /// <summary>
        /// ���һ�����ָ�����Ե�Ҫ��ȡ����
        /// </summary>
        public int AddColBatch(string Table, EnumDBColProperty Property, params string[] ColumnName)
        {
            //Table = PreTreatTableName(Table);

            foreach (string Col in ColumnName)
            {
                this.AccessColList.Add(new ColValuePair(new DBCol(Col, Table, Property)));
            }

            Predicate<string> Match = delegate(string Str) { return Str == Table; };

            if (TableList.FindIndex(Match) == -1)
            {
                TableList.Add(Table);
            }

            return 1;
        }

        /// <summary>
        /// ���һ�����ָ�����Ե�Ҫ��ȡ����
        /// </summary>
        public int AddColBatch(string Table, EnumDBColProperty[] Propertys, params string[] ColumnName)
        {
            //Table = PreTreatTableName(Table);

            foreach (string Col in ColumnName)
            {
                this.AccessColList.Add(new ColValuePair(new DBCol(Col, Table, Propertys)));
            }

            Predicate<string> Match = delegate(string Str) { return Str == Table; };

            if (TableList.FindIndex(Match) == -1)
            {
                TableList.Add(Table);
            }

            return 1;
        }

        /// <summary>
        /// ����ӻ���¼�¼ʱ�����е�ֵ
        /// </summary>
        public int SetColValueBatch(params object[] ColumnValue)
        {
            for (int idx = 0; idx < Math.Min(this.AccessColList.Count, ColumnValue.Length); ++idx)
            {
                if (ColumnValue[idx] is DBValue)
                {
                    AccessColList[idx].Value = ColumnValue[idx] as DBValue;
                }
                else if (ColumnValue[idx] is string)
                {
                    AccessColList[idx].Value = new DBValue(ColumnValue[idx] as string);
                }
            }

            return 1;
        }

        /// <summary>
        /// ��ָ���е�ֵ��Ϊָ��ֵ
        /// </summary>
        /// <param name="ColName">����</param>
        /// <param name="Value">ֵ</param>
        /// <returns>�Ƿ����óɹ�</returns>
        public int SetColValue(string ColName, DBValue Value)
        {
            ColValuePair Pair = GetCol(ColName);

            if (Pair == null) return 0;

            Pair.Value = Value;

            return 1;
        }

        /// <summary>
        /// ��ָ���е�ֵ��Ϊָ��ֵ
        /// </summary>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetColValue(string ColName, string Value)
        {
            ColValuePair Pair = GetCol(ColName);

            if (Pair == null) return 0;

            Pair.Value = new DBValue(Value);

            return 1;
        }

        /// <summary>
        /// ��������õ�ֵ���˹��̽���������ֵ��ΪDBValue.NoSet
        /// </summary>
        /// <returns></returns>
        public int ClearValue()
        {
            foreach (ColValuePair Pair in AccessColList)
            {
                Pair.Value = DBValue.NoSet;
            }

            return 1;
        }

        /// <summary>
        /// Ϊ����������
        /// </summary>
        /// <param name="ColName">��</param>
        /// <param name="Propertys">����</param>
        /// <returns></returns>
        public int SetColProperty(string ColName, params EnumDBColProperty[] Propertys)
        {
            ColValuePair Pair = GetCol(ColName);

            if (Pair == null) return 0;

            Pair.Col.SetProperty(Propertys);

            return 1;
        }

        /// <summary>
        /// Ϊ����������
        /// </summary>
        /// <param name="ColName">��</param>
        /// <param name="Propertys">����</param>
        /// <returns></returns>
        public int SetColProperty(EnumDBColProperty Property, params string[] ColNames)
        {
            foreach (string ColName in ColNames)
            {
                ColValuePair Pair = GetCol(ColName);

                if (Pair == null) continue;

                Pair.Col.SetProperty(Property);
            }

            return 1;
        }

        /// <summary>
        /// ���ò�ѯ��¼��������ʽ���ַ���
        /// </summary>
        public int SetSelectConditionFormator(string Formater)
        {
            this.SelectConditionFormater = Formater;

            return 1;
        }

        /// <summary>
        /// ����ָ����ĸ��¼�¼������������ʽ���ַ���
        /// </summary>
        public int SetUpdateConditionFormater(string TableName, string Formater)
        {
            //Table = PreTreatTableName(Table);

            int Idx = FindIdxOfConditionFormater(TableName, UpdateConditionFormaterList);
            if (Idx == -1)
            {
                UpdateConditionFormaterList.Add(new Pair<string, string>(TableName, Formater));
            }
            else
            {
                UpdateConditionFormaterList[Idx].Second = Formater;
            }

            return 1;
        }

        /// <summary>
        /// ����ָ�����ɾ����¼������������ʽ���ַ���
        /// </summary>
        public int SetDeleteConditionFormater(string TableName, string Formater)
        {
            //Table = PreTreatTableName(Table);

            int Idx = FindIdxOfConditionFormater(TableName, DeleteConditionFormaterList);
            if (Idx == -1)
            {
                DeleteConditionFormaterList.Add(new Pair<string, string>(TableName, Formater));
            }
            else
            {
                DeleteConditionFormaterList[Idx].Second = Formater;
            }

            return 1;
        }

        #endregion

        #region �ڲ�����

        /// <summary>
        /// ��ȡָ�����������ʽ���ַ������б��е�λ��
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        private int FindIdxOfConditionFormater(string TableName, List<Pair<string, string>> List)
        {
            for (int Idx = 0; Idx < List.Count; ++Idx)
            {
                if (List[Idx].First == TableName) return Idx;
            }

            return -1;
        }

        /// <summary>
        /// ������ַ���ƴ��Ϊһ���ַ���
        /// </summary>
        /// <param name="Linker">�������ӵ��ַ���</param>
        /// <param name="Source">Դ�ַ���</param>
        /// <returns>ƴ�ӵõ����ַ���</returns>
        public string StringSplice(string Linker, IEnumerable<string> Source)
        {
            if (Source == null) return string.Empty;

            IEnumerator<string> Enumerator = Source.GetEnumerator();

            string Result = "";

            while (Enumerator.MoveNext())
            {
                Result += Linker + Enumerator.Current;
            }

            return Result.Substring(Linker.Length);
        }

        /// <summary>
        /// ���ɶ������ӵ������ַ�����ӵ�ָ��StringBuilder
        /// </summary>
        private int AppendColSpliceStr(StringBuilder Builder)
        {
            if (AccessColList.Count == 0)
            {
                Builder.Append(" * ");
            }
            else
            {
                AccessColList[0].Col.AppendWholeNameToBuilder(Builder);

                for (int idx = 1; idx < AccessColList.Count; ++idx)
                {
                    Builder.Append(", ");
                    AccessColList[idx].Col.AppendWholeNameToBuilder(Builder);
                }
            }

            return 1;
        }

        /// <summary>
        /// ��ָ����StringBuilder���һ���������ʽ
        /// </summary>
        /// <param name="Builder">Ŀ��StringBuilder</param>
        /// <param name="Formater">��ʽ���ַ���</param>
        /// <param name="ConditionValue">����ֵ</param>
        /// <returns></returns>
        private int AppendConditionStr(StringBuilder Builder, string Formater, string[] ConditionValue)
        {
            if (Formater == null || Formater == string.Empty) return 0;

            Builder.Append(" WHERE ");
            Builder.AppendFormat(Formater, ConditionValue);

            return 1;
        }

        /// <summary>
        /// ���ɶ������ӵı���������ӵ�ָ��StringBuilder
        /// </summary>
        /// <param name="Builder"></param>
        /// <returns></returns>
        private int AppendTabSpliceStr(StringBuilder Builder)
        {
            if (TableList.Count <= 0)
            {
                throw new Exception("����ΪSqlBuilderָ��Դ��");
            }

            Builder.Append(TableList[0]);

            for (int idx = 1; idx < TableList.Count; ++idx)
            {
                Builder.Append(", ");
                Builder.Append(TableList[idx]);
            }


            return 1;
        }

        /// <summary>
        /// ����ָ������
        /// </summary>
        /// <param name="ColName">����</param>
        /// <returns>Ҫ���ҵĶ���</returns>
        private ColValuePair GetCol(string ColName)
        {
            string[] Temp = ColName.Split('.');

            //���⴦��
            //if (Temp.Length == 2) Temp[0] = PreTreatTableName(Temp[0]);

            Predicate<ColValuePair> MatchWholeName = delegate(ColValuePair Pair) { return (Pair.Col.TabName == Temp[0] && Pair.Col.ColName == Temp[1]); };

            Predicate<ColValuePair> MatchColName = delegate(ColValuePair Pair) { return Pair.Col.ColName == Temp[0]; };

            Predicate<ColValuePair> Match = (Temp.Length == 2) ? MatchWholeName : MatchColName;

            return AccessColList.Find(Match);
        }

        #endregion

        #region ���⴦��

        /// <summary>
        /// Ԥ�������
        /// </summary>
        public static PreTreatTableNameDelegate PreTreatTableName;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static SqlBuilder()
        {
            //if (SystemData.s_DatabaseType == 2)
            //{
            //    PreTreatTableName = PreTreatTableName_DB2;
            //}
            //else
            //{
            //    PreTreatTableName = PreTreatTableName_Normal;
            //}
        }

        /// <summary>
        /// ��DB��Ա����Ĵ���
        /// </summary>
        /// <param name="TableName">����</param>
        /// <returns>�����ı���</returns>
        public static string PreTreatTableName_Normal(string TableName)
        {
            return TableName;
        }

        /// <summary>
        /// ������DB2���ݿ�ı���Ԥ����
        /// </summary>
        /// <param name="TableName">����</param>
        /// <returns>�����ı���</returns>
        public static string PreTreatTableName_DB2(string TableName)
        {
            return "JB_ZD_" + TableName;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// ����Ԥ�����ί��
    /// </summary>
    /// <param name="TableName">����</param>
    /// <returns>�����ı���</returns>
    public delegate string PreTreatTableNameDelegate(string TableName);

    /// <summary>
    /// ���ڱ�ʾ���ݿ�ֵ���࣬��SqlBuilder���ʹ��
    /// </summary>
    public class DBValue
    {
        /// <summary>
        /// ��ʾֵδ���õ�DBValue����
        /// </summary>
        public static readonly DBValue NoSet = new DBValue(string.Empty);

        /// <summary>
        /// ��ʾһ�����ݿ��ֵ
        /// </summary>
        public static readonly DBValue Null = new DBValue(string.Empty);

        /// <summary>
        /// ��ʾһ�����ݿ�ֵ
        /// </summary>
        public string Value;

        public DBValue(string Value)
        {
            this.Value = Value;
        }
    }

    /// <summary>
    /// �����������ݿ������Ե�ö�٣�������DBCol���ʹ��
    /// </summary>
    public enum EnumDBColProperty
    {
        /// <summary>
        /// ������ֵ����Ǹ����Ե���������ֵʱ����Ҫ��ֵ���߼ӵ�����
        /// λ��������1λΪ0
        /// </summary>
        NumValue = 65534,

        /// <summary>
        /// �ַ�����ֵ����Ǹ����Ե���������ֵʱ��Ҫ��ֵ���߼ӵ�����
        /// λ��������1λΪ1
        /// </summary>
        StrValue = 1,

        /// <summary>
        /// �ɿ�
        /// λ��������2λΪ0
        /// </summary>
        NullAble = 65533,

        /// <summary>
        /// �ǿ�
        /// λ��������2λΪ1
        /// </summary>
        UnNull = 2
    }

    /// <summary>
    /// ���ڱ�ʾ���ݿ��е��࣬��SqlBuilder���ʹ��
    /// </summary>
    public class DBCol
    {
        public string ColName;
        public string TabName;
        public short ColProperty = 0;

        public DBCol(string ColName, string TabName, params EnumDBColProperty[] Propertys)
        {
            this.ColName = ColName;
            this.TabName = TabName;

            SetProperty(Propertys);
        }

        public DBCol(string ColName, string TabName)
        {
            this.ColName = ColName;
            this.TabName = TabName;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="Propertys"></param>
        public void SetProperty(params EnumDBColProperty[] Propertys)
        {
            foreach (short Pro in Propertys)
            {
                //��������ֵ�Ķ�����������1�ĸ�����������������
                if (((Pro - 1) & Pro) == 0)
                {
                    ColProperty = (short)(ColProperty | Pro);
                }
                else
                {
                    ColProperty = (short)(ColProperty & Pro);
                }
            }
        }

        /// <summary>
        /// ȫ��
        /// </summary>
        public string WholeName
        {
            get
            {
                return TabName + '.' + ColName;
            }
        }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public bool IsNullAble
        {
            get
            {
                return (ColProperty & (int)EnumDBColProperty.UnNull) == 0;
            }
        }

        /// <summary>
        /// �Ƿ�Ϊ�ַ�����ֵ
        /// </summary>
        public bool IsStrValue
        {
            get
            {
                return (ColProperty & (int)EnumDBColProperty.StrValue) != 0;
            }
        }

        /// <summary>
        /// ��ȫ�������һ��StringBuilder
        /// </summary>
        /// <param name="Builder">Ŀ��StringBuilder</param>
        public void AppendWholeNameToBuilder(StringBuilder Builder)
        {
            Builder.Append(TabName);
            Builder.Append('.');
            Builder.Append(ColName);
        }
    }

    /// <summary>
    /// ���ڱ�ʾһ����������ֵ�Ե��࣬��SqlBuilder���ʹ��
    /// </summary>
    public class ColValuePair
    {
        /// <summary>
        /// ��
        /// </summary>
        public DBCol Col;

        /// <summary>
        /// ֵ
        /// </summary>
        public DBValue Value;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Col">��Ӧ����</param>
        public ColValuePair(DBCol Col)
        {
            this.Col = Col;
            this.Value = DBValue.NoSet;
        }

        /// <summary>
        /// ��ȡһ����صĸ�ֵ���
        /// </summary>
        /// <returns>��ֵ���</returns>
        public string GetAssignStr()
        {
            if (Value == DBValue.NoSet)
            {
                return string.Empty;
            }
            else if (Value == DBValue.Null)
            {
                return Col.WholeName + " = null ";
            }
            else
            {
                if (Col.IsStrValue)
                {
                    return Col.WholeName + " = '" + Value.Value + '\'';
                }
                else
                {
                    return Col.WholeName + " = " + Value.Value;
                }
            }
        }

        /// <summary>
        /// ����ֵ�ַ��������һ��StringBuilder
        /// </summary>
        /// <param name="Builder">���Ŀ��</param>
        public void AppendAssignStrToBuilder(StringBuilder Builder)
        {
            if (Value == DBValue.NoSet)
            {
                return;
            }
            else
            {
                Builder.Append(Col.ColName);

                //Col.AppendWholeNameToBuilder(Builder);

                Builder.Append(" = ");

                AppendValueStrToBuilder(Builder);
            }
        }

        /// <summary>
        /// �����������ݿ���µ���ֵ�ַ�����ӵ�ָ��StringBuilder
        /// </summary>
        /// <param name="Builder">Ŀ��</param>
        public void AppendValueStrToBuilder(StringBuilder Builder)
        {
            if (Value == DBValue.Null)
            {
                Builder.Append("null");
            }
            else
            {
                if (Col.IsStrValue)
                {
                    Builder.Append('\'');
                    Builder.Append(Value.Value);
                    Builder.Append('\'');
                }
                else
                {
                    Builder.Append(Value.Value);
                }
            }
        }
    }
}
