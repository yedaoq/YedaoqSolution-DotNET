/*

 * 中恒博瑞数字电力科技有限公司 版权所有 保留所有权利。

 * 版本说明 ：0.0.0.1
 
 * 文件名称 ：SqlBuilder.cs

 * 说明：简单非嵌套的DML语句生成器

 * 作者：叶道全
 
 * 时间：2009年10月10日
 
 * 修改记录 ： 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary
{
    /// <summary>
    /// 用于创建Sql语句的辅助类
    /// </summary>
    public class SqlBuilder
    {
        #region 数据

        /// <summary>
        /// 存取列集
        /// </summary>
        public readonly List<ColValuePair> AccessColList = new List<ColValuePair>();

        /// <summary>
        /// 表集
        /// </summary>
        public readonly List<string> TableList = new List<string>(1);

        /// <summary>
        /// 选择条件格式化字符串
        /// </summary>
        public string SelectConditionFormater;

        /// <summary>
        /// 更新条件表达式列表
        /// </summary>
        public readonly XList<Pair<string, string>> UpdateConditionFormaterList = new XList<Pair<string, string>>();

        /// <summary>
        /// 删除条件表达式列表
        /// </summary>
        public readonly XList<Pair<string, string>> DeleteConditionFormaterList = new XList<Pair<string, string>>();

        /// <summary>
        /// 用于构造Sql的对象
        /// </summary>
        private StringBuilder StrBuilder = new StringBuilder();

        /// <summary>
        /// 一个辅助用的StringBuilder，目前仅用于构造Insert语句
        /// </summary>
        private StringBuilder AssistStrBuilder = new StringBuilder();

        #endregion

        #region 方法

        #region 生成Sql接口

        /// <summary>
        /// 获取查询记录语句
        /// </summary>
        public string GetSelectSql(params string[] ConditionValue)
        {
            //清空对象
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
        /// 获取更新记录语句
        /// </summary>
        public string GetUpdateSql(string Table, params string[] ConditionValue)
        {
            int UpdateColCounts = 0;

            //清空对象
            StrBuilder.Remove(0, StrBuilder.Length);

            StrBuilder.Append("UPDATE ");

            StrBuilder.Append(Table);

            StrBuilder.Append(" SET ");

            //添加列
            foreach (ColValuePair Pair in this.AccessColList)
            {
                if (Pair.Col.TabName != Table || Pair.Value == DBValue.NoSet) continue;

                if (UpdateColCounts != 0) StrBuilder.Append(", ");

                Pair.AppendAssignStrToBuilder(StrBuilder);

                ++UpdateColCounts;
            }

            if (UpdateColCounts == 0) return string.Empty;

            //添加条件表达式：如果没有相应表的更新条件格式化字符串，则使用查询的条件格式化字符串
            int Idx = FindIdxOfConditionFormater(Table, UpdateConditionFormaterList);

            string Formater = (Idx == -1) ? SelectConditionFormater : UpdateConditionFormaterList[Idx].Second;

            AppendConditionStr(StrBuilder, Formater, ConditionValue);

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        /// <summary>
        /// 获取删除记录语句
        /// </summary>
        public string GetDeleteSql(string Table, params string[] ConditionValue)
        {
            //清空对象
            StrBuilder.Remove(0, StrBuilder.Length);

            StrBuilder.Append("DELETE FROM ");

            StrBuilder.Append(Table);

            //添加条件表达式：如果没有相应表的更新条件格式化字符串，则使用查询的条件格式化字符串
            int Idx = FindIdxOfConditionFormater(Table, DeleteConditionFormaterList);

            string Formater = (Idx == -1) ? SelectConditionFormater : UpdateConditionFormaterList[Idx].Second;

            AppendConditionStr(StrBuilder, Formater, ConditionValue);

            //Test
            Console.WriteLine(StrBuilder.ToString());

            return StrBuilder.ToString();
        }

        /// <summary>
        /// 获取添加记录语句
        /// </summary>
        public string GetInsertSql(string Table)
        {
            int UpdateColCounts = 0;

            //清空对象
            StrBuilder.Remove(0, StrBuilder.Length);
            AssistStrBuilder.Remove(0, AssistStrBuilder.Length);

            StrBuilder.Append("INSERT INTO ");

            StrBuilder.Append(Table);

            StrBuilder.Append(" ( ");

            //添加列
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

        #region 编辑数据接口

        /// <summary>
        /// 添加一组要存取的列
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
        /// 添加一组具有指定属性的要存取的列
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
        /// 添加一组具有指定属性的要存取的列
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
        /// 在添加或更新记录时设置列的值
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
        /// 将指定列的值置为指定值
        /// </summary>
        /// <param name="ColName">列名</param>
        /// <param name="Value">值</param>
        /// <returns>是否设置成功</returns>
        public int SetColValue(string ColName, DBValue Value)
        {
            ColValuePair Pair = GetCol(ColName);

            if (Pair == null) return 0;

            Pair.Value = Value;

            return 1;
        }

        /// <summary>
        /// 将指定列的值置为指定值
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
        /// 清空已设置的值，此过程将把所有列值置为DBValue.NoSet
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
        /// 为列设置属性
        /// </summary>
        /// <param name="ColName">列</param>
        /// <param name="Propertys">属性</param>
        /// <returns></returns>
        public int SetColProperty(string ColName, params EnumDBColProperty[] Propertys)
        {
            ColValuePair Pair = GetCol(ColName);

            if (Pair == null) return 0;

            Pair.Col.SetProperty(Propertys);

            return 1;
        }

        /// <summary>
        /// 为列设置属性
        /// </summary>
        /// <param name="ColName">列</param>
        /// <param name="Propertys">属性</param>
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
        /// 设置查询记录的条件格式化字符串
        /// </summary>
        public int SetSelectConditionFormator(string Formater)
        {
            this.SelectConditionFormater = Formater;

            return 1;
        }

        /// <summary>
        /// 设置指定表的更新记录操作的条件格式化字符串
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
        /// 设置指定表的删除记录操作的条件格式化字符串
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

        #region 内部方法

        /// <summary>
        /// 获取指定表的条件格式化字符串在列表中的位置
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
        /// 将多个字符串拼接为一个字符串
        /// </summary>
        /// <param name="Linker">用于连接的字符串</param>
        /// <param name="Source">源字符串</param>
        /// <returns>拼接得到的字符串</returns>
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
        /// 将由逗号连接的列名字符串添加到指定StringBuilder
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
        /// 向指定的StringBuilder添加一个条件表达式
        /// </summary>
        /// <param name="Builder">目标StringBuilder</param>
        /// <param name="Formater">格式化字符串</param>
        /// <param name="ConditionValue">条件值</param>
        /// <returns></returns>
        private int AppendConditionStr(StringBuilder Builder, string Formater, string[] ConditionValue)
        {
            if (Formater == null || Formater == string.Empty) return 0;

            Builder.Append(" WHERE ");
            Builder.AppendFormat(Formater, ConditionValue);

            return 1;
        }

        /// <summary>
        /// 将由逗号连接的表名符串添加到指定StringBuilder
        /// </summary>
        /// <param name="Builder"></param>
        /// <returns></returns>
        private int AppendTabSpliceStr(StringBuilder Builder)
        {
            if (TableList.Count <= 0)
            {
                throw new Exception("必须为SqlBuilder指定源表！");
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
        /// 查找指定的列
        /// </summary>
        /// <param name="ColName">列名</param>
        /// <returns>要查找的对象</returns>
        private ColValuePair GetCol(string ColName)
        {
            string[] Temp = ColName.Split('.');

            //特殊处理
            //if (Temp.Length == 2) Temp[0] = PreTreatTableName(Temp[0]);

            Predicate<ColValuePair> MatchWholeName = delegate(ColValuePair Pair) { return (Pair.Col.TabName == Temp[0] && Pair.Col.ColName == Temp[1]); };

            Predicate<ColValuePair> MatchColName = delegate(ColValuePair Pair) { return Pair.Col.ColName == Temp[0]; };

            Predicate<ColValuePair> Match = (Temp.Length == 2) ? MatchWholeName : MatchColName;

            return AccessColList.Find(Match);
        }

        #endregion

        #region 特殊处理

        /// <summary>
        /// 预处理表名
        /// </summary>
        public static PreTreatTableNameDelegate PreTreatTableName;

        /// <summary>
        /// 静态构造函数
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
        /// 除DB外对表名的处理
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>处理后的表名</returns>
        public static string PreTreatTableName_Normal(string TableName)
        {
            return TableName;
        }

        /// <summary>
        /// 适用于DB2数据库的表名预处理
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>处理后的表名</returns>
        public static string PreTreatTableName_DB2(string TableName)
        {
            return "JB_ZD_" + TableName;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 表名预处理的委托
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <returns>处理后的表名</returns>
    public delegate string PreTreatTableNameDelegate(string TableName);

    /// <summary>
    /// 用于表示数据库值的类，与SqlBuilder配合使用
    /// </summary>
    public class DBValue
    {
        /// <summary>
        /// 表示值未设置的DBValue对象
        /// </summary>
        public static readonly DBValue NoSet = new DBValue(string.Empty);

        /// <summary>
        /// 表示一个数据库空值
        /// </summary>
        public static readonly DBValue Null = new DBValue(string.Empty);

        /// <summary>
        /// 表示一个数据库值
        /// </summary>
        public string Value;

        public DBValue(string Value)
        {
            this.Value = Value;
        }
    }

    /// <summary>
    /// 用于描述数据库列属性的枚举，用于与DBCol配合使用
    /// </summary>
    public enum EnumDBColProperty
    {
        /// <summary>
        /// 数字型值：标记该属性的列在设置值时不需要在值两边加单引号
        /// 位特征：第1位为0
        /// </summary>
        NumValue = 65534,

        /// <summary>
        /// 字符串型值：标记该属性的列在设置值时需要在值两边加单引号
        /// 位特征：第1位为1
        /// </summary>
        StrValue = 1,

        /// <summary>
        /// 可空
        /// 位特征：第2位为0
        /// </summary>
        NullAble = 65533,

        /// <summary>
        /// 非空
        /// 位特征：第2位为1
        /// </summary>
        UnNull = 2
    }

    /// <summary>
    /// 用于表示数据库列的类，与SqlBuilder配合使用
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
        /// 设置列属性
        /// </summary>
        /// <param name="Propertys"></param>
        public void SetProperty(params EnumDBColProperty[] Propertys)
        {
            foreach (short Pro in Propertys)
            {
                //根据属性值的二进制序列中1的个数决定如何添加属性
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
        /// 全名
        /// </summary>
        public string WholeName
        {
            get
            {
                return TabName + '.' + ColName;
            }
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool IsNullAble
        {
            get
            {
                return (ColProperty & (int)EnumDBColProperty.UnNull) == 0;
            }
        }

        /// <summary>
        /// 是否为字符串型值
        /// </summary>
        public bool IsStrValue
        {
            get
            {
                return (ColProperty & (int)EnumDBColProperty.StrValue) != 0;
            }
        }

        /// <summary>
        /// 将全名输出到一个StringBuilder
        /// </summary>
        /// <param name="Builder">目标StringBuilder</param>
        public void AppendWholeNameToBuilder(StringBuilder Builder)
        {
            Builder.Append(TabName);
            Builder.Append('.');
            Builder.Append(ColName);
        }
    }

    /// <summary>
    /// 用于表示一个数据列与值对的类，与SqlBuilder配合使用
    /// </summary>
    public class ColValuePair
    {
        /// <summary>
        /// 列
        /// </summary>
        public DBCol Col;

        /// <summary>
        /// 值
        /// </summary>
        public DBValue Value;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Col">对应的列</param>
        public ColValuePair(DBCol Col)
        {
            this.Col = Col;
            this.Value = DBValue.NoSet;
        }

        /// <summary>
        /// 获取一个相关的赋值语句
        /// </summary>
        /// <returns>赋值语句</returns>
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
        /// 将赋值字符串输出到一个StringBuilder
        /// </summary>
        /// <param name="Builder">输出目标</param>
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
        /// 将可用于数据库更新的列值字符串添加到指定StringBuilder
        /// </summary>
        /// <param name="Builder">目标</param>
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
