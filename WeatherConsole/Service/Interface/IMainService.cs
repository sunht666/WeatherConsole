using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsole.Service.Interface
{
    public interface IMainService
    {
        /// <summary>
        /// 跑主服务
        /// </summary>
        void Run(string cityName, DateTime startTime, DateTime endTime);
    }
}
