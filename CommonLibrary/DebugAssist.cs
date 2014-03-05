using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CommonLibrary
{
    /// <summary>
    /// ���������ṩ���Գ���ĸ�������
    /// </summary>
    public class DebugAssist
    {
        /// <summary>
        /// ��ָ���������ӵ��ļ��У���Ŀ���ļ������ڣ����Զ�������
        /// </summary>
        /// <param name="Content">Ҫ���������</param>
        /// <param name="FileName">Ŀ���ļ�</param>
        /// <returns></returns>
        public static int WriteToFile(object Content, string FileName)
        {
            using (StreamWriter Writer = File.AppendText(FileName))
            {
                Writer.WriteLine(Content.ToString());
            }

            return 1;
        }

        /// <summary>
        /// ����ļ�
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <returns></returns>
        public static int ClearFile(string FileName)
        {
            using (StreamWriter Writer = new StreamWriter(FileName,false))
            {
                Writer.BaseStream.SetLength(0);
                Writer.Close();
            }

            return 1;
        }
    }
}
