using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherConsole.Extension
{
    public static partial class Extensions
    {
        /// <summary>
        /// 单个单词：首字母大写，其余小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstUpper(this string s)
        {
            try
            {
                return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
            }
            catch { }
            return "";
        }
    }
}
