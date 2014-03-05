using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CommonLibrary
{
    /// <summary>
    /// 此类用于提供调试程序的辅助功能
    /// </summary>
    public class DebugAssist
    {
        /// <summary>
        /// 将指定内容增加到文件中，若目标文件不存在，将自动创建。
        /// </summary>
        /// <param name="Content">要输出的内容</param>
        /// <param name="FileName">目标文件</param>
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
        /// 清空文件
        /// </summary>
        /// <param name="FileName">文件名</param>
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
