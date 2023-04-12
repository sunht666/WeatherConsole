using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherConsole.Extension
{
    public static partial class Extensions
    {
        /// <summary>
        /// 过滤除数为0的除法运算
        /// </summary>
        /// <param name="Dividend">被除数</param>
        /// <param name="Divisor">除数</param>
        /// <returns></returns>
        public static double DivideBy(this double Dividend, double Divisor)
        {
            return ((Divisor == 0) ? 0 : Dividend) / ((Divisor == 0) ? 1 : Divisor);
        }

        /// <summary>
        /// 过滤除数为0的除法运算，乘以100
        /// </summary>
        /// <param name="Dividend">被除数</param>
        /// <param name="Divisor">除数</param>
        /// <returns></returns>
        public static double DivideByWithPercent(this double Dividend, double Divisor)
        {
            return (100 * ((Divisor == 0) ? 0 : Dividend)) / ((Divisor == 0) ? 1 : Divisor);
        }

        /// <summary>
        /// 保留小数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="digits">保留小数位数</param>
        /// <returns></returns>
        public static double Round(this double Value, int Digits)
        {
            return Math.Round(Value, Digits);
        }

        /// <summary>
        /// 过滤除数是0的除法运算计算百分比
        /// </summary>
        /// <param name="Dividend">被除数</param>
        /// <param name="Divisor">除数</param>
        /// <param name="Digits">保留小数位数</param>
        /// <param name="NoDataReturnValue">除数是0时显示的值，为空时正常计算</param>
        /// <returns></returns>
        public static string DivideByWithPercentString(this double Dividend, double Divisor, int Digits = 2, string NoDataReturnValue = "-")
        {
            if (Divisor == 0 && NoDataReturnValue != string.Empty)
            {
                return NoDataReturnValue;
            }
            return Dividend.DivideByWithPercent(Divisor).Round(Digits).ToString() + "%";
        }
    }
}
