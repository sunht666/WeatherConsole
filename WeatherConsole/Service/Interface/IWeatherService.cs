using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsole.Service.Interface
{
    public interface IWeatherService
    {
        /// <summary>
        /// 获取天气
        /// </summary>
        /// <returns></returns>
        Task ExportCityWeatherAsync(string cityName, DateTime startTime, DateTime endTime);
    }
}
