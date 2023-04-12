using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherConsole.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// Linq列表分页方法
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources">列表</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">数量</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> sources, int PageIndex, int PageSize)
        {
            return sources.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }
    }
}
