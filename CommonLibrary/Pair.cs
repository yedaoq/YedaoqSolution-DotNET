/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��Pair.cs

 * ˵�������ڴ洢һ�Թ������ݵ���

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��10��10��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// �洢һ������������ݵ���
    /// </summary>
    /// <typeparam name="FirstT">��һ��ֵ������</typeparam>
    /// <typeparam name="SecondT">�ڶ���ֵ������</typeparam>
    public class Pair<FirstT, SecondT>
    {
        /// <summary>
        /// ��һ��ֵ
        /// </summary>
        private FirstT _First;

        /// <summary>
        /// �ڶ���ֵ
        /// </summary>
        private SecondT _Second;

        /// <summary>
        /// ��һ��ֵ
        /// </summary>
        public FirstT First
        {
            get { return _First; }
            set { _First = value; }
        }

        /// <summary>
        /// �ڶ���ֵ
        /// </summary>
        public SecondT Second
        {
            get { return _Second; }
            set { _Second = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        public Pair(FirstT First, SecondT Second)
        {
            this.First = First;
            this.Second = Second;
        }

        /// <summary>
        /// ���ص���ȱȽ��߼�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;
            if (obj is Pair<FirstT, SecondT>)
            {
                Pair<FirstT, SecondT> Other = obj as Pair<FirstT, SecondT>;
                if (this.First.Equals(Other.First) && this.Second.Equals(Other.Second)) return true;
            }
            else if (obj is FirstT)
            {
                if (this.First.Equals((FirstT)obj)) return true;
            }
            else if (obj is SecondT)
            {
                if (this.Second.Equals((SecondT)obj)) return true;
            }
            return this.First.Equals(obj);
        }

        /// <summary>
        /// ��ȡ��ϣ��
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return First.GetHashCode() + Second.GetHashCode();
        }

        public static bool operator ==(Pair<FirstT, SecondT> A, object B)
        {
            return (object.ReferenceEquals(A, null) && object.ReferenceEquals(B, null)) || A.Equals(B);
        }

        public static bool operator !=(Pair<FirstT, SecondT> A, object B)
        {
            return !(A == B);
        }
    }
}
