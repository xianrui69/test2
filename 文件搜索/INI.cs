using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 文件搜索
{
    public static class INI
    {

        #region "声明变量"

        public static string ReadINI(string section, string key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(section, key, "", temp, 1024, strFilePath);
                return temp.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static void WriteINI(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, strFilePath);
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private static string strFilePath
        {
            get
            {
                var myPath = Application.StartupPath + "\\FileConfig.ini";
                if (!File.Exists(myPath))
                {
                    File.Create(myPath);
                }
                return myPath;
            }
        } //获取INI文件路径
        private static string strSec = ""; //INI文件名

        #endregion
    }
}
