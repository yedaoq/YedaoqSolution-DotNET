/*

 * �汾˵�� ��0.0.0.1
 
 * �ļ����� ��BusImpedanceItem.cs

 * ˵������һ�������ļ��¼�������

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
    /// ���Դ���һ�����ݵļ��¼�������
    /// </summary>
    public class SimpleEvnetArgs : EventArgs
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public object Obj;

        public SimpleEvnetArgs(object Obj)
        {
            this.Obj = Obj;
        }
    }

    /// <summary>
    ///  ����һ�����ݵļ��¼�����Ӧԭ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimpleEventHandler(object sender, SimpleEvnetArgs e);
}
