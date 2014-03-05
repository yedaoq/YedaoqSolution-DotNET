/*

 * �к㲩�����ֵ����Ƽ����޹�˾ ��Ȩ���� ��������Ȩ����

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��SourceGridHelper.cs

 * ˵������չ��List<T>��

 * ���ߣ�Ҷ��ȫ
 
 * ʱ�䣺2009��6��9��
 
 * �޸ļ�¼ �� 
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// <![CDATA[��չ��List<T>��]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XList<T>:List<T>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        /// <param name="Obj">Ҫ���ҵĶ���</param>
        /// <returns>���ҵ���Ԫ��</returns>
        public object FindObject(object Obj)
        {
            if (Obj == null) return null;

            Enumerator Enumerator = GetEnumerator();

            while (Enumerator.MoveNext())
            {
                if (Enumerator.Current != null && Enumerator.Current.Equals(Obj)) return Enumerator.Current;
            }

            return null;
        }
    }
}
