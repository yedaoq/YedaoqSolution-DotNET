using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// �������ڱ�ʾһ��ö�����͵����ݵļ���
    /// </summary>
    public class EnumClassInfo:IListSource
    {
        /// <summary>
        /// ��Ӧ��ö������
        /// </summary>
        public readonly Enum EnumClass;

        /// <summary>
        /// ö�����б�
        /// </summary>
        public List<EnumItem> ItemList = new List<EnumItem>();

        /// <summary>
        /// ��ȡָ��ֵ��ö������Ϣ
        /// </summary>
        /// <param name="Value">ö����ֵ</param>
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
        /// ��ȡָ������ֵ��ö������Ϣ
        /// </summary>
        /// <param name="BaseValue">ö�������</param>
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
        /// ��ȡ��ʾ�����Ƿ��� IList ���󼯺ϵ�ֵ��
        /// </summary>
        public bool ContainsListCollection
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ��ȡ���ݰ��б�
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            return ItemList;
        }
    }

    /// <summary>
    /// ������������һ��ö��ֵ
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// ö�����ֵ
        /// </summary>
        public string _Value;

        /// <summary>
        /// ö���������
        /// </summary>
        public string _Name;

        /// <summary>
        /// ����
        /// </summary>
        public int _BaseValue;

        /// <summary>
        /// ö�����ֵ
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// ö���������
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int BaseValue
        {
            get { return _BaseValue; }
            set { _BaseValue = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Value">ֵ</param>
        /// <param name="Name">����</param>
        /// <param name="BaseValue">����</param>
        public EnumItem(string Value, string Name, int BaseValue)
        {
            this.Value = Value;
            this.Name = Name;
            this.BaseValue = BaseValue;
        }
    }
}
