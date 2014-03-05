using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// 此类用于表示一组枚举类型的数据的集合
    /// </summary>
    public class EnumClassInfo:IListSource
    {
        /// <summary>
        /// 对应的枚举类型
        /// </summary>
        public readonly Enum EnumClass;

        /// <summary>
        /// 枚举项列表
        /// </summary>
        public List<EnumItem> ItemList = new List<EnumItem>();

        /// <summary>
        /// 获取指定值的枚举项信息
        /// </summary>
        /// <param name="Value">枚举项值</param>
        /// <returns></returns>
        public EnumItem this[string Value]
        {
            get
            {
                foreach (EnumItem Item in ItemList)
                {
                    if (Item.Value == Value) return Item;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取指定基数值的枚举项信息
        /// </summary>
        /// <param name="BaseValue">枚举项基数</param>
        /// <returns></returns>
        public EnumItem this[int BaseValue]
        {
            get
            {
                foreach (EnumItem Item in ItemList)
                {
                    if (Item.BaseValue == BaseValue) return Item;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取表示集合是否是 IList 对象集合的值。
        /// </summary>
        public bool ContainsListCollection
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取数据绑定列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            return ItemList;
        }
    }

    /// <summary>
    /// 此类用于描述一个枚举值
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 枚举项的值
        /// </summary>
        public string _Value;

        /// <summary>
        /// 枚举项的名称
        /// </summary>
        public string _Name;

        /// <summary>
        /// 基数
        /// </summary>
        public int _BaseValue;

        /// <summary>
        /// 枚举项的值
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// 枚举项的名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 基数
        /// </summary>
        public int BaseValue
        {
            get { return _BaseValue; }
            set { _BaseValue = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Value">值</param>
        /// <param name="Name">名称</param>
        /// <param name="BaseValue">基数</param>
        public EnumItem(string Value, string Name, int BaseValue)
        {
            this.Value = Value;
            this.Name = Name;
            this.BaseValue = BaseValue;
        }
    }
}
